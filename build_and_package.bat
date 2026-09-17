@echo off
setlocal

:: ==== CONFIGURATION ====
set PROJECT_NAME=QuickCrossbowReload
set VERSION=1.0.0
set FRAMEWORK=netstandard2.1
set BUILD_CONFIG=Release

:: Path to compiled DLL
set DLL_PATH=bin\%BUILD_CONFIG%\%FRAMEWORK%\%PROJECT_NAME%.dll

:: Thunderstore Plugin destination
set TMM_PLUGIN_PATH=%APPDATA%\Thunderstore Mod Manager\DataFolder\Valheim\profiles\Default\BepInEx\plugins

:: Paths for packaging
set OUTPUT_ZIP=%PROJECT_NAME%-%VERSION%-valheim.zip
set PACKAGE_TEMP=_PackageTemp

:: ==== CLEAN & BUILD ====
echo Cleaning project...
dotnet clean

echo Building project (%BUILD_CONFIG%)...
dotnet build -c %BUILD_CONFIG%
if NOT EXIST "%DLL_PATH%" (
    echo ERROR: Build failed. DLL not found at %DLL_PATH%
    pause
    exit /b 1
)

:: ==== COPY DLL TO THUNDERSTORE PLUGIN PATH ====
echo Copying DLL to Thunderstore Mod Manager plugin folder...
xcopy /Y "%DLL_PATH%" "%TMM_PLUGIN_PATH%" >nul

:: ==== CREATE ZIP PACKAGE ====
echo Preparing zip package...
rmdir /S /Q "%PACKAGE_TEMP%" >nul 2>&1
mkdir "%PACKAGE_TEMP%"
copy /Y "%DLL_PATH%" "%PACKAGE_TEMP%\" >nul
copy /Y "manifest.json" "%PACKAGE_TEMP%\" >nul
copy /Y "icon.png" "%PACKAGE_TEMP%\" >nul
copy /Y "README.md" "%PACKAGE_TEMP%\" >nul

echo Creating %OUTPUT_ZIP%...
powershell -Command "Compress-Archive -Path '%PACKAGE_TEMP%\*' -DestinationPath '%OUTPUT_ZIP%'" >nul

:: ==== CLEANUP ====
rmdir /S /Q "%PACKAGE_TEMP%" >nul

echo Done. Mod installed and packaged as %OUTPUT_ZIP%
pause
