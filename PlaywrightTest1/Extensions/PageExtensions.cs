using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTest1.Extensions;

public static class PageExtensions
{
    public static async Task WaitForSpinner(this IPage page)
    {
        var spinnerSelector = ".spinnerselector";
        await WaitToAppearAndDisappearAsync(page, spinnerSelector);
    }

    /// <summary>
    /// Spinners/loaders are hard to expect. Can be that the spinner not yet appeared when you try to wait until it is hidden.
    /// That's why you first need to wait a little time, because it can also already be gone, before you wait for it to be hidden.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="selector"></param>
    private static async Task WaitToAppearAndDisappearAsync(IPage page, string selector)
    {
        try
        {
            await page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions() { State = WaitForSelectorState.Visible, Timeout = 500 });
        }
        catch { }
        finally
        {
            await page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions() { State = WaitForSelectorState.Hidden });
        }
    }
}