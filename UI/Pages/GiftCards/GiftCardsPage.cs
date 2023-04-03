namespace PlaywrightTests.UI.Pages.GiftCards
{
    using Microsoft.Playwright;

    public class GiftCardsPage : BasePage
    {
        public GiftCardsPage(IPage page)
            : base(page)
        {
        }

        public static string AmountGiftCardDetailsButton => "Amount";

        public static string EmailGiftCardDetailsButton => "Email";

        public static string ToEmailGiftCardDetailsField => "Enter an email for each recipient";

        public static string FromGiftCardDetailsField => "From";

        public static string MessageGiftCardDetailsField => "Message";

        public static string QuantityGiftCardDetailsField => "Quantity";

    }
}