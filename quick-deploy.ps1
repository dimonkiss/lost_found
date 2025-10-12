# Quick Deploy Script for Lost & Found Web Platform
# This script provides a simple way to deploy the application

param(
    [Parameter(Mandatory=$false)]
    [ValidateSet("windows", "linux", "macos", "all")]
    [string]$Target = "all",
    
    [Parameter(Mandatory=$false)]
    [switch]$Build,
    
    [Parameter(Mandatory=$false)]
    [switch]$Deploy
)

Write-Host "Lost & Found Web Platform - Quick Deploy Script" -ForegroundColor Green
Write-Host "=================================================" -ForegroundColor Green

if ($Build) {
    Write-Host "Building and packaging application..." -ForegroundColor Yellow
    .\scripts\build-and-package.ps1
    
    if ($Deploy) {
        Write-Host "Deploying to BaGet..." -ForegroundColor Yellow
        .\scripts\deploy-to-baget.ps1
    }
}

Write-Host "Starting Vagrant deployment..." -ForegroundColor Yellow

switch ($Target) {
    "windows" {
        Write-Host "Deploying to Windows..." -ForegroundColor Cyan
        vagrant up windows
    }
    "linux" {
        Write-Host "Deploying to Linux..." -ForegroundColor Cyan
        vagrant up linux
    }
    "macos" {
        Write-Host "Deploying to macOS..." -ForegroundColor Cyan
        vagrant up macos
    }
    "all" {
        Write-Host "Deploying to all operating systems..." -ForegroundColor Cyan
        vagrant up
    }
}

Write-Host "Deployment completed!" -ForegroundColor Green
Write-Host ""
Write-Host "Access your applications at:" -ForegroundColor Cyan
Write-Host "Windows:  http://192.168.56.10:5000" -ForegroundColor White
Write-Host "Linux:    http://192.168.56.11:8080" -ForegroundColor White
Write-Host "macOS:    http://192.168.56.12:5003" -ForegroundColor White
Write-Host ""
Write-Host "BaGet repositories:" -ForegroundColor Cyan
Write-Host "Windows:  http://192.168.56.10:5555" -ForegroundColor White
Write-Host "Linux:    http://192.168.56.11:5556" -ForegroundColor White
Write-Host "macOS:    http://192.168.56.12:5557" -ForegroundColor White
