using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace alttrashcat_tests_csharp.tests
{
    public class BaseTest
    {
        public IOSDriver<IOSElement> appiumDriver;
        public AltDriver altDriver;
        public String HOST_ALT_SERVER = Environment.GetEnvironmentVariable("HOST_ALT_SERVER");
        public String BITBAR_APIKEY = Environment.GetEnvironmentVariable("BITBAR_APIKEY");
        public String BITBAR_APP_ID_SDK_202_IPA = Environment.GetEnvironmentVariable("BITBAR_APP_ID_SDK_202_IPA");
        [OneTimeSetUp]
        public void SetupAppiumAndAltDriver()
        {

            AppiumOptions capabilities = new AppiumOptions();

            capabilities.AddAdditionalCapability("platformName", "iOS");
            capabilities.AddAdditionalCapability("appium:deviceName", "Apple iPhone SE 2020 A2296 13.4.1");
            capabilities.AddAdditionalCapability("appium:automationName", "XCUITest");
            capabilities.AddAdditionalCapability("appium:bundleId", "fi.altom.trashcat");
            capabilities.AddAdditionalCapability("bitbar_apiKey", BITBAR_APIKEY);
            capabilities.AddAdditionalCapability("bitbar_project", "client-side: AltServer on custom host; iOS");
            capabilities.AddAdditionalCapability("bitbar_testrun", "Start Page Tests on Apple iPhone SE 2020 A2296 13.4.1");

            // See available devices at: https://cloud.bitbar.com/#public/devices
            capabilities.AddAdditionalCapability("bitbar_device", "Apple iPhone SE 2020 A2296 13.4.1");
            capabilities.AddAdditionalCapability("bitbar_app", BITBAR_APP_ID_SDK_202_IPA);

            Console.WriteLine("WebDriver request initiated. Waiting for response, this typically takes 2-3 mins");

            appiumDriver = new IOSDriver<IOSElement>(new Uri("https://eu-mobile-hub.bitbar.com/wd/hub"), capabilities, TimeSpan.FromSeconds(3000));
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