# Running C# tests using Appium and AltTester® on Bitbar cloud devices

## Server-Side

Follow [Running Cloud-Side Appium tests](https://support.smartbear.com/bitbar/docs/en/mobile-app-tests/automated-testing/appium-support/running-cloud-side-appium-tests.html) to prepare zip archive.

1. Upload zip with all tests and with `run-test.sh` script to launch test execution at the root level of the package.
2. Upload .apk / .ipa
3. Create new test run with previously uploaded files

### Server-side with AltTester® Server running in a separate Windows VM, accessible by both tests and game build through IP.

Tested setup using AltTester® Server running in a Windows Azure VM. The conditions for the connection to work:
- the IP of the VM needs to be specified in `BaseTest.cs` when altDriver is instantiated.
- the game build needs to be instrumented with the same host IP.

### Server-side with AltTester® Server running in Bitbar Ubuntu VM (localhost)

The script which is executed on Bitbar VM needs to contain the installation and launching of the AltTester® Desktop build.

## Client-Side

Follow [Running Client-Side Appium tests](https://support.smartbear.com/bitbar/docs/en/mobile-app-tests/automated-testing/appium-support/running-client-side-appium-tests.html) to have an overview of the requirements.

1. Upload .apk / .ipa
2. Set & load `BITBAR_APIKEY` and `BITBAR_APP_ID_SDK_202` as environment variables
3. Prepare appium and bitbar specific capabilities in SetupAppium function from `BaseTest.cs`.
4. To be able to connect to AltTester® Server running in a separate Windows VM, accessible by both tests and game build through IP, set `HOST_ALT_SERVER` as environment variable.


! NOTE: Running client side tests with AltTester® Server running on same machine is failing even if using `SmartBear SecureTunnel`. We assume this is happening due to websocket implementation and incompatibility with AltTester® Server.