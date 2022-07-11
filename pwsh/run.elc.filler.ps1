Write-Host "Script for ELK data generation" -ForegroundColor Green
[Environment]::SetEnvironmentVariable("Path", "%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin", "User")
msbuild.exe 
Write-Host "AuditService.ELK.FillTestData project was built" -ForegroundColor Green
.\src\AuditService.ELK.FillTestData\bin\Debug\net6.0\AuditService.ELK.FillTestData.exe