using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTest1.PageObjects
{
    public interface IPageObject
    {
        void SetPage(IPage page);
        Task OpenAsync();
    }
}
