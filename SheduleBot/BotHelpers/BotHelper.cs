using System.Net;
using System.Text;
using VkNet.Model.Attachments;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using static SheduleBot.Data.BotInfo;
using SheduleBot.Helpers;
using VkNet.Model.RequestParams;

namespace SheduleBot.BotHelpers;
internal static class BotHelper
{
    [Obsolete("Obsolete")]

    public static MediaAttachment UploadPhoto(this VkApi vkApi, FileInfo file)
    {
        var photoServer = vkApi.Photo.GetMessagesUploadServerAsync((long)GroupId).Result;
        using var wc = new WebClient();
        var response = Encoding.ASCII.GetString(wc.UploadFile(photoServer.UploadUrl, file.FullName));
        return vkApi.Photo.SaveMessagesPhotoAsync(response).Result.First();
    }

    public static IEnumerable<MediaAttachment> UploadPhotos(this VkApi vkApi, IEnumerable<FileInfo> files)
    {
        if (!files.Any()) throw new Exception("Length is null");
        return files.Select(file => UploadPhoto(vkApi, file)).ToList();
    }

    public static void SendAnswer(this VkApi vkApi, string eventId, long? peerId, string text)
    {
        vkApi.Messages.SendMessageEventAnswerAsync(eventId, (long)peerId, (long)peerId, new EventData()
        {
            Type = MessageEventType.SnowSnackbar,
            Text = text
        });
    }
    public static async Task SendLastPhotoAsync(this VkApi vkApi, long? peerId)
    {
        await vkApi.Messages.SendAsync(new MessagesSendParams
        {
            RandomId = new Random().Next(),
            Attachments = vkApi.UploadPhotos(FileHelper.GetPngFiles())!,
            PeerId = peerId
        });
    }

}
