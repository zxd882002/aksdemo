create:
cd k8s/cert
kubectl create -n istio-system secret tls istio-ingressgateway-cert --key=zyxhome.top.exp20230418.key --cert=zyxhome.top.exp20230418.cert

delete:
kubectl delete secret istio-ingressgateway-cert -n istio-system