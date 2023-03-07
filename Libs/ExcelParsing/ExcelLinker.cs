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
    private readonly FileInfo _file;
    public Worksheet Sheet => _workbook.Worksheets[0];
    public Cells? Cells => Sheet.Cells;

    public DateTime SheduleDate => DateTime.TryParse(Path.GetFileNameWithoutExtension(_file.Name), out var sheduleFileDate)
        ? sheduleFileDate : DateTime.Today.Date;

    public static readonly Color OddWeekColor = Color.FromArgb(0, 146, 208, 80);
    public static readonly Color EvenWeekColor = Color.FromArgb(0, 255, 255, 0);
    public ExcelLinker(Workbook workbook, Range groupRange, FileInfo file)
    {
        _workbook = workbook;
        _groupRange = groupRange;
        _file = file;
        _workbook.Worksheets.Add();
        LinkRange();
    }
    private void LinkRange()
    {
        var fileName = Path.GetFileNameWithoutExtension(_file.FullName);
        Cells[0, 0].Value = fileName;
        Cells.InsertCutCells(_groupRange, 1, 0, ShiftType.Down);

        var infoStyle = GetInfoCellStyle(Cells[1, 0].GetStyle(), DateTimeFormatInfo.CurrentInfo);
        Cells[1, 0].SetStyle(infoStyle);
        Cells[1, 1].SetStyle(infoStyle);
    }

    private Style GetInfoCellStyle(Style style, DateTimeFormatInfo dateTimeFormatInfo)
    {
        style.ForegroundColor = GetNumberOfWeek(SheduleDate, dateTimeFormatInfo) % 2 == 0 ? EvenWeekColor
            : OddWeekColor;
        return style;
    }

    private static int GetNumberOfWeek(DateTime dateTime, DateTimeFormatInfo dateTimeFormatInfo)
        => dateTimeFormatInfo.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

    public void SaveLinkedRangeToPng()
    {
        var pngFile = Path.Combine(Photo, Path.GetFileNameWithoutExtension(_file.Name) + ".png");
        _workbook.Save(pngFile, SaveFormat.Png);
    }

    public void Dispose()
    {
        _workbook.Dispose();
        GC.SuppressFinalize(this);
    }
}