using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;
using VkNet;

namespace SheduleBot.BotHelpers;

internal static class KeyboardHelper
{
    private static Random _random = new();
    public static void SendKeyboard(this VkApi vkApi, long peerId, MessageKeyboard keyboard)
    {
        vkApi.Messages.Send(new MessagesSendParams
        {
            RandomId = _random.Next(),
            PeerId = peerId,
            Keyboard = keyboard,
            Message = "Ура!"
        });
    }
}