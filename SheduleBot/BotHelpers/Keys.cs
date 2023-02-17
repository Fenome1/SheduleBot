using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace SheduleBot.BotHelpers;

internal static class Keys
{
    public static readonly MessageKeyboard SheduleGetKeyboard = new KeyboardBuilder().AddButton(new MessageKeyboardButtonAction
        {
            Type = KeyboardButtonActionType.Callback,
            Label = "Получить",
            Payload = "1"
        }, KeyboardButtonColor.Primary)
        .AddLine()
        .Build();
}