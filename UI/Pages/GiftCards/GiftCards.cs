namespace PlaywrightTests.UI.Pages.GiftCards
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Playwright;
    using PlaywrightTests.Models;

    public class GiftCards : BasePage
    {
        public GiftCards(IPage page)
            : base(page)
        {
        }

        private static string StandardButtonDesignName => "Standard";

        private static string AnimatedButtonDesignName => "Animated";

        public async Task ClickToGiftCardDesignButtonByName(EGiftCardsDesignName cardsDesignName)
        {
            switch (cardsDesignName)
            {
                case EGiftCardsDesignName.Standard:
                    await this.ClickToStandardDesign();
                    break;
                case EGiftCardsDesignName.Animated:
                    await this.ClickToAnimatedDesign();
                    break;
                default:
                    throw new InvalidOperationException($"GiftCards.ClickToGiftCardDesignButtonByName::cardsDesignName - has incorrect disgn name: {cardsDesignName}");
            }
        }

        private async Task ClickToStandardDesign() => await this.GetPage().GetByRole(AriaRole.Button, new () { Name = StandardButtonDesignName, Exact = true }).ClickAsync();

        private async Task ClickToAnimatedDesign() => await this.GetPage().GetByRole(AriaRole.Button, new () { Name = AnimatedButtonDesignName, Exact = true }).ClickAsync();
    }
}