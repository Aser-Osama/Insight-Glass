using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

public class SuiteTests : IDisposable
{
    public IWebDriver driver { get; private set; }
    public IDictionary<string, object> vars { get; private set; }
    public IJavaScriptExecutor js { get; private set; }

    public SuiteTests()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); // Run in headless mode
        options.AddArgument("--disable-gpu"); // Disable GPU acceleration
        options.AddArgument("--no-sandbox"); // Bypass OS security model
        options.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems
        options.AddArgument("--ignore-certificate-errors"); // Ignore certificate errors
        options.AddArgument("--window-size=1920,1080"); // Set window size to ensure consistent rendering

        driver = new ChromeDriver(options);
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
    }

    public void Dispose()
    {
        driver.Quit();
    }

    [Fact]
    public void Search()
    {
        // Test name: search
        // Step # | name | target | value
        // 1 | open | / | 
        driver.Navigate().GoToUrl("https://localhost:5173/");

        // Wait for the page to load completely
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

        // 2 | setWindowSize | 2048x1255 | 
        driver.Manage().Window.Size = new System.Drawing.Size(2048, 1255);

        // 3 | click | css=.form-control | 
        driver.FindElement(By.CssSelector(".form-control")).Click();

        // 4 | type | css=.form-control | ba
        driver.FindElement(By.CssSelector(".form-control")).SendKeys("ba");

        // 5 | click | css=.btn-outline-success | 
        driver.FindElement(By.CssSelector(".btn-outline-success")).Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        // 6 | click | css=.row:nth-child(10) .job-logo | 
        driver.FindElement(By.CssSelector(".btn-outline-success")).Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        // 7 | click | css=.col-sm-4 > div:nth-child(1) > div:nth-child(1) | 
        driver.FindElement(By.CssSelector(".col-sm-4 > div:nth-child(1) > div:nth-child(1)")).Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        // 8 | assertElementPresent | css=.col-sm-8 .d-inline-block | 
        {
            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".col-sm-8 .d-inline-block"));
            Assert.True(elements.Count > 0);
        }
    }
}