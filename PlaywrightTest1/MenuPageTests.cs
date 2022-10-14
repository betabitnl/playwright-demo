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

    [SetUp]
    public async Task OneTimeSetUp()
    {
        _playwrightServiceProvider = await PlaywrightServiceProvider.Register(this);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _playwrightServiceProvider.GetRequiredService<ScreenshotTestResult>()
            .AttachToTestContextWhenFailedAsync(Page);
    }

    [Test]
    public async Task NavigationToKennis()
    {
        MenuPage menuPageObject = _playwrightServiceProvider.IPageObjectGetPageObject<MenuPage>(Page);
        await menuPageObject.OpenAsync();

        Regex expectedUrlEnd = new Regex(@".*\/kennis");

        await Expect(Page).Not.ToHaveURLAsync(expectedUrlEnd);

        await menuPageObject.Kennis.ClickAsync();

        await Expect(Page).ToHaveURLAsync(expectedUrlEnd);
    }

    [Test]
    public async Task NavigationToWerkenBij()
    {
        MenuPage menuPageObject = _playwrightServiceProvider.IPageObjectGetPageObject<MenuPage>(Page);
        await menuPageObject.OpenAsync();

        Regex expectedUrlEnd = new Regex(@".*\/werken-bij");

        await Expect(Page).Not.ToHaveURLAsync(expectedUrlEnd);

        await menuPageObject.WerkenBij.ClickAsync();

        await Expect(Page).ToHaveURLAsync(expectedUrlEnd);
    }
}
