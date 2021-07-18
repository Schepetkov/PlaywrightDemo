using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Models;
using PlaywrightTests.WebDriver;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace PlaywrightTests.Hooks
{
    [Binding]
    class HooksInitializer
    {
        Context _context;
        public HooksInitializer(Context context) => _context = context;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            _context.Page = await playwrightDriver.CreatePlaywright(BrowserType.Chromium, new BrowserTypeLaunchOptions { Headless = false });
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
           await _context.Page.CloseAsync();
        }
    }
}
