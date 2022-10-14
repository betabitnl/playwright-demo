using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTest1.Helpers;

public class ScreenshotTestResult
{

    public ScreenshotTestResult()
    { }

    public async Task AttachToTestContextWhenFailedAsync(IPage page)
    {
        TestContext testContext = TestContext.CurrentContext;

        if (testContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            string uniqueName = Path.Combine(Path.GetTempPath(), $"{testContext.Test.ID}_{DateTime.Now.ToString("yyyyMMddHHmmssss")}.png");

            await page.ScreenshotAsync(new PageScreenshotOptions { Path = uniqueName });

            TestContext.AddTestAttachment(uniqueName, testContext.Test.MethodName);
        }
    }
}