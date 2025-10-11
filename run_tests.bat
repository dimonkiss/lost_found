@echo off
echo Running Lost & Found Web Tests...
echo.

cd lost_found_web.Tests
dotnet test --verbosity normal --logger "console;verbosity=detailed"

echo.
echo Tests completed!
pause

