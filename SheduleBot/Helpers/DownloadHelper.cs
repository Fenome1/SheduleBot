using AngleSharp.Html.Parser;
using FileParsing;
using FileParsing.Data;

namespace SheduleBot.Helpers;

internal static class DownloadHelper
{
    public static async Task DownloadFileAsync(string selector)
    {
        try
        {
            using var htmlDownloader = new HtmlDownloader(Uris.BaseUrl, new HttpClient());
            var htmlDoc = await htmlDownloader.GetHtmlPageCodeAsync();
            var elementParser = new ElementParser(new HtmlParser());
            var anchorElement = await elementParser.GetElementBySelectorAsync(selector, htmlDoc);
            using var fileSaver = new FileSaver(new HttpClient(), anchorElement);
            await fileSaver.DownloadFileAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public static async Task DownloadFilesAsync(IEnumerable<string> selectors)
    {
        if (!selectors.Any()) { return; }

        foreach (var selector in selectors)
            await DownloadFileAsync(selector);
    }
}