using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class WebDriverManager
    {
        readonly IWebDriver driver;
        public WebDriverManager(string browserType)
        {
            switch (browserType.ToLower())
            {
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-maximized");
                    driver = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--start-maximized");
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("start-maximized");
                    driver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new WebDriverException($"Unsupported browser type: {browserType}");
            }
        }

        public IWebDriver GetDriver()
        {
            return driver;
        }
    }
}
