### 必须使用kerel是generic的image。否则一定会失败！

使用 `uname -r`命令看kernel版本

### 使用su账号

`sudo su`

### 改su密码

`passwd`

`JamaicaQA1!`

### 安装qemu

`apt install qemu-guest-agent`

关机

`poweroff`

而后在pve ->选项里enable qemu guest agent

重新开机，此时应该看到有ip了

用root账号登录

### 更新apt

`apt update`

`apt upgrade`

### 如果没有安装的话，安装vim

`sudo apt install vim`

### 开启ssh

1. `apt-get install openssh-server`
2. `service ssh status`
3. 跳转到ssh目录：`cd /etc/ssh`
4. 更新 `ssh_config`文件（使用sudo）
   1. 去掉注释 ``PasswordAuthentication yes``
5. 更新 `sshd_config`文件（使用sudo）
   1. 把 `PasswordAuthentication no`改成 `PasswordAuthentication yes`
   2. 把 `PermitRootLogin Prohibit-password`改成 `PermitRootLogin yes`
6. 重启 `reboot`

---



### 对每个机器都要做如下的操作

### 修改机器名

`sudo hostnamectl set-hostname new-hostname`

### 修改静态IP

1. 跳转到netplan目录：`cd /etc/netplan`
2. 备份netplan文件：`sudo cp 50-xxxx 50-xxxx.bak`
3. 修改原始文件

```yaml
network:
    version: 2
    ethernets:
        eth0:
            addresses: [192.168.88.200/24] // 不同的机器的ip地址不一样
            nameservers:
                addresses: [192.168.88.2]
            routes:
                - to: default
                  via: 192.168.88.2
```

4. 让配置生效：`sudo netplan try`

### 修改hosts

1. 打开hosts `sudo /etc/hosts`
2. 修改master和node的hosts
3. 所有机器添加所有机器的 ip+host

### 开启root账号密码

1. 打开root账号：`sudo su`
2. 修改密码：`passwd`

### 对master和node，允许root登录

`sudo sed -i "/PermitRootLogin/d" /etc/ssh/sshd_config`
`sudo sh -c "echo 'PermitRootLogin yes' >> /etc/ssh/sshd_config"`
`sudo sed -i "/StrictHostKeyChecking/s/^#//; /StrictHostKeyChecking/s/ask/no/" /etc/ssh/ssh_config`
`sudo systemctl restart sshd`

### 重起机器

重起: `sudo reboot`
关机: `sudo poweroff`

### 修复ubuntu /etc/resolv.conf namespace是127.0.0.53的问题所导致的coredns失败

参考：https://dyrnq.com/ubuntu-update-etc-resolve-conf/
使用如下命令修改dns

```bash
mkdir -p /etc/systemd/resolved.conf.d/
cat >/etc/systemd/resolved.conf.d/99-dns.conf << EOF
[Resolve]
DNS=114.114.114.114 8.8.8.8
EOF
ln -s -f /run/systemd/resolve/resolv.conf /etc/resolv.conf
systemctl daemon-reload && systemctl restart systemd-resolved.service && systemctl status -l systemd-resolved.service --no-pager
cat /etc/resolv.conf
```
