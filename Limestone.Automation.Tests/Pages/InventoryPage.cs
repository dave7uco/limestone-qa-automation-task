using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Limestone.Automation.Tests.Pages;

public class InventoryPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private static By InventoryContainer => By.Id("inventory_container");
    private static By BackpackAddButton => By.Id("add-to-cart-sauce-labs-backpack");
    private static By CartLink => By.ClassName("shopping_cart_link");

    public InventoryPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public bool IsDisplayed()
    {
        return _wait.Until(driver =>
            driver.Url.EndsWith("inventory.html", StringComparison.OrdinalIgnoreCase) &&
            driver.FindElement(InventoryContainer).Displayed);
    }

    public void AddBackpackToCart()
    {
        _wait.Until(driver => driver.FindElement(BackpackAddButton)).Click();
    }

    public CartPage OpenCart()
    {
        _driver.FindElement(CartLink).Click();
        return new CartPage(_driver);
    }
}
