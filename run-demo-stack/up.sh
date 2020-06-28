GIT_TAG=(`git log --pretty=format:'%h' -n 1`)
export APP_TAGS="$GIT_TAG"
export APP_PORT=8080
export DB_PORT=25432
echo "BUILD: $APP_TAGS"

docker-compose build --parallel
# printenv
docker-compose up