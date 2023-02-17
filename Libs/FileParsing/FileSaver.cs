using AngleSharp.Html.Dom;
using static FileParsing.Data.FolderPath;

namespace FileParsing;
public class FileSaver : IDisposable
{
    private readonly HttpClient _httpClient;

    private readonly IHtmlAnchorElement _element;
    public string FileName => _element.Text.Replace("ИЗМЕНЕНИЕ В РАСПИСАНИИ НА", "").Trim(); //elementa.TEXT - ИЗМЕНЕНИЕ В РАСПИСАНИИ НА XX МЕСЯЦА 

    public FileSaver(HttpClient httpClient, IHtmlAnchorElement element)
    {
        _httpClient = httpClient;
        _element = element;
    }

    public async Task DownloadFileAsync()
    {
        await using var stream = await _httpClient.GetStreamAsync(_element.Href);
        await using var file = new FileStream($"{Excel}/{FileName}.xls", FileMode.Create);
        await stream.CopyToAsync(file);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}