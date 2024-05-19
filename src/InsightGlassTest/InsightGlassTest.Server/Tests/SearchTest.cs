using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

public class SuiteTests : IDisposable
{
    public IWebDriver driver { get; private set; }
    public IDictionary<String, Object> vars { get; private set; }
    public IJavaScriptExecutor js { get; private set; }

    public SuiteTests()
    {
        // Configure Chrome options for headless mode
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); // Run in headless mode
        options.AddArgument("--disable-gpu"); // Disable GPU acceleration
        options.AddArgument("--no-sandbox"); // Bypass OS security model
        options.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems
        options.AddArgument("--ignore-certificate-errors"); // Ignore certificate errors
        options.AddArgument("--window-size=1920,1080"); // Set window size to ensure consistent rendering

        driver = new ChromeDriver(options);
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<String, Object>();
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
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);        
        // 6 | click | css=.row:nth-child(10) .job-logo | 
        driver.FindElement(By.CssSelector(".row:nth-child(10) .job-logo")).Click();
        
        // 7 | click | css=.row:nth-child(10) .job-logo | 
        driver.FindElement(By.CssSelector(".row:nth-child(10) .job-logo")).Click();
        
        // 8 | assertElementPresent | linkText=Apply | Button to apply
        {
            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.LinkText("Apply"));
            Assert.True(elements.Count > 0);
        }
    }
}
