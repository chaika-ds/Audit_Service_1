Write-Host "Script for ELK data generation" -ForegroundColor Green
msbuild.exe 
Write-Host "AuditService.ELK.FillTestData project was built" -ForegroundColor Green
.\src\AuditService.ELK.FillTestData\bin\Debug\net6.0\AuditService.ELK.FillTestData.exe