# Deploy to BaGet Script
# This script pushes the NuGet package to the BaGet private repository

Write-Host "Deploying Lost & Found Web Platform to BaGet..." -ForegroundColor Green

# Set BaGet server URL
$BAGET_URL = "http://localhost:5555"
$API_KEY = "NUGET-SERVER-API-KEY"

# Find the latest package
$packagePath = Get-ChildItem -Path "packages" -Filter "*.nupkg" | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if ($packagePath) {
    Write-Host "Found package: $($packagePath.Name)" -ForegroundColor Yellow
    
    # Push to BaGet
    Write-Host "Pushing package to BaGet..." -ForegroundColor Yellow
    dotnet nuget push $packagePath.FullName --source $BAGET_URL --api-key $API_KEY
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Package deployed successfully to BaGet!" -ForegroundColor Green
        Write-Host "BaGet URL: $BAGET_URL" -ForegroundColor Cyan
    } else {
        Write-Host "Failed to deploy package to BaGet." -ForegroundColor Red
    }
} else {
    Write-Host "No package found in packages directory. Please run build-and-package.ps1 first." -ForegroundColor Red
}
