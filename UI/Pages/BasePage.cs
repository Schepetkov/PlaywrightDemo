namespace PlaywrightTests.UI.Pages
{
    using FluentAssertions;
    using Microsoft.Playwright;

    public class BasePage
    {
        private readonly IPage page;

        public BasePage(IPage page) => this.page = page;

        public IPage GetPage() => this.page;

        public void StopTestWithReason(string reason)
        {
            var bTestFail = true;
            bTestFail.Should().BeFalse($"{reason}");
        }
    }
}