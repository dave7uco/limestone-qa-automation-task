using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Limestone.Automation.Tests.Pages;

public class CartPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private static By CartList => By.ClassName("cart_list");
    private static By ItemNames => By.ClassName("inventory_item_name");

    public CartPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public bool ContainsProduct(string productName)
    {
        _wait.Until(driver => driver.FindElement(CartList).Displayed);

        return _driver.FindElements(ItemNames)
            .Any(item => item.Text.Equals(productName, StringComparison.Ordinal));
    }
}
