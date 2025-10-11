Write-Host "Running Lost & Found Web Tests..." -ForegroundColor Green
Write-Host ""

Set-Location "lost_found_web.Tests"
dotnet test --verbosity normal --logger "console;verbosity=detailed"

Write-Host ""
Write-Host "Tests completed!" -ForegroundColor Green
Read-Host "Press Enter to continue"

