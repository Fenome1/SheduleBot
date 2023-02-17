using System.Globalization;
using Aspose.Cells;
using static FileParsing.Data.FolderPath;
using System.Drawing;
using Range = Aspose.Cells.Range;

namespace ExcelParsing;
public class ExcelLinker : IDisposable
{
    private readonly Workbook _workbook;
    private readonly Range _groupRange;
    public Worksheet Sheet => _workbook.Worksheets[0];
    public Cells? Cells => Sheet.Cells;
    public ExcelLinker(Workbook workbook, Range groupRange)
    {
        _workbook = workbook;
        _groupRange = groupRange;
        _workbook.Worksheets.Add();
    }

    private void LinkRange(FileInfo file)
    {
        var fileName = Path.GetFileNameWithoutExtension(file.FullName);
        Cells[0, 0].Value = fileName;
        Cells.InsertCutCells(_groupRange, 1, 0, ShiftType.Down);

        var infoStyle = GetInfoCellStyle(Cells[1, 0].GetStyle(), DateTimeFormatInfo.CurrentInfo);
        Cells[1, 0].SetStyle(infoStyle);
        Cells[1, 1].SetStyle(infoStyle);
    }

    private static Style GetInfoCellStyle(Style style, DateTimeFormatInfo dateTimeFormatInfo)
    {
        style.ForegroundColor = GetNumberOfWeek(DateTime.Today, dateTimeFormatInfo) % 2 == 0 ? Color.FromArgb(0, 255, 255, 0)
            : Color.FromArgb(0, 146, 208, 80); // Yellow : Green
        return style;
    }

    private static int GetNumberOfWeek(DateTime dateTime, DateTimeFormatInfo dateTimeFormatInfo)
        => dateTimeFormatInfo.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

    public void SavePngRange(FileInfo file)
    {
        LinkRange(file);
        var pngFile = Path.Combine(Photo, Path.GetFileNameWithoutExtension(file.Name) + ".png");
        _workbook.Save(pngFile, SaveFormat.Png);
    }

    public void Dispose()
    {
        _workbook.Dispose();
        GC.SuppressFinalize(this);
    }
}