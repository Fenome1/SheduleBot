using Aspose.Cells;
using Range = Aspose.Cells.Range;

namespace ExcelParsing;
public class ExcelRangeParser : IDisposable
{
    private const int MaxIndexForLastCouple = 21; // т.к 4-5 пара имеет row = ~20-19;
    private readonly Workbook _workBook;
    private Cell? _groupNameCell;
    public Worksheet Sheet => _workBook.Worksheets[0];
    public Cells Cells => Sheet.Cells;

    public string? GroupName { get; set; }

    public ExcelRangeParser(Workbook workBook)
    {
        _workBook = workBook;
    }

    private Cell? SearchGroupNameCell() => Cells.Find(GroupName, null);

    private int GetLastRowIndex()
    {
        _groupNameCell = SearchGroupNameCell() ?? throw new NullReferenceException("Name not found");

        var currentCellRowIndex = _groupNameCell.Row + 1; //пропуск итерации, тк индекс ячейки с именем уже есть
        var coupleIndexLimit = currentCellRowIndex <= MaxIndexForLastCouple;

        while (true)
        {
            var currentCell = Cells.GetCell(currentCellRowIndex, _groupNameCell.Column);
            if (currentCell == null || !coupleIndexLimit && !currentCell.IsMerged)
                break;

            if (currentCell.IsMerged && coupleIndexLimit)
                currentCellRowIndex += currentCell.GetMergedRange().RowCount;
            else
                currentCellRowIndex++;
        }
        return currentCellRowIndex;
    }

    public Range GetGroupRange()
    {
        if (GroupName is null) throw new ArgumentException("Invalid argument: GroupName");

        var lastRowIndex = GetLastRowIndex();
        var leftCell = Cells.GetCell(lastRowIndex - 1, _groupNameCell.Column + 1);
        return Cells.CreateRange(_groupNameCell.Name, leftCell.Name);
    }

    public void Dispose()
    {
        _workBook.Dispose();
        GC.SuppressFinalize(this);
    }

}