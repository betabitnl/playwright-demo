using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using System.Threading.Tasks;

namespace PlaywrightTest1.PageObjects;

public class MicrosoftPage
{
    private readonly IPage _page;
    private readonly UrlService _urlService;

    public ILocator LeaveMessage => _page.Locator("a[href='#contact-form-0']").First;
    public ILocator Name => _page.Locator("input[placeholder='Jouw naam']");
    public ILocator TelephoneNumber => _page.Locator("input[placeholder='Jouw telefoonnummer']");
    public ILocator EmailAdress => _page.Locator("input[placeholder='Jouw e-mailadres']");
    public ILocator SuggestionQuestion => _page.Locator(".c-input-text--textarea");
    public ILocator FormErrorMessages => _page.Locator(".field-validation-error");
    public ILocator SendButton => _page.Locator("button[title='Verzenden']");

    public MicrosoftPage(IPage page, UrlService urlService)
    {
        _page = page;
        _urlService = urlService;
    }

    public async Task OpenAsync()
    {
        await _urlService.NavigateTo("microsoft");
    }
}