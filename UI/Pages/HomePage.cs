namespace PlaywrightTests.UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Playwright;
    using PlaywrightTests.Models;

    public class HomePage : BasePage
    {
        public HomePage(IPage page)
           : base(page)
        {
        }

        public static string TotalAmount => "TotalAmount";

        private static string AmazonSearchPlaceholder => "Search Amazon";

        private static string EGiftCard => "Amazon.com eGift Card";

        // Part of the URL can be different
        private static string EGiftCardUrlStringValidation => "**/Amazon-eGift-Card-Logo/**";

        public async Task Search(string searchText)
        {
            await this.GetPage().GetByPlaceholder(AmazonSearchPlaceholder).FillAsync(searchText);
            await this.GetPage().GetByPlaceholder(AmazonSearchPlaceholder).PressAsync("Enter");
        }

        public async Task ClickToGiftCardsByType(EGiftCardsType giftCardsType)
        {
            switch (giftCardsType)
            {
                case EGiftCardsType.EGiftCards:
                    await this.GetPage().RunAndWaitForNavigationAsync(async () =>
                    {
                        await this.ClickToEGiftCard();
                    }, new PageRunAndWaitForNavigationOptions
                    {
                        UrlString = EGiftCardUrlStringValidation,
                    });
                    break;
                default:
                    throw new InvalidOperationException($"HomePage.ClickToGiftCardsByType::giftCardsType - has incorrect type name: {giftCardsType}");
            }
        }

        // alt="Amazon.com eGift Card"
        private async Task ClickToEGiftCard() => await this.GetPage().GetByRole(AriaRole.Link, new () { Name = EGiftCard }).First.ClickAsync();
    }
}
