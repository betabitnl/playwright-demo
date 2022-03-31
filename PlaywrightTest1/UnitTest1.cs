using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTest1;

public class Tests : PageTest
{
    [SetUp]
    public void Setup()
    {
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        var options = base.ContextOptions() ?? new();
        options.RecordVideoDir = "videos";
        
        return options;
    }

    [Test]
    public async Task Test1()
    {
        await Page.GotoAsync("https://podcast.betatalks.nl/");
        await Page.Locator(".episode-list--play").First.ClickAsync();
        
        Assert.Pass();
    }

    [TearDown]
    public async Task TearDown()
    {
        if (Page?.Video != null)
            TestContext.AddTestAttachment(await Page.Video.PathAsync());

    }
}
