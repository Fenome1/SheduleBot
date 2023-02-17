namespace FileParsing;
public class HtmlDownloader : IDisposable
{
    private readonly Uri _baseUri;
    private readonly HttpClient _httpClient;
    public HtmlDownloader(Uri baseUri, HttpClient httpClient)
    {
        _baseUri = baseUri;
        _httpClient = httpClient;
    }

    public async Task<string> GetHtmlPageCodeAsync()
    {
        using var response = await _httpClient.GetAsync(_baseUri);
        return await response.Content.ReadAsStringAsync();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}