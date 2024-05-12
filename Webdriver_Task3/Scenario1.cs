using GoogleCloud;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;

namespace Webdriver_Task3
{
    public class Tests
    {
        IWebDriver driver;


        #region Data

        //Search
        readonly string searchData = "Google Cloud Platform Pricing Calculator";
        readonly string searchLinkText = "Google Cloud Pricing Calculator";
        //Compute Engine 
        readonly int instancesNum = 4;
        readonly string OS = "free-debian-centos-coreos-ubuntu-or-byol-bring-your-own-license";
        readonly string Model = "Regular";
        readonly string MachFamily = "general-purpose";
        readonly string MachSeries = "n1";
        readonly string MachType = "n1-standard-8";
        // GPU
        readonly string GPUType = "nvidia-tesla-v100";
        readonly string GPUNum = "1";
        readonly string LocalSSD = "2";
        readonly string Region = "europe-west4";

        #endregion


        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://cloud.google.com/?hl=en";

            HomePage hp = new HomePage(driver);
            ComputeEngine ce = new ComputeEngine(driver);
            hp.HPSearch(searchData, searchLinkText);
            ce.SetupEngine(instancesNum, OS, Model, MachFamily, MachSeries, MachType);
            ce.SetupGPU(GPUType, GPUNum, LocalSSD, Region);
            ce.Share();

            List<string> windowHandles = driver.WindowHandles.ToList();
            driver.SwitchTo().Window(windowHandles[1]);
        }

        
        [TestCase("Free: Debian, CentOS, CoreOS, Ubuntu or BYOL (Bring Your Own License)")]
        [TestCase("Regular")]
        [TestCase("n1")]
        [TestCase("n1-standard-8, vCPUs: 8, RAM: 30 GB")]
        [TestCase("NVIDIA Tesla V100")]
        [TestCase("2x375 GB")]
        [TestCase("Netherlands")]
        public void ComputeEngineSetup(string expected)
        {
            
            string actual = driver.FindElement(By.XPath("//*[@class='uZSMzf']")).Text;
            Assert.IsTrue(actual.Contains(expected));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}