using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleCloud
{
    public class ComputeEngine
    {
        private readonly IWebDriver driver;
        public ComputeEngine(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Window.Maximize();
        }
        protected void ScrollDown(int amount)
        {
            new Actions(driver).ScrollByAmount(default, amount).Perform();
        }

        protected void ScrollToElement(By element)
        {
            new Actions(driver).ScrollToElement(driver.FindElement(element)).Perform();
        }

        protected void Wait()
        {
            Thread.Sleep(500);
        }

        #region Locators

        readonly By AddToEstimate_Button = By.XPath("//I[@class='google-material-icons'][text()='add']");
        readonly By ComputeEngine_Tab = By.CssSelector("[data-service-form='8']");
        readonly By AddNumOfInstances_Button = By.XPath("(//DIV[@class='wX4xVc-Bz112c-RLmnJb'])[2]");
        readonly By OS_Droplist = By.XPath("(//DIV[@class='VfPpkd-aPP78e'])[4]");
        readonly By MachineFamily_Droplist = By.XPath("(//DIV[@class='VfPpkd-aPP78e'])[5]");
        readonly By Series_Droplist = By.XPath("(//DIV[@class='VfPpkd-aPP78e'])[6]");
        readonly By Type_Droplist = By.XPath("(//DIV[@class='VfPpkd-aPP78e'])[7]");

        #endregion
        public void SetupEngine(int instancesNum, string OS, string ProvisioningModel, string machineFamily, string series, string machineType)
        {
            driver.FindElement(AddToEstimate_Button).Click();
            driver.FindElement(ComputeEngine_Tab).Click();

            for (int i = 0; i < instancesNum - 1; i++)
            {
                driver.FindElement(AddNumOfInstances_Button).Click();
            }
            Wait();
            //OS
            driver.FindElement(OS_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{OS}']")).Click();

            ScrollDown(280);
            Wait();
            //Model button click
            driver.FindElement(By.XPath($"//LABEL[@class='zT2df'][text()='{ProvisioningModel}']")).Click();
            Wait();

            //Machine family
            driver.FindElement(MachineFamily_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{machineFamily}']")).Click();
            Wait();

            //series
            driver.FindElement(Series_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{series}']")).Click();
            Wait();

            //Type
            driver.FindElement(Type_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{machineType}']")).Click();
            Wait();

        }

        #region Locators

        readonly By addGPU_Button = By.XPath("(//SPAN[@class='eBlXUe-hywKDc'])[5]");
        readonly By GPUType_Droplist = By.CssSelector("[data-field-type='158']");
        readonly By GPUNum_Droplist = By.CssSelector("[data-field-type='174']");
        readonly By LocalSSD_Droplist = By.CssSelector("[data-field-type='180']");
        readonly By Region_Droplist = By.CssSelector("[data-field-type='115']");

        #endregion

        public void SetupGPU(string GPUtype, string GPUnum, string localSSD, string region)
        {
            ScrollDown(900);
            Wait();
            //Add GPU 
            driver.FindElement(addGPU_Button).Click();
            Wait();

            //GPU type
            driver.FindElement(GPUType_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{GPUtype}']")).Click();
            Wait();

            //GPU Num
            driver.FindElement(GPUNum_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{GPUnum}']")).Click();
            Wait();

            //LocalSSD
            driver.FindElement(LocalSSD_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{localSSD}']:nth-of-type(3)")).Click();
            ScrollDown(280);

            Wait();
            //Region
            driver.FindElement(Region_Droplist).Click();
            Wait();
            driver.FindElement(By.CssSelector($"[data-value='{region}']")).Click();
            Wait();

            //Committed use discount options
            driver.FindElement(By.XPath("//LABEL[@class='zT2df'][text()='1 year']")).Click();

            Wait();
            //Close Hint
            driver.FindElement(By.XPath("//SPAN[@class='close']")).Click();

            Wait();

        }

        public void Share()
        {
            driver.FindElement(By.XPath("//SPAN[@jsname='V67aGc'][text()='Share']")).Click();
            Thread.Sleep(1000);
            var share = driver.FindElement(By.CssSelector("[class='tltOzc MExMre rP2xkc jl2ntd']"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(d => share.Displayed);
            Thread.Sleep(2000);
            share.Click();
        }
    }
}
