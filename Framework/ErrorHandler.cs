using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using OpenQA.Selenium.Support.Extensions;

namespace Framework
{
    public class ErrorHandler
    {
        private readonly IWebDriver driver;
        public ErrorHandler(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void TakeScreenshot()
        {
            string SaveLocation = @"C:\Users\Ammoniy\source\repos\Webdriver_Task3\ErrorScreenshots\" +
                               "failshot_" +
                               $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}" +
                               ".png";
            driver.TakeScreenshot().SaveAsFile(SaveLocation);
        }
    }
}
