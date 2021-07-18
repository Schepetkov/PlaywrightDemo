using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.UI.Pages
{
    public class HomePage : BasePage
    {
        private static string GiftCards => "text=Gift Cards";
        private static string DoNotChangeButton => "text=Don't Change Change Address";

        public HomePage(IPage page) : base(page) { }

        public async Task<bool> IsDoNotChangeButtonVisible()
        {
            return await Page.IsVisibleAsync(DoNotChangeButton);
        }
        public async Task ClickGiftCardsTab()
        {
            await Page.ClickAsync(GiftCards);
        }
        public async Task ClickDoNotChangeButton()
        {
            await Page.ClickAsync(DoNotChangeButton);
        }
    }
}
