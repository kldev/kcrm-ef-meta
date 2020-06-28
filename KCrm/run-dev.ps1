$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ASPNETCORE_URLS = "http://localhost:8080"

Write-Host "$ASPNETCORE_URLS"

dotnet build && dotnet run -p KCrm.Server.Api --no-launch-profile