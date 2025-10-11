# Build and Package Script for Lost & Found Web Platform
# This script builds the application and creates NuGet packages

Write-Host "Building and packaging Lost & Found Web Platform..." -ForegroundColor Green

# Navigate to the project directory
Set-Location "lost_found_web"

# Clean previous builds
Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
dotnet clean

# Restore dependencies
Write-Host "Restoring dependencies..." -ForegroundColor Yellow
dotnet restore

# Build the project
Write-Host "Building the project..." -ForegroundColor Yellow
dotnet build --configuration Release

# Create NuGet package
Write-Host "Creating NuGet package..." -ForegroundColor Yellow
dotnet pack --configuration Release --output ../packages

# Create deployment package
Write-Host "Creating deployment package..." -ForegroundColor Yellow
dotnet publish --configuration Release --output ../deploy --self-contained true --runtime win-x64

Write-Host "Build and packaging completed successfully!" -ForegroundColor Green
Write-Host "Packages created in: ../packages" -ForegroundColor Cyan
Write-Host "Deployment files created in: ../deploy" -ForegroundColor Cyan
