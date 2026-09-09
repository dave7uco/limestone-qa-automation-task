using OpenQA.Selenium;

namespace Limestone.Automation.Tests.Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;

    private static By UsernameInput => By.Id("user-name");
    private static By PasswordInput => By.Id("password");
    private static By LoginButton => By.Id("login-button");

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Open(string baseUrl)
    {
        _driver.Navigate().GoToUrl(baseUrl);
    }

    public InventoryPage LoginAs(string username, string password)
    {
        _driver.FindElement(UsernameInput).SendKeys(username);
        _driver.FindElement(PasswordInput).SendKeys(password);
        _driver.FindElement(LoginButton).Click();

        return new InventoryPage(_driver);
    }
}
