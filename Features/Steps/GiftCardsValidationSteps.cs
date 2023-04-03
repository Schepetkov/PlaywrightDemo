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
        private readonly GiftCards giftCardsPage = null;
        private readonly ScenarioContext scenarioContext = null;

        public GiftCardsValidationSteps(Driver driver, ScenarioContext scenarioContext)
        {
            this.homePage = new HomePage(driver.Page);
            this.giftCardsPage = new GiftCards(driver.Page);
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

        [Then(@"I pick gift card design '([^']*)'")]
        public async Task PickGiftCardDesignByName(string designName)
        {
            Enum.TryParse(designName, out EGiftCardsDesignName giftCardsDesignName);
            await this.giftCardsPage.ClickToGiftCardDesignButtonByName(giftCardsDesignName);
        }

        [Then(@"I wait load state '([^']*)'")]
        public async Task WaitLoadState(string stateToWaite)
        {
            Enum.TryParse(stateToWaite, out LoadState state);
            await this.homePage.GetPage().WaitForLoadStateAsync(state);
        }

        [Then(@"I click to card type by image name '([^']*)'")]
        public async Task ClickToCardTypeByImageName(string imageName)
        {
            await this.giftCardsPage.GetPage().GetByRole(AriaRole.Button, new () { Name = imageName, Exact = true }).ClickAsync();
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
                bool bCustomAmount = false;
                if (field.Amount != null)
                {
                    await this.giftCardsPage.GetPage().GetByRole(AriaRole.Button, new () { Name = $"${field.Amount}" }).ClickAsync();
                }
                else if (field.CustomAmount != null)
                {
                    await this.giftCardsPage.GetPage().GetByLabel("Amount").FillAsync(field.CustomAmount);
                    bCustomAmount = true;
                }

                if (field.DeliveryEmail != null)
                {
                    await this.giftCardsPage.GetPage().GetByRole(AriaRole.Button, new () { Name = "Email" }).ClickAsync();
                    await this.giftCardsPage.GetPage().GetByPlaceholder("Enter an email for each recipient").FillAsync(field.DeliveryEmail);
                }

                if (field.From != null)
                {
                    await this.giftCardsPage.GetPage().GetByLabel("From").FillAsync(field.From);
                }

                if (field.Message != null)
                {
                    await this.giftCardsPage.GetPage().GetByRole(AriaRole.Textbox, new () { Name = "Message" }).FillAsync(field.Message);
                }

                if (field.Quantity != null)
                {
                    await this.giftCardsPage.GetPage().GetByText("Quantity").FillAsync(field.Quantity);
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

                    await this.giftCardsPage.GetPage().Locator("#gc-order-form-date i").ClickAsync();
                    await this.giftCardsPage.GetPage().GetByRole(AriaRole.Link, new () { Name = date, Exact = true }).ClickAsync();
                }

                int.TryParse(field.Quantity, out int ammountQuantity);

                int ammountNumber = 0;
                var result = bCustomAmount ? int.TryParse(field.CustomAmount, out ammountNumber) : int.TryParse(field.Amount, out ammountNumber);

                var totalAmmount = ammountQuantity * ammountNumber;

                await this.giftCardsPage.GetPage().Locator("#gc-buy-box-text").GetByText($"${totalAmmount}").ClickAsync();

                this.scenarioContext.Set<int>(totalAmmount, HomePage.TotalAmount);
            }
        }

        [Then(@"I click to button by name '([^']*)'")]
        public async Task ClickToButtonByName(string buttonName)
        {
            await this.giftCardsPage.GetPage().GetByRole(AriaRole.Button, new () { Name = buttonName }).ClickAsync();
        }

        [Then(@"I validate cart total amount")]
        public async Task ValidateCartTotalAmmount()
        {
            await this.homePage.GetPage().GetByText($"Cart Subtotal: ${this.scenarioContext.Get<int>(HomePage.TotalAmount)}").ClickAsync();
        }
    }
}
