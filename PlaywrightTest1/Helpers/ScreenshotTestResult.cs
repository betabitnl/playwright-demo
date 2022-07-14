using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTest1.Helpers;

public class ScreenshotTestResult
{
    private readonly IPage _page;

    public ScreenshotTestResult(IPage page)
    {
        _page = page;
    }

    public async Task AttachToTestContextWhenFailedAsync()
    {
        var testContext = TestContext.CurrentContext;

        if (testContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var uniqueName = Path.Combine(Path.GetTempPath(), $"{testContext.Test.ID}_{DateTime.Now.ToString("yyyyMMddHHmmssss")}.png");

            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = uniqueName });

            TestContext.AddTestAttachment(uniqueName, testContext.Test.MethodName);
        }
    }
}