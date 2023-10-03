#!/bin/bash

# Install dotnet
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x ./dotnet-install.sh
./dotnet-install.sh --channel 7.0

echo "Set dotnet in PATH so it can be used to execute tests"
export PATH="$PATH:$HOME/.dotnet"

##### Cloud testrun dependencies start
echo "Extracting tests.zip..."
unzip tests.zip

## Environment variables setup
echo "UDID set to ${IOS_UDID}"
export APPIUM_PORT="4723"
export APPIUM_AUTOMATION="XCUITest"
export APPIUM_APPFILE="$PWD/TrashCat.ipa"

## Appium server launch
echo "Starting Appium ..."
appium -U ${IOS_UDID} --log-no-colors --log-timestamp --command-timeout 120

## Run the test:
echo "Running tests"
mkdir TestResults
dotnet test TestAlttrashCSharp.csproj --logger:junit --filter=MainMenuTests

echo "==> Collect reports"
mv TestResults/TestResults.xml TEST-all.xml