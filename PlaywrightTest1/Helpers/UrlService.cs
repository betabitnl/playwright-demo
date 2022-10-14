using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTest1.Helpers;

public class UrlService
{
    private readonly string _baseUrl;

    public UrlService(IConfiguration configuration)
    {
        _baseUrl = configuration["BaseUrl"];
    }

    public async Task NavigateTo(IPage page, string urlExtension = "")
    {
        await page.GotoAsync(Path.Combine(_baseUrl, urlExtension), new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
    }
}