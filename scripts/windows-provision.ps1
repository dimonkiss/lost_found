# Windows Provisioning Script for Lost & Found Web Platform
# This script sets up .NET 8, BaGet, and deploys the application

Write-Host "Starting Windows provisioning for Lost & Found Web Platform..." -ForegroundColor Green

# Set execution policy
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser -Force

# Install Chocolatey if not present
if (!(Get-Command choco -ErrorAction SilentlyContinue)) {
    Write-Host "Installing Chocolatey..." -ForegroundColor Yellow
    Set-ExecutionPolicy Bypass -Scope Process -Force
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
    iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
}

# Install .NET 8 SDK
Write-Host "Installing .NET 8 SDK..." -ForegroundColor Yellow
choco install dotnet-8.0-sdk -y

# Install Docker Desktop for BaGet
Write-Host "Installing Docker Desktop..." -ForegroundColor Yellow
choco install docker-desktop -y

# Install Git
Write-Host "Installing Git..." -ForegroundColor Yellow
choco install git -y

# Refresh environment variables
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

# Wait for Docker to be available
Write-Host "Waiting for Docker to start..." -ForegroundColor Yellow
Start-Sleep -Seconds 30

# Start BaGet using Docker
Write-Host "Starting BaGet NuGet server..." -ForegroundColor Yellow
docker run -d --name baget -p 5555:80 -v baget-data:/var/baget/packages -v baget-config:/var/baget/config loicsharma/baget:latest

# Wait for BaGet to start
Start-Sleep -Seconds 10

# Configure NuGet to use BaGet
Write-Host "Configuring NuGet to use BaGet..." -ForegroundColor Yellow
dotnet nuget add source http://localhost:5555/v3/index.json --name "BaGet" --username "admin" --password "NUGET-SERVER-API-KEY" --store-password-in-clear-text

# Create application directory
$appDir = "C:\LostFoundApp"
New-Item -ItemType Directory -Path $appDir -Force

# Copy application files (this would normally be done via shared folder or git clone)
Write-Host "Setting up application directory..." -ForegroundColor Yellow

# Create a simple test application to demonstrate the deployment
$testAppContent = @"
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapGet("/", () => "Lost & Found Web Platform - Windows Deployment Successful!");

app.Run();
"@

$testAppContent | Out-File -FilePath "$appDir\Program.cs" -Encoding UTF8

# Create project file
$projectContent = @"
<Project Sdk="Microsoft.NET.Sdk.Web">
    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
</Project>
"@

$projectContent | Out-File -FilePath "$appDir\LostFoundApp.csproj" -Encoding UTF8

# Build and run the application
Set-Location $appDir
Write-Host "Building and running Lost & Found application..." -ForegroundColor Yellow
dotnet build
dotnet run --urls "http://localhost:5000;https://localhost:5001" &

Write-Host "Windows provisioning completed successfully!" -ForegroundColor Green
Write-Host "Application is running at:" -ForegroundColor Cyan
Write-Host "  HTTP: http://localhost:5000" -ForegroundColor Cyan
Write-Host "  HTTPS: https://localhost:5001" -ForegroundColor Cyan
Write-Host "  BaGet: http://localhost:5555" -ForegroundColor Cyan
