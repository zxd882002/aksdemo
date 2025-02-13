# 安装k8s步骤
1. OS CentoOS 7
2. login root@172.245.68.116 + password OxY96q02vJy5ZbGe4B
3. 更改root密码：`passwd`
4. 关闭防火墙：
   ``` bash
   systemctl disable firewalld
   systemctl stop firewalld
   ```
6. 更新 yum：`yum update -y`
7. 执行面的语句下载工具脚本ezdown：
    `export release=3.3.1`
    `wget https://github.com/easzlab/kubeasz/releases/download/${release}/ezdown`
    `chmod +x ./ezdown`
8. 使用工具脚本下载code：`./ezdown -D -m standard`
9. 运行kubeasz：`./ezdown -S`
10. 安装 aio 集群：`docker exec -it kubeasz ezctl start-aio`

# 验证安装
如果提示kubectl: command not found，退出重新ssh登录一下，环境变量生效即可:
  ``` bash
  kubectl version         # 验证集群版本
  kubectl get node        # 验证节点就绪 (Ready) 状态
  kubectl get pod -A      # 验证集群pod状态，默认已安装网络插件、coredns、metrics-server等
  kubectl get svc -A      # 验证集群服务状态
  ```

# 使用dashboard
1. 获取port：`kubectl get svc -n kube-system|grep dashboard` (如：32657)
2. 获取token：`kubectl describe -n kube-system secrets admin-user`
3. 访问 https://172.245.68.116:32657
4. 如果使用metalLB，nodePort将无法使用。可以改用kuboard。另外通过kuboard的service页也能访问dashboard

# 安装kubectl工具
- kubectl aliases
  1. 下载安装script：
   `wget https://raw.githubusercontent.com/ahmetb/kubectl-aliases/master/.kubectl_aliases`
  2. 把下面的语句放到.bashrc文件里   
   `[ -f ~/.kubectl_aliases ] && source ~/.kubectl_aliases`
  3. 重新登录
- ~~kubebox（没啥用）~~
  ~~1. 执行下面语句安装：~~
   ~~`curl -Lo kubebox https://github.com/astefanutti/kubebox/releases/download/v0.10.0/kubebox-linux && chmod +x kubebox`~~
- ~~kubeshell（不建议）~~
  ~~1. 安装python3和pip3：`yum install python3 -y`~~
  ~~2. 用pip3安装kube-shell：`pip3 install kube-shell`~~
- kubens/kubectx
  1. 执行下面的语句安装：      
   ``` bash
   sudo git clone https://github.com/ahmetb/kubectx /opt/kubectx
   sudo ln -s /opt/kubectx/kubectx /usr/local/bin/kubectx
   sudo ln -s /opt/kubectx/kubens /usr/local/bin/kubens
   ```
  2. 重新登录

# 安装istio步骤 
注：
1. 下面步骤已过期。可以参考https://istio.io/latest/zh/docs/setup/getting-started/#install
2. 但网页验证部分命令要改成：kubectl port-forward svc/bookinfo-gateway-istio 8080:80 **--address 192.168.88.200**

以下为老版步骤：
1. 下载安装demo, 并设置环境变量
  `curl -L https://istio.io/downloadIstio | sh -`
  `cd istio-1.16.1`
  `export PATH=$PWD/bin:$PATH`
2. 安装istio
  `istioctl install --set profile=demo -y`
3. 添加注入脚本
  `kubectl label namespace default istio-injection=enabled`
4. （可选）修改istio-ingressgateway为NodePort (或者安装metalLB)：
   `kubectl edit svc istio-ingressgateway -n istio-system`
5. 卸载：`istioctl uninstall -y --purge`
6. 去除namespace：`kubectl delete namespace istio-system`
7. 去除注入：`kubectl label namespace default istio-injection-`

# 安装metalLB
**注意：安装完metalLB之后，所有的nodeport功能将无法使用**
1. 安装metalLB：`kubectl apply -f https://raw.githubusercontent.com/metallb/metallb/v0.13.7/config/manifests/metallb-native.yaml`
2. 配置address pool：`vi address.yaml`
   ``` yaml
   apiVersion: metallb.io/v1beta1
   kind: IPAddressPool
   metadata:
     name: pool
     namespace: metallb-system
   spec:
     addresses:
     - 192.168.88.2/32
   ```
