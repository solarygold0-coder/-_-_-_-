# PowerShell script for building a Windows installer using WiX.
# Run this from a Windows machine with WiX Toolset installed.

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$appProject = Join-Path $root "TransactionManagementSystem.csproj"
$installerProject = Join-Path $PSScriptRoot "TransactionManagementSystemSetup.wixproj"
$buildDir = Join-Path $root "bin\Release\net6.0-windows"

Write-Host "Building the WPF application..."
& dotnet build $appProject -c Release -p:UseWPF=true

if (-not (Test-Path $buildDir)) {
    throw "The application output folder was not created: $buildDir"
}

Write-Host "Building the MSI installer..."
& dotnet build $installerProject -c Release

Write-Host "Done. MSI file is inside the Installer/bin/Release folder."
