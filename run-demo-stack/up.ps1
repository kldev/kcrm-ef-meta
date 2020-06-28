$GIT_TAG = git log --pretty=format:'%h' -n 1

$env:APP_TAGS = $GIT_TAG
$env:APP_PORT = 8080
$env:DB_PORT = 25432
Write-Host "BUILD:" $GIT_TAG

docker-compose build --parallel
docker-compose up