4. 配置L2：`vi l2.yaml`
   ``` yaml
   apiVersion: metallb.io/v1beta1
   kind: L2Advertisement
   metadata:
     name: example
     namespace: metallb-system
   spec:
    ipAddressPools:
    - pool
   ```
5. 部署 address pool：`kubectl apply -f address.yaml`
6. 部署 L2：`kubectl apply -f l2.yaml`
7. 把istio的ingress从node port改成LoadBalancer
8. 查看istio的LoadBalancer：`kubectl get service -A`。这时应该把本机ip分配上去了

# 安装book-info步骤
1. 安装book info：
   `kubectl apply -f samples/bookinfo/platform/kube/bookinfo.yaml`
2. 应该看到service和pod都能成功启动
   ``` bash
   $ kubectl get services
   NAME          TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)    AGE
   details       ClusterIP   10.0.0.212      <none>        9080/TCP   29s
   kubernetes    ClusterIP   10.0.0.1        <none>        443/TCP    25m
   productpage   ClusterIP   10.0.0.57       <none>        9080/TCP   28s
   ratings       ClusterIP   10.0.0.33       <none>        9080/TCP   29s
   reviews       ClusterIP   10.0.0.28       <none>        9080/TCP   29s

   $ kubectl get pods
   NAME                              READY   STATUS    RESTARTS   AGE
   details-v1-558b8b4b76-2llld       2/2     Running   0          2m41s
   productpage-v1-6987489c74-lpkgl   2/2     Running   0          2m40s
   ratings-v1-7dc98c7588-vzftc       2/2     Running   0          2m41s
   reviews-v1-7f99cc4496-gdxfn       2/2     Running   0          2m41s
   reviews-v2-7d79d5bd5d-8zzqd       2/2     Running   0          2m41s
   reviews-v3-7dbcdcbc56-m8dph       2/2     Running   0          2m41s

   $ kubectl exec "$(kubectl get pod -l app=ratings -o jsonpath='{.items[0].metadata.name}')" -c ratings -- curl -sS productpage:9080/productpage | grep -o "<title>.*</title>"
   <title>Simple Bookstore App</title>
   ```
3. 添加ingress-gateway：
   `kubectl apply -f samples/bookinfo/networking/bookinfo-gateway.yaml`
4. 找到external IP：
   ``` bash
   $ kubectl get svc istio-ingressgateway -n istio-system
   NAME                   TYPE           CLUSTER-IP       EXTERNAL-IP     PORT(S)                                      AGE
   istio-ingressgateway   NodePort       10.68.97.26     172.245.68.116          80:31380/TCP,443:31390/TCP,31400:31400/TCP   17h
   ```
5. 执行下面命令卸载：
   `samples/bookinfo/platform/kube/cleanup.sh`

# 安装istio工具
1. 执行下面命令：
   `kubectl apply -f samples/addons`
2. 可以通过kuboard的service页访问
3. 卸载tool：`kubectl delete -f samples/addons`

# 安装kuboard
1. 安装 `docker run -d --restart=unless-stopped --name=kuboard -p 18080:80/tcp -p 10081:10081/tcp -e KUBOARD_ENDPOINT="http://172.245.68.116:18080" -e KUBOARD_AGENT_SERVER_TCP_PORT="10081" -v /root/kuboard-data:/data eipwork/kuboard:v3`
2. 访问 http://172.245.68.116:18080，登录admin/Kuboard123
3. 使用 `cat $HOME/.kube/config` 拿到config，修改server地址。添加cluster

# 参考
https://github.com/easzlab/kubeasz
https://github.com/ahmetb/kubectl-aliases
https://github.com/astefanutti/kubebox
https://github.com/cloudnativelabs/kube-shell
https://github.com/ahmetb/kubectx
https://istio.io/latest/docs/setup/getting-started/
https://github.com/istio/istio/tree/release-1.16/samples/addons
https://metallb.universe.tf/installation/
https://ieevee.com/tech/2019/06/30/metallb.html
https://kuboard.cn/install/v3/install-built-in.html#%E9%83%A8%E7%BD%B2%E8%AE%A1%E5%88%92