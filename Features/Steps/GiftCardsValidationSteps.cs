using NUnit.Framework;
using PlaywrightTests.Hooks;
using System.Threading.Tasks;
using PlaywrightTests.UI.Pages;
using TechTalk.SpecFlow;

namespace PlaywrightTests.Steps
{
    [Binding]
    public class GiftCardsValidationSteps
    {
        readonly Context _context = null;
        readonly HomePage _homePage = null;
        readonly GiftCards _giftCardsPage = null;
        readonly CartPage _cartPage = null;      
        
        int _giftGardAmount = 0;
        int _quantity = 0;

        public GiftCardsValidationSteps(Context context)
        {
            _context = context;
            _homePage = new HomePage(_context.Page);
            _giftCardsPage = new GiftCards(_context.Page);
            _cartPage = new CartPage(_context.Page);
        }

        [Given(@"I navigate to application")]
        public async Task NavigateTo()
        {
            await _context.Page.GotoAsync("https://www.amazon.com/");

            if (_homePage != null && await _homePage.IsDoNotChangeButtonVisible())
            {
                await _homePage.ClickDoNotChangeButton();
            }
        }

        [Given(@"I click on the gift cards tab")]
        public async Task OpenGiftCardsTab()
        {
            await _homePage.ClickGiftCardsTab();
        }

        [Given(@"I click on the picture eGif card")]
        public async Task ClickEGifCard()
        {
            await _giftCardsPage.GetPage().ClickAsync(_giftCardsPage.EGift);

            //check default value
            Assert.AreEqual(await _giftCardsPage.GetGiftGardAmount(), "$50.00");
        }

        [Given(@"I choose the amount '(.*)'\$")]
        public async Task ChooseGiftCardAmount(int giftCardAmount)
        {
            await _giftCardsPage.ClickToGiftCardAmount(_giftCardsPage.GetGiftGardSelectorByAmount(giftCardAmount));

            _giftGardAmount = giftCardAmount;
        }

        [Given(@"Fill in To '(.*)'")]
        public async Task FillToField(string email)
        {
            await _giftCardsPage.GetPage().FillAsync(_giftCardsPage.ToField, email);
        }

        [Given(@"Fill in From '(.*)'")]
        public async Task FillFromField(string fromText)
        {
            await _giftCardsPage.GetPage().FillAsync(_giftCardsPage.FromField, fromText);
        }

        [Given(@"Fill in Message '(.*)'")]
        public async Task FillMessageField(string messageText)
        {
            await _giftCardsPage.GetPage().FillAsync(_giftCardsPage.MessageField, messageText);
        }

        [Given(@"Fill in Quantity '(.*)'")]
        public async Task FillQuantityField(int quantity)
        {
            _quantity = quantity;
            await _giftCardsPage.GetPage().FillAsync( _giftCardsPage.Quantity, quantity.ToString());
        }

        [Then(@"I'm adding a gift card to my cart")]
        public async Task AddGiftCardToCart()
        {
            string price = $"${_giftGardAmount}.00";

            Assert.AreEqual(await _giftCardsPage.GetGiftGardAmount(), price);

            if (_quantity > 1)
            {
                price = "$" + (_giftGardAmount * _quantity).ToString() + ".00";

                //refresh element in DOM for get valid count
                await _giftCardsPage.GetPage().ClickAsync(_giftCardsPage.Qty);
            }
            
            Assert.AreEqual(await _giftCardsPage.GetQtyAmount(), price);
            await _giftCardsPage.GetPage().ClickAsync(_giftCardsPage.AddToCartButton);

            await _cartPage.WaitForCheckoutButton();
            Assert.AreEqual(await _cartPage.GetCartTotalPrice(), price);
        }

        [Given(@"I choose the Animated card")]
        public async Task ChooseAnimatedGiftCard()
        {
            await _giftCardsPage.GetPage().ClickAsync(_giftCardsPage.AnimatedButton);

            await _giftCardsPage.WaitUntilDesignTitleTextChange();

            Assert.AreEqual(await _giftCardsPage.IsAnimatedButtonSelected(), true);
            Assert.AreEqual(await _giftCardsPage.GetDesignTitleText(), _giftCardsPage.DesignTitleAnimatedText);
        }

    }
}
