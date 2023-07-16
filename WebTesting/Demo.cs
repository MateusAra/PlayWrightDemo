using System.Text.RegularExpressions;
using Microsoft.Playwright;


namespace Demo
{
    [TestFixture]
    public class Tests
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IPage page;

        [SetUp]
        public async Task SetUp()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            page = await browser.NewPageAsync();

        }
        [Test]
        public async Task HomePage()
        {

            await page.GotoAsync("https://playwright.dev");

            var button = await page.QuerySelectorAsync("#docusaurus_skipToContent_fallback > header > div > div > a");

            if (button != null)
            {
                await button.ClickAsync();
                Assert.That(await button.GetAttributeAsync("href"), Is.EqualTo("/docs/intro"));
                Assert.That(page.Url.ToString(), Is.EqualTo("https://playwright.dev/docs/intro"));
            }
        }

    }
}
