#!/bin/bash

# Install dotnet
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x ./dotnet-install.sh
./dotnet-install.sh --channel 7.0

echo "Set dotnet in PATH so it can be used to execute tests"
export PATH="$PATH:$HOME/.dotnet"

echo "==> Setup ADB port reverse..."
adb reverse --remove-all
adb reverse tcp:13000 tcp:13000

##### Cloud testrun dependencies start
echo "Extracting tests.zip..."
unzip tests.zip

## Environment variables setup
export PLATFORM_NAME="Android"
export UDID=${ANDROID_SERIAL}
export APPIUM_PORT="4723"

APILEVEL=$(adb shell getprop ro.build.version.sdk)
APILEVEL="${APILEVEL//[$'\t\r\n']}"
export PLATFORM_VERSION=${APILEVEL}
echo "API level is: ${APILEVEL}"

if [ "$APILEVEL" -gt "19" ]; then
	export AUTOMATION_NAME="UiAutomator2"
	echo "UiAutomator2"
else
	export AUTOMATION_NAME="UiAutomator1"
	echo "UiAutomator1"
fi

TEST=${TEST:="SampleAppTest"}


## Appium server launch
echo "Starting Appium ..."
appium --log-no-colors --log-timestamp

export APPIUM_APPFILE=$PWD/application.apk # App file is at current working folder
export LICENSE_KEY=$(cat license.txt)

# Install and launch AltTester Desktop
brew install wget
wget https://alttester.com/app/uploads/AltTester/desktop/AltTesterDesktopLinuxBatchmode.zip
unzip AltTesterDesktopLinuxBatchmode.zip
cd AltTesterDesktopLinux

# Start AltTester Desktop from batchmode
echo "Starting AltTester Desktop ..."

chmod +x ./AltTesterDesktop.x86_64
./AltTesterDesktop.x86_64 -batchmode -port 13000 -license $LICENSE_KEY -nographics -termsAndConditionsAccepted &
cd ..

## Run the test:
echo "Running tests"
mkdir TestResults
dotnet test TestAlttrashCSharp.csproj --logger:junit --filter=MainMenuTests

echo "==> Collect reports"
mv TestResults/TestResults.xml TEST-all.xml

echo "Deactivate AltTesterDesktop license"
cd AltTesterDesktopLinux
kill -2 `ps -ef | awk '/AltTesterDesktop.x86_64/{print $2}'`
sleep 10
./AltTesterDesktop.x86_64 -batchmode -nographics -removeActivation