using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.UI.Pages
{
    public class CartPage : BasePage
    {
        private readonly string _cartTotalPrice = "//*[@id='hlb-subcart']/div[1]/span/span[2]";
        private readonly string _checkoutButton = "//*[@id='hlb-ptc-btn']/span";

        public CartPage(IPage page) : base(page) {}

        public async Task<string> GetCartTotalPrice()
        {
            await Page.WaitForSelectorAsync(_cartTotalPrice);

            var element = await Page.QuerySelectorAsync(_cartTotalPrice);

            if (element == null)
            {
                return null;
            }
            
            return await element.InnerTextAsync();
        }
        public async Task WaitForCheckoutButton()
        {
            await Page.WaitForSelectorAsync(_checkoutButton);
        }
    }
}