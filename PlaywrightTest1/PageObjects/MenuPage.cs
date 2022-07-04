using Microsoft.Playwright;

namespace PlaywrightTest1.PageObjects;

public class MenuPage
{
    private readonly IPage _page;

    public ILocator Kennis => _page.Locator(".c-header .c-nav-primary a:text('Kennis')");
    public ILocator WerkenBij => _page.Locator(".c-header .c-nav-primary a:text('Werken bij')");

    public MenuPage(IPage page)
    {
        _page = page;
    }
}