using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using System.Threading.Tasks;

namespace PlaywrightTest1.PageObjects
{
    public abstract class BasePageObject : IPageObject
    {
        protected IPage _page;
        private readonly UrlService urlService;

        protected BasePageObject(UrlService urlService)
        {
            this.urlService = urlService;
        }

        protected async Task Navigate(string relativePath = "")
        {
            await urlService.NavigateTo(_page, relativePath);
        }

        public void SetPage(IPage page)
        {
            _page = page;
        }

        public abstract Task OpenAsync();
    }
}
