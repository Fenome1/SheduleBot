using FileParsing;
using SheduleBot.Helpers;
using static FileParsing.Data.Selectors;

namespace SheduleBot.Services;

public class FileUpdateService
{
    public static string? GroupName { get; set; }
    private static readonly List<string> Selectors = new()
    {
        Now,
        Future
    };

    private const int WaitMinutes = 30;
    public FileUpdateService(string? groupName)
    {
        GroupName = groupName;
    }

    public async void RunAsync()
    {
        DirectoryHelper.InicializeDirectories();
        while (true)
        {
            try
            {
                await UpdateFileAsync();
                await Task.Delay(TimeSpan.FromMinutes(WaitMinutes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }

    public static async Task UpdateFileAsync()
    {
        FileHelper.DeleteFiles();
        await DownloadHelper.DownloadFilesAsync(Selectors);
        ExcelHelper.GroupRangeToPngFiles(FileHelper.GetExcelFiles(), GroupName);
        ImageHelper.CropImages(FileHelper.GetPngFiles());
    }
}