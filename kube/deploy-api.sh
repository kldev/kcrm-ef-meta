if [ -z "$VERSION" ]; then
	echo "VERSION env variable is not set, cannot continue"
    exit -1
fi 

function exitOnError() {
    if [ $? -ne 0 ]; then
        echo "We have error"
        exit $?
    fi
}

NOBUILD=false
NODEPLOY=false
DRY_RUN=""

for ARG in "$@"
	do
		if [ "$ARG" = "--no-build" ]; then
			NOBUILD=true
		fi

		if [ "$ARG" = "--no-deploy" ]; then
			NODEPLOY=true
		fi

        if [ "$ARG" = "--dry-run" ]; then
			DRY_RUN="--dry-run"
		fi
done

GREEN='\033[0;32m'
REGISTRY="docker.local.lab.pl"

DEPLOY=""
BUILD=""

CONTEXT="kube-k3s"
NAMESPACE="crm"
DOCKER_NAME="k-crm/api"

RELEASE="k-crm"

buildAndPush() {
    echo -e "${GREEN} build docker image"
    docker build -t $DOCKER_NAME:$VERSION  ../KCrm
    exitOnError
    echo -e "${GREEN} build docker success"
    echo -e "${GREEN} build tag and push $REGISTRY/$DOCKER_NAME:$VERSION"

    docker tag $DOCKER_NAME:$VERSION  $REGISTRY/$DOCKER_NAME:$VERSION 
    docker push $REGISTRY/$DOCKER_NAME:$VERSION
    exitOnError
}

helmInstall() {
    echo -e "${GREEN} deploy on kube"    
    
    helm upgrade $RELEASE ../charts/k-crm-api -f values-api-lab.yaml \
         --set image.repository=$REGISTRY/$DOCKER_NAME \
         --set image.tag=$VERSION \
         --description "$RELEASE-$VERSION" \
         --create-namespace --namespace ${NAMESPACE} $DRY_RUN --install
}

echo -e "Switch ctx to $CONTEXT"
kubectx $CONTEXT
kubens $NAMESPACE
exitOnError


if [ "$NOBUILD" = false ]; then
    buildAndPush
fi 

if [ "$NODEPLOY" = false ]; then
    helmInstall
fi 
