using Microsoft.Playwright;

namespace PlaywrightTests.UI.Pages
{
    public class BasePage
    {
        protected IPage Page;
        public BasePage(IPage page) => Page = page;

        public IPage GetPage() => Page;
    }
}
