using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.UI.Pages
{
    public class GiftCards : BasePage
    {
        public readonly string SelectedAnimatedButton = ".a-button-selected #gc-customization-type-button-Animated";
        public readonly string AnimatedButton = "//*[@id='gc-customization-type-button-Animated']";
        public readonly string DesignTitle = "//*[@id='gc-design-title']";
        public readonly string DesignTitleAnimatedText = "Workplace Thank You (Animated)";
        public readonly string Qty = "//*[@id='gc-buy-box-text']/span";
        public readonly string EGift = "img[alt=\"eGift\"]";
        public readonly string ToField = "textarea[name=\"emails\"]";
        public readonly string FromField = "[placeholder=\"Your name\"]";
        public readonly string MessageField = "textarea[name=\"message\"]";
        public readonly string Quantity = "input[name=\"quantity\"]";
        public readonly string AddToCartButton = "input[name=\"submit.gc-add-to-cart\"]";
       
        #region GiftGardAmount
        private const string TwentyFive = "text=$25";
        private const string Fifty = "text=$50";
        private const string SeventyFive = "text=$75";
        private const string OneHundred = "text=$100";

        private const string СustomAmount = "[placeholder =\"Enter amount\"]";
        private const string GiftGardAmount = "#gc-live-preview-amount";

        private bool _hasCustomAmount = false;
        #endregion

        public GiftCards(IPage page) : base(page) { }
        
        public async Task WaitUntilDesignTitleTextChange()
        {
            if (await GetDesignTitleText() != DesignTitleAnimatedText)
            {
                await Page.WaitForTimeoutAsync(100f);
                await WaitUntilDesignTitleTextChange();
            }
            else 
            {
                return;
            }
        }

        public async Task<bool> IsAnimatedButtonSelected()
        {
            var element = await Page.QuerySelectorAsync(SelectedAnimatedButton);
            
            if (element == null)
            {
                return false;
            }

            return true;
        }
        public async Task<string> GetDesignTitleText()
        {
            await Page.WaitForSelectorAsync(DesignTitle);
            
            var element = await Page.QuerySelectorAsync(DesignTitle);

            if (element == null)
            {
                return null;
            }

            return await element.InnerTextAsync();
        }
        public async Task<string> GetQtyAmount()
        {
            var element = await Page.QuerySelectorAsync(Qty);

            if (element == null)
            {
                return null;
            }

            return await element.InnerTextAsync();
        }
        public async Task ClickToGiftCardAmount(string inSelector)
        {
            if (_hasCustomAmount)
            {
                await Page.FillAsync(СustomAmount, inSelector);
                return;
            }

            await Page.ClickAsync(inSelector);
        }
        public string GetGiftGardSelectorByAmount(int inAmount)
        {
            if (inAmount == 25)
            {
                return TwentyFive;
            }

            if (inAmount == 50)
            {
                return Fifty;
            }

            if (inAmount == 75)
            {
                return SeventyFive;
            }

            if (inAmount == 100)
            {
                return OneHundred;
            }

            _hasCustomAmount = true;
            return inAmount.ToString();
        }
        public async Task<string> GetGiftGardAmount()
        {
            await Page.WaitForSelectorAsync(GiftGardAmount);

            var element = await Page.QuerySelectorAsync(GiftGardAmount);

            if (element == null)
            {
                return null;
            }

            return await element.InnerTextAsync();
        }
    }
}
