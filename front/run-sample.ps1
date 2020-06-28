Write-Host "Setting environment variables";

$env:REACT_APP_NAME="K-CRM-APP-DEV"
$env:REACT_APP_API_URL="http://localhost:8080"

$env:REACT_APP_BASE_URL="http://localhost:4200"
$env:PORT=4200

$env:REACT_APP_USERNAME="root"
$env:REACT_APP_PASSWORD="123456"

Write-Host "Start project";

npm start