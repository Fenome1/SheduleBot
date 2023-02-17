using SheduleBot.Services;
using VkNet;

namespace SheduleBot;

internal class Program
{
    public static void Main(string[] args)
    {
        new FileUpdateService("ИСПП-01").RunAsync();
        new BotService(new VkApi()).Run();
    }
}
