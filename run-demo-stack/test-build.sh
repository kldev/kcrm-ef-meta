GIT_TAG=(`git log --pretty=format:'%h' -n 1`)
export APP_TAGS="$GIT_TAG"
export APP_PORT=80
export DB_PORT=
export REACT_APP_GIT_SHA="$GIT_TAG"
echo "BUILD: $APP_TAGS"

docker-compose build
