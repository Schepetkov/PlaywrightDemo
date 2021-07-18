using Microsoft.Playwright;
using PlaywrightTests.Models;
using System.Threading.Tasks;

namespace PlaywrightTests.WebDriver
{
    public class PlaywrightDriver
    {
        public async Task<IPage> CreatePlaywright(BrowserType inBrowser, BrowserTypeLaunchOptions inLaunchOptions)
        {
            var playwright = await Playwright.CreateAsync();

            IBrowser browser = null;
            if (inBrowser == BrowserType.Chromium)
            {
                browser = await playwright.Chromium.LaunchAsync(inLaunchOptions);
            }
               
            if (inBrowser == BrowserType.Firefox)
            {
                browser = await playwright.Firefox.LaunchAsync(inLaunchOptions);
            }

            if (inBrowser == BrowserType.WebKit)
            {
                browser = await playwright.Webkit.LaunchAsync(inLaunchOptions);
            }

            return await browser.NewPageAsync();
        }
    }
}
