namespace PlaywrightTests.Steps
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.Playwright;
    using PlaywrightTests.Models;
    using PlaywrightTests.UI.Pages;
    using PlaywrightTests.UI.Pages.GiftCards;
    using PlaywrightTests.WebDriver;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class GiftCardsValidationSteps
    {
        private readonly HomePage homePage = null;
        private readonly GiftCardsPage giftCardsPage = null;
        private readonly ScenarioContext scenarioContext = null;

        public GiftCardsValidationSteps(Driver driver, ScenarioContext scenarioContext)
        {
            this.homePage = new HomePage(driver.Page);
            this.giftCardsPage = new GiftCardsPage(driver.Page);
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I navigate to '([^']*)'")]
        public async Task NavigateTo(string url)
        {
            await this.homePage.GetPage().GotoAsync(url);
            string title = url.Substring(url.IndexOf(".") + 1);
            string removeLastCharFromTitleResult = title.Remove(title.Length - 1, 1);

            removeLastCharFromTitleResult.ToUpper();
            string titleToValidate = removeLastCharFromTitleResult.Remove(1).ToUpper() + removeLastCharFromTitleResult.Substring(1);

            // validate URL link name
            await Assertions.Expect(this.homePage.GetPage()).ToHaveTitleAsync(new Regex(titleToValidate));
        }

        [Then(@"I search '([^']*)'")]
        public async Task Search(string searchText)
        {
            await this.homePage.Search(searchText);
        }

        [Then(@"I choose the gift card by type name '([^']*)'")]
        public async Task ClickOnGiftCardByTypeName(string giftCardTypeName)
        {
            Enum.TryParse(giftCardTypeName, out EGiftCardsType giftCardsType);
            await this.homePage.ClickToGiftCardsByType(giftCardsType);
        }

        [Then(@"I wait load page state '([^']*)'")]
        public async Task WaitLoadState(string stateToWaite)
        {
            Enum.TryParse(stateToWaite, out LoadState state);
            await this.homePage.GetPage().WaitForLoadStateAsync(state);
        }

        [Then(@"I enter gift card details")]
        public async Task EnterGiftCardDetails(Table details)
        {
            var cardDetails = details.CreateSet<GiftCard>();
            if (cardDetails == null)
            {
                this.homePage.StopTestWithReason("EnterGiftCardDetails::cardDetails == null");
                return;
            }

            foreach (var field in cardDetails)
            {
                int amountValue = 0;
                if (field.Amount != null)
                {
                    await this.ClickToButtonByName($"${field.Amount}");
                    int.TryParse(field.Amount, out amountValue);
                }
                else if (field.CustomAmount != null)
                {
                    await this.giftCardsPage.GetPage().GetByLabel(GiftCardsPage.AmountGiftCardDetailsButton).FillAsync(field.CustomAmount);
                    int.TryParse(field.CustomAmount, out amountValue);
                }

                if (field.DeliveryEmail != null)
                {
                    await this.ClickToButtonByName(GiftCardsPage.EmailGiftCardDetailsButton);
                    await this.giftCardsPage.GetPage().GetByPlaceholder(GiftCardsPage.ToEmailGiftCardDetailsField).FillAsync(field.DeliveryEmail);
                }

                if (field.From != null)
                {
                    await this.giftCardsPage.GetPage().GetByLabel(GiftCardsPage.FromGiftCardDetailsField).FillAsync(field.From);
                }

                if (field.Message != null)
                {
                    await this.giftCardsPage.GetPage().GetByRole(AriaRole.Textbox, new () { Name = GiftCardsPage.MessageGiftCardDetailsField }).FillAsync(field.Message);
                }

                if (field.Quantity != null)
                {
                    await this.giftCardsPage.GetPage().GetByText(GiftCardsPage.QuantityGiftCardDetailsField).FillAsync(field.Quantity);
                }

                if (field.DeliveryDate != null)
                {
                    string date = string.Empty;
                    if (field.DeliveryDate == "Today")
                    {
                        date = DateTime.Today.Day.ToString();
                    }
                    else
                    {
                        date = field.DeliveryDate;
                    }

                    await this.giftCardsPage.ChooseCalendarDate(date);
                }

                int.TryParse(field.Quantity, out int amountQuantity);
                var totalAmountValue = amountQuantity * amountValue;

                // validate total amount before added to cart
                await this.giftCardsPage.GetPage().Locator("#gc-buy-box-text").GetByText($"${totalAmountValue}").ClickAsync();

                // save total amount value
                this.scenarioContext.Set<int>(totalAmountValue, HomePage.TotalAmountKeyName);
            }
        }

        [Then(@"I click to button by name '([^']*)'")]
        public async Task ClickToButtonByName(string buttonName)
        {
            await this.homePage.ClickToButtonByName(buttonName);
        }

        [Then(@"I validate cart total amount")]
        public async Task ValidateCartTotalAmmount()
        {
            await this.homePage.GetPage().GetByText($"Cart Subtotal: ${this.scenarioContext.Get<int>(HomePage.TotalAmountKeyName)}").ClickAsync();
        }
    }
}
