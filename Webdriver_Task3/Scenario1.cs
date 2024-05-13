using Framework;
using GoogleCloud;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;
using NUnit.Framework.Interfaces;
using System.Drawing.Imaging;
using OpenQA.Selenium.Support.Extensions;

namespace Webdriver_Task3
{
    public class Tests
    {
        IWebDriver driver;


        #region BuissnesObj

        readonly ComputeEngine_BO compEng_BO = new ComputeEngine_BO
        {
            InstancesNum = 4,
            OS = "free-debian-centos-coreos-ubuntu-or-byol-bring-your-own-license",
            ProvisioningModel = "Regular",
            MachineFamily = "general-purpose",
            Series = "n1",
            MachineType = "n1-standard-8"
        };

        readonly GPU_BO gpu_BO = new GPU_BO
        {
            Type = "nvidia-tesla-v100",
            Num = "1",
            LocalSSD = "2",
            Region = "europe-west4"
        };

        readonly Search_BO srch_BO = new Search_BO()
        {
            SearchRequest = "Google Cloud Platform Pricing Calculator",
            SearchLinkName = "Google Cloud Pricing Calculator"
        };

        #endregion


        [OneTimeSetUp]
        public void Setup()
        {
            WebDriverManager manager = new WebDriverManager("chrome"); //chrome, firefox, edge
            driver = manager.GetDriver();
            driver.Url = "https://cloud.google.com/?hl=en";

            HomePage hp = new HomePage(driver);
            ComputeEngine ce = new ComputeEngine(driver);
            hp.HPSearch(srch_BO.SearchRequest, srch_BO.SearchLinkName);
            ce.SetupEngine(compEng_BO.InstancesNum, compEng_BO.OS, compEng_BO.ProvisioningModel, compEng_BO.MachineFamily, compEng_BO.Series, compEng_BO.MachineType);
            ce.SetupGPU(gpu_BO.Type, gpu_BO.Num, gpu_BO.LocalSSD, gpu_BO.Region);
            ce.Share();

            List<string> windowHandles = driver.WindowHandles.ToList();
            driver.SwitchTo().Window(windowHandles[1]);
        }

        [TestCase("(//*[@class='Kfvdz'])[10]", "4")]
        [TestCase("(//*[@class='Kfvdz'])[6]", "1")]
        [TestCase("(//*[@class='Kfvdz'])[19]", "1 year")]
        [TestCase("(//*[@class='Kfvdz'])[11]", "Free: Debian, CentOS, CoreOS, Ubuntu or BYOL (Bring Your Own License)")]
        [TestCase("(//*[@class='Kfvdz'])[12]", "Regular")]
        [TestCase("//*[@class='uZSMzf']", "n1")]
        [TestCase("(//*[@class='Kfvdz'])[3]", "n1-standard-8, vCPUs: 8, RAM: 30 GB")]
        [TestCase("(//*[@class='Kfvdz'])[5]", "NVIDIA Tesla V100")]
        [TestCase("(//*[@class='Kfvdz'])[7]", "2x375 GB")]
        [TestCase("(//*[@class='Kfvdz'])[18]", "Netherlands")]
        public void ComputeEngineSetup(string locator, string expected)
        {
            string actual = driver.FindElement(By.XPath(locator)).Text;
            Assert.IsTrue(actual.Contains(expected));
        }

        [OneTimeTearDown]
        public  void TearDown()
        {
            driver.Quit();
        }

        [TearDown]
        public void SnapshotOnFailure()
        {
            ErrorHandler er = new ErrorHandler(driver);
            if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed))
            {
                er.TakeScreenshot();
            }
        }
    }
}