using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.Init;
using PlaywrightTest1.PageObjects;
using System.Threading.Tasks;

namespace PlaywrightTest1;

[Parallelizable(ParallelScope.Self)]
public class MicrosoftPageTests : PageTest
{
    private PlaywrightServiceProvider _playwrightServiceProvider;
    private IPage _page;
    private ScreenshotTestResult _screenshotTestResult;
    private MicrosoftPage _microsoftPage;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _playwrightServiceProvider = new PlaywrightServiceProvider();
        _page = await _playwrightServiceProvider.GetRequiredServiceAsync<IPage>();
        _screenshotTestResult = await _playwrightServiceProvider.GetRequiredServiceAsync<ScreenshotTestResult>();
        _microsoftPage = await _playwrightServiceProvider.GetRequiredServiceAsync<MicrosoftPage>();
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

    /// <summary>
    /// There should be 4 errormessages, bug in telephoneNumber errorMessage
    /// </summary>
    [Test]
    public async Task FormRequiredFieldsNotFilled_ShowsErrorMessages()
    {
        await _microsoftPage.OpenAsync();
        await _microsoftPage.LeaveMessage.ClickAsync();
        await _microsoftPage.SendButton.ClickAsync();

        await Expect(_microsoftPage.FormErrorMessages).ToHaveCountAsync(3);

        var expectedErrorMessages = new string[]
        {
            "Je bent je naam vergeten",
            "Je bent je email vergeten",
            "Wat wilde je zeggen?"
        };

        await Expect(_microsoftPage.FormErrorMessages).ToHaveTextAsync(expectedErrorMessages);
    }

    [Test]
    public async Task FormRequiredFieldsFilled_NoErrorMessages()
    {
        await _microsoftPage.OpenAsync();
        await _microsoftPage.LeaveMessage.ClickAsync();
        await _microsoftPage.SendButton.ClickAsync();

        await Expect(_microsoftPage.FormErrorMessages).ToHaveCountAsync(3);

        await _microsoftPage.Name.FillAsync("Ronald Veth");
        await _microsoftPage.EmailAdress.FillAsync("r.veth@betabit.nl");
        await _microsoftPage.TelephoneNumber.FillAsync("0612345678");
        await _microsoftPage.SuggestionQuestion.FillAsync("I have a question!");
        await _microsoftPage.SendButton.ClickAsync();

        await Expect(_microsoftPage.FormErrorMessages).ToBeHiddenAsync();
    }
}
