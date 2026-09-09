using Limestone.Automation.Tests.Configuration;
using Limestone.Automation.Tests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Limestone.Automation.Tests.Tests.UI;

[TestFixture]
public class SauceDemoTests
{
    private IWebDriver? _driver;

    [SetUp]
    public void SetUp()
    {
        _driver = TestConfiguration.Browser switch
        {
            "Chrome" => new ChromeDriver(),
            _ => throw new NotSupportedException(
                $"Browser '{TestConfiguration.Browser}' is not supported by this assignment.")
        };
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [Test]
    public void ValidUserCanLogIn()
    {
        var loginPage = OpenLoginPage();

        var inventoryPage = loginPage.LoginAs(
            TestConfiguration.SauceDemoUsername,
            TestConfiguration.SauceDemoPassword);

        Assert.That(inventoryPage.IsDisplayed(), Is.True);
    }

    [Test]
    public void UserCanAddBackpackToCart()
    {
        const string productName = "Sauce Labs Backpack";
        var loginPage = OpenLoginPage();
        var inventoryPage = loginPage.LoginAs(
            TestConfiguration.SauceDemoUsername,
            TestConfiguration.SauceDemoPassword);

        inventoryPage.AddBackpackToCart();
        var cartPage = inventoryPage.OpenCart();

        Assert.That(cartPage.ContainsProduct(productName), Is.True);
    }

    private LoginPage OpenLoginPage()
    {
        var loginPage = new LoginPage(_driver!);
        loginPage.Open(TestConfiguration.SauceDemoBaseUrl);
        return loginPage;
    }
}
