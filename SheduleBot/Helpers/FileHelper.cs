using FileParsing.Data;

namespace SheduleBot.Helpers;
internal static class FileHelper
{
    public static int DeleteFiles()
    {
        var sheduleFiles = Directory.GetFiles(FolderPath.Shedule, "*", SearchOption.AllDirectories);
        Parallel.ForEach(sheduleFiles, File.Delete);
        return sheduleFiles.Length;
    }
    public static IEnumerable<FileInfo> GetExcelFiles() => new DirectoryInfo(FolderPath.Excel).EnumerateFiles()
        .Where(file => file.Extension == ".xls");

    public static IEnumerable<FileInfo> GetPngFiles() => new DirectoryInfo(FolderPath.Photo).EnumerateFiles()
        .Where(file => file.Extension == ".png");
}