kubectl create -n istio-system secret tls istio-ingressgateway-cert --key=zyxhome.top.key --cert=zyxhome.top.cert

kubectl delete secret istio-ingressgateway-cert -n istio-system 