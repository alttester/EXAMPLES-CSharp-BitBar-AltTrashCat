using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace alttrashcat_tests_csharp.tests
{
    public class BaseTest
    {
        public AndroidDriver<AndroidElement> appiumDriver;
        public AltDriver altDriver;
        [OneTimeSetUp]
        public void SetupAppiumAndAltDriver()
        {

            AppiumOptions capabilities = new AppiumOptions();
            string appPath = System.Environment.CurrentDirectory + "/../../../application.apk";
            capabilities.AddAdditionalCapability("appium:deviceName", "Android Phone");
            capabilities.AddAdditionalCapability("platformName", "Android");
            capabilities.AddAdditionalCapability("appium:automationName", "uiautomator2");
            capabilities.AddAdditionalCapability("appium:newCommandTimeout", 2000);
            capabilities.AddAdditionalCapability("appium:app", appPath);

            Console.WriteLine("WebDriver request initiated. Waiting for response, this typically takes 2-3 mins");

            appiumDriver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), capabilities, TimeSpan.FromSeconds(36000));
            appiumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Console.WriteLine("Appium driver started");
            altDriver = new AltDriver();
            Console.WriteLine("AltDriver started");
        }

        [OneTimeTearDown]
        public void DisposeAppiumAndAltDriver()
        {
            Console.WriteLine("Ending");
            altDriver.Stop();
            appiumDriver.Quit();
        }
    }
}
