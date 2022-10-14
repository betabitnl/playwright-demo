using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using System.Threading.Tasks;

namespace PlaywrightTest1.PageObjects;

public class MenuPage : BasePageObject, IPageObject
{
    public ILocator Kennis => _page.Locator(".c-header .c-nav-primary a:text('Kennis')");
    public ILocator WerkenBij => _page.Locator(".c-header .c-nav-primary a:text('Werken bij')");

    public MenuPage(UrlService urlService)
        : base(urlService)
    { }

    public override async Task OpenAsync()
    {
        await Navigate();
    }
}