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

    [SetUp]
    public async Task OneTimeSetUp()
    {
        _playwrightServiceProvider = await PlaywrightServiceProvider.Register(this);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _playwrightServiceProvider.DisposeAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _playwrightServiceProvider.GetRequiredService<ScreenshotTestResult>()
            .AttachToTestContextWhenFailedAsync(Page);
    }

    /// <summary>
    /// There should be 4 errormessages, bug in telephoneNumber errorMessage
    /// </summary>
    [Test]
    public async Task FormRequiredFieldsNotFilled_ShowsErrorMessages()
    {
        MicrosoftPage microsoftPageObject = _playwrightServiceProvider.IPageObjectGetPageObject<MicrosoftPage>(Page);
        await microsoftPageObject.OpenAsync();

        await microsoftPageObject.LeaveMessage.ClickAsync();
        await microsoftPageObject.SendButton.ClickAsync();

        await Expect(microsoftPageObject.FormErrorMessages).ToHaveCountAsync(3);

        string[] expectedErrorMessages = new string[]
        {
            "Je bent je naam vergeten",
            "Je bent je email vergeten",
            "Wat wilde je zeggen?"
        };

        await Expect(microsoftPageObject.FormErrorMessages).ToHaveTextAsync(expectedErrorMessages);
    }

    [Test]
    public async Task FormRequiredFieldsFilled_NoErrorMessages()
    {
        MicrosoftPage microsoftPageObject = _playwrightServiceProvider.IPageObjectGetPageObject<MicrosoftPage>(Page);
        await microsoftPageObject.OpenAsync();

        await microsoftPageObject.LeaveMessage.ClickAsync();
        await microsoftPageObject.SendButton.ClickAsync();

        await Expect(microsoftPageObject.FormErrorMessages).ToHaveCountAsync(3);

        await microsoftPageObject.Name.FillAsync("Ronald Veth");
        await microsoftPageObject.EmailAdress.FillAsync("r.veth@betabit.nl");
        await microsoftPageObject.TelephoneNumber.FillAsync("0612345678");
        await microsoftPageObject.SuggestionQuestion.FillAsync("I have a question!");
        await microsoftPageObject.SendButton.ClickAsync();

        await Expect(microsoftPageObject.FormErrorMessages).ToBeHiddenAsync();
    }
}
