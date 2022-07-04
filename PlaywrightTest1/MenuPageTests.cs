using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.Init;
using PlaywrightTest1.PageObjects;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlaywrightTest1;

[Parallelizable(ParallelScope.Self)]
public class MenuPageTests : PageTest
{
    private PlaywrightServiceProvider _playwrightServiceProvider;
    private IPage _page;
    private ScreenshotTestResult _screenshotTestResult;
    private UrlService _urlService;
    private MenuPage _menu;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _playwrightServiceProvider = new PlaywrightServiceProvider();
        _page = await _playwrightServiceProvider.GetRequiredServiceAsync<IPage>();
        _screenshotTestResult = await _playwrightServiceProvider.GetRequiredServiceAsync<ScreenshotTestResult>();
        _urlService = await _playwrightServiceProvider.GetRequiredServiceAsync<UrlService>();
        _menu = await _playwrightServiceProvider.GetRequiredServiceAsync<MenuPage>();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _playwrightServiceProvider.DisposeAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _screenshotTestResult.AttachToTestContextWhenFailedAsync();
    }

    [Test]
    public async Task NavigationToKennis()
    {
        await _urlService.NavigateTo();

        var expectedUrlEnd = new Regex(@".*\/kennis");

        await Expect(_page).Not.ToHaveURLAsync(expectedUrlEnd);

        await _menu.Kennis.ClickAsync();

        await Expect(_page).ToHaveURLAsync(expectedUrlEnd);
    }

    [Test]
    public async Task NavigationToWerkenBij()
    {
        await _urlService.NavigateTo();

        var expectedUrlEnd = new Regex(@".*\/werken-bij");

        await Expect(_page).Not.ToHaveURLAsync(expectedUrlEnd);

        await _menu.WerkenBij.ClickAsync();

        await Expect(_page).ToHaveURLAsync(expectedUrlEnd);
    }
}
