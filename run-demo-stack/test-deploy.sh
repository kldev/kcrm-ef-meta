GIT_TAG=(`git log --pretty=format:'%h' -n 1`)
export APP_TAGS="$GIT_TAG"
export APP_PORT=80
export DB_PORT=
echo "BUILD: $APP_TAGS"
export REACT_APP_GIT_SHA="$GIT_TAG"

#docker-compose build --parallel
docker stack deploy -c docker-compose.yml -c docker-compose.deploy.yml test
