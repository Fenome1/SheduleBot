using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace FileParsing;
public class ElementParser
{
    private readonly HtmlParser _parser;

    public ElementParser(HtmlParser parser)
    {
        _parser = parser;
    }

    public async Task<IHtmlAnchorElement> GetElementBySelectorAsync(string selector, string htmlDocument)
    {
        using var document = await _parser.ParseDocumentAsync(htmlDocument);
        var element = (IHtmlAnchorElement)document.QuerySelector(selector)!;

        if (element is null)
        {
            throw new Exception("Selector not found");
        }

        return element;
    }
}