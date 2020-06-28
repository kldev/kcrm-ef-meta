$GIT_TAG = git log --pretty=format:'%h' -n 1

$env:APP_TAGS = $GIT_TAG
$env:APP_PORT = 8080
$env:DB_PORT =
Write-Host "Deploy up:" $GIT_TAG

docker-compose build --parallel
docker stack deploy -c docker-compose.yml -c docker-compose.deploy.yml test