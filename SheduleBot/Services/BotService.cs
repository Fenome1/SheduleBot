using static SheduleBot.Data.BotInfo;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using SheduleBot.BotHelpers;
using VkNet.Exception;
using VkNet.Model.GroupUpdate;

namespace SheduleBot.Services;

internal class BotService
{
    private readonly VkApi _vkApi;

    public BotService(VkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public void Run()
    {
        _vkApi.Authorize(new ApiAuthParams
        {
            AccessToken = Token
        });

        var server = _vkApi.Groups.GetLongPollServerAsync(GroupId).Result;
        var ts = server.Ts;

        while (true)
        {
            try
            {
                var poll = _vkApi.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams
                {
                    Key = server.Key,
                    Server = server.Server,
                    Ts = ts,
                    Wait = 25
                });

                var updateItem = poll.Updates.FirstOrDefault(x => x.Instance is not null);

                if (updateItem is null)
                    continue;

                ts = poll.Ts;

                var updateInstance = updateItem.Instance;
                var typeUpdate = updateInstance.GetType().Name;

                Task.Run(async () =>
                {
                    try
                    {
                        switch (typeUpdate)
                        {
                            case "MessageNew":
                                {
                                    var peerId = ((MessageNew)updateInstance).Message.PeerId;
                                    var message = ((MessageNew)updateInstance).Message;
                                    if (message is null) return;
                                    var mText = message.Text;

                                    _vkApi.SendKeyboard((long)peerId!, Keys.SheduleGetKeyboard);

                                    if (mText.Contains("!gn")
                                        && peerId == AdminId)
                                    {
                                        var newGroupName = mText.Split("!gn").Last().Trim();
                                        FileUpdateService.GroupName = newGroupName;
                                        await FileUpdateService.UpdateFileAsync();
                                    }
                                }
                                break;
                            case "MessageEvent":
                                {
                                    var eEvent = (MessageEvent)updateInstance;
                                    switch (eEvent.Payload)
                                    {
                                        case "1":
                                            {
                                                _vkApi.SendAnswer(eEvent.EventId, eEvent.PeerId, "Получаю!");
                                                await _vkApi.SendLastPhotoAsync(eEvent.PeerId);
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                    catch (Exception sendMessageError)
                    {
                        await _vkApi.Messages.SendAsync(new MessagesSendParams
                        {
                            RandomId = new Random().Next(),
                            Message = sendMessageError.Message,
                            PeerId = AdminId
                        });
                    }
                });
            }
            catch (LongPollKeyExpiredException)
            {
                server = _vkApi.Groups.GetLongPollServer(GroupId);
            };
        }

    }
    
}
