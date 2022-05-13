helm repo add bitnami https://charts.bitnami.com/bitnami
helm -n redis install redis bitnami/redis -f redis-values-standalone.yaml
https://www.teanote.pub/archives/347
https://artifacthub.io/packages/helm/bitnami/redis
https://docs.redis.com/latest/rs/references/client_references/client_csharp/

NAME: redis
LAST DEPLOYED: Fri May 13 13:50:09 2022
NAMESPACE: redis
STATUS: deployed
REVISION: 1
TEST SUITE: None
NOTES:
CHART NAME: redis
CHART VERSION: 16.9.3
APP VERSION: 6.2.7

** Please be patient while the chart is being deployed **

Redis&trade; can be accessed via port 6379 on the following DNS name from within your cluster:

    redis-master.redis.svc.cluster.local

To get your password run:

    export REDIS_PASSWORD=$(kubectl get secret --namespace redis redis -o jsonpath="{.data.redis-password}" | base64 --decode)

To connect to your Redis&trade; server:

1. Run a Redis&trade; pod that you can use as a client:

   kubectl run --namespace redis redis-client --restart='Never' --env REDIS_PASSWORD=$REDIS_PASSWORD --image docker.io/bitnami/redis:6.2.7-debian-10-r0 --command -- sleep infinity

   Use the following command to attach to the pod:

   kubectl exec --tty -i redis-client \
   --namespace redis -- bash

2. Connect using the Redis&trade; CLI:
   REDISCLI_AUTH="$REDIS_PASSWORD" redis-cli -h redis-master

To connect to your database from outside the cluster execute the following commands:

    kubectl port-forward --namespace redis svc/redis-master 6379:6379 &
    REDISCLI_AUTH="$REDIS_PASSWORD" redis-cli -h 127.0.0.1 -p 6379
