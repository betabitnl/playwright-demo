using Microsoft.Playwright;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTest1.Helpers;

public class UrlService
{
    private readonly IPage _page;
    private readonly string _baseUrl;

    public UrlService(IPage page, string baseUrl)
    {
        _page = page;
        _baseUrl = baseUrl;
    }

    public async Task NavigateTo(string urlExtension = "")
    {
        await _page.GotoAsync(Path.Combine(_baseUrl, urlExtension), new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
    }
}