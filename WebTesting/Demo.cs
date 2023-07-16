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
                Headless = true
            });
            page = await browser.NewPageAsync();
        }
        [Test]
        public async Task GoToPageDocsPlaywright()
        {
            //Arrange
            await page.GotoAsync("https://playwright.dev");
            //Action
            var button = await page.QuerySelectorAsync("#docusaurus_skipToContent_fallback > header > div > div > a");
            //Assert
            if (button != null)
            {
                await button.ClickAsync();
                Assert.That(await button.GetAttributeAsync("href"), Is.EqualTo("/docs/intro"));
                Assert.That(page.Url.ToString(), Is.EqualTo("https://playwright.dev/docs/intro"));
            }
        }
        [Test]
        public async Task GoToLoginEaaP()
        {
            //Arrange
            await page.GotoAsync("http://eaapp.somee.com");
            await page.ClickAsync("text=Login");
            var titleVisible = await page.Locator("h2").IsVisibleAsync();
            //Action
            await page.FillAsync("#UserName", "admin");
            await page.FillAsync("#Password", "password");
            await page.ClickAsync("text=Log in");
            var employeeVisible = await page.Locator("text='Employee Details'").IsVisibleAsync();
            //Assert
            Assert.IsTrue(employeeVisible);
            Assert.IsTrue(titleVisible);
            Console.WriteLine($"Titulo visivel:{titleVisible}");
            Console.WriteLine($"Employee Details visivel:{employeeVisible}");
        }
    }
}
