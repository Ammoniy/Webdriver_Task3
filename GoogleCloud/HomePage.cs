using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloud
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Window.Maximize();
        }

        #region locators
        readonly By searchButton = By.ClassName("p1o4Hf");
        readonly By searchField = By.TagName("input");
        readonly By searchAssertButton = By.XPath("//i[@class='google-material-icons PETVs PETVs-OWXEXe-UbuQg']");
        
        #endregion

        public void HPSearch(string searchData_text, string searchLink_text)
        {
            By searchLink = By.XPath($"(//A[@class='K5hUy'][text()='{searchLink_text}'])[1]");
            driver.FindElement(searchButton).Click();
            driver.FindElement(searchField).SendKeys(searchData_text);
            driver.FindElement(searchAssertButton).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(searchLink).Click();
        }
    }
}
