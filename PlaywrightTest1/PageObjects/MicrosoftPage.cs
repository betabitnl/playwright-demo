using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using System.Threading.Tasks;

namespace PlaywrightTest1.PageObjects;

public class MicrosoftPage : BasePageObject, IPageObject
{
    public ILocator LeaveMessage => _page.Locator("a[href^='#contact-form-']").First;
    public ILocator Name => _page.Locator("input[placeholder='Jouw naam']");
    public ILocator TelephoneNumber => _page.Locator("input[placeholder='Jouw telefoonnummer']");
    public ILocator EmailAdress => _page.Locator("input[placeholder='Jouw e-mailadres']");
    public ILocator SuggestionQuestion => _page.Locator(".c-input-text--textarea");
    public ILocator FormErrorMessages => _page.Locator(".field-validation-error");
    public ILocator SendButton => _page.Locator("button[title='Verzenden']");

    public MicrosoftPage(UrlService urlService)
        : base(urlService)
    { }

    public override async Task OpenAsync()
    {
        await Navigate("microsoft");
    }
}