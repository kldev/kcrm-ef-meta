CONTEXT="kube-k3s"
NAMESPACE="crm"

kubectx $CONTEXT
kubens $NAMESPACE

kubectl create configmap front-nginx-conf --from-file ./default.conf