using Aspose.Cells;
using ExcelParsing;

namespace SheduleBot.Helpers;

internal static class ExcelHelper
{
    public static void GroupRangeToPngFile(FileInfo excelFile, string? groupName)
    {
        try
        {
            using var workbook = new Workbook(excelFile.FullName);
            using var excelParser = new ExcelRangeParser(workbook)
            {
                GroupName = groupName
            };
            var groupRange = excelParser.GetGroupRange();
            using var excelLinker = new ExcelLinker(new Workbook(), groupRange, excelFile);
            excelLinker.SaveLinkedRangeToPng();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public static void GroupRangeToPngFiles(IEnumerable<FileInfo> excelFiles, string? groupName)
    {
        if (!excelFiles.Any()) { return; }

        foreach (var excelFile in excelFiles)
            GroupRangeToPngFile(excelFile, groupName);
    }
}