using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace alttrashcat_tests_csharp.tests
{
    public class BaseTest
    {
        public AndroidDriver<AndroidElement> appiumDriver;
        public AltDriver altDriver;
        public String HOST_ALT_SERVER = Environment.GetEnvironmentVariable("HOST_ALT_SERVER");
        public String BITBAR_APIKEY = Environment.GetEnvironmentVariable("BITBAR_APIKEY");
        public String BITBAR_APP_ID_SDK_202 = Environment.GetEnvironmentVariable("BITBAR_APP_ID_SDK_202");
        [OneTimeSetUp]
        public void SetupAppiumAndAltDriver()
        {

            AppiumOptions capabilities = new AppiumOptions();

            capabilities.AddAdditionalCapability("platformName", "Android");
            capabilities.AddAdditionalCapability("appium:deviceName", "Android");
            capabilities.AddAdditionalCapability("appium:automationName", "UiAutomator2");
            capabilities.AddAdditionalCapability("appium:newCommandTimeout", 2000);
            
            capabilities.AddAdditionalCapability("bitbar_apiKey", BITBAR_APIKEY);
            capabilities.AddAdditionalCapability("bitbar_project", "client-side: AltServer on custom host; Android");
            capabilities.AddAdditionalCapability("bitbar_testrun", "Start Page Tests on Samsung");

            // See available devices at: https://cloud.bitbar.com/#public/devices
            capabilities.AddAdditionalCapability("bitbar_device", "Samsung Galaxy A52 -US");
            capabilities.AddAdditionalCapability("bitbar_app", BITBAR_APP_ID_SDK_202);

            Console.WriteLine("WebDriver request initiated. Waiting for response, this typically takes 2-3 mins");

            appiumDriver = new AndroidDriver<AndroidElement>(new Uri("https://eu-mobile-hub.bitbar.com/wd/hub"), capabilities, TimeSpan.FromSeconds(3000));
            appiumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(15);
            Console.WriteLine("Appium driver started");
            altDriver = new AltDriver(host: HOST_ALT_SERVER);
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