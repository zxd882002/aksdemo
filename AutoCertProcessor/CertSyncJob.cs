﻿using System.Text;
using k8s;
using k8s.Models;
using Quartz;

namespace AutoCertProcessor;

[DisallowConcurrentExecution]
public class CertSyncJob : IJob
{
    public const string ScheduleJobCron = "0 0 0 * * ?";
    private const string KubeConfigFileName = "demo.yaml";
    private const string CertKeyFileName = "zyxhome.top.key";
    private const string CertFileName = "zyxhome.top.cert";
    private const string SecretNameSpace = "istio-system";
    private const string SecretName = "istio-ingressgateway-cert";

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"[{DateTime.Now}] [Info] Start Cert Sync Job");

        try
        {
            string lastCert = File.Exists(CertFileName) ? await File.ReadAllTextAsync(CertFileName) : "";
            string key = await File.ReadAllTextAsync(CertKeyFileName);
            string cert = await GetCertAsync();

            if (String.IsNullOrWhiteSpace(cert))
                return;

            if (lastCert == cert)
            {
                Console.WriteLine($"[{DateTime.Now}] [Info] Cert is the same. Skip");
                return;
            }

            Kubernetes? client = GetKubernetesClient(KubeConfigFileName);
            if (client == null)
                return;

            V1Secret? certSecret = await GetCertSecretAsync(client, SecretNameSpace, SecretName);
            if (certSecret != null)
                await DeleteCertSecretAsync(client, SecretNameSpace, SecretName);

            await NewCertSecretAsync(client, SecretNameSpace, SecretName, key, cert);
            await File.WriteAllTextAsync(CertFileName, cert);

            Console.WriteLine($"[{DateTime.Now}] [Info] End Cert Sync Job");
        }
        catch (Exception e)
        {
            Console.WriteLine($"[{DateTime.Now}] [Error] Unhandled Exception");
            Console.WriteLine(e);
        }
    }

    private static async Task<string> GetCertAsync()
    {
        try
        {
            using var webClient = new HttpClient();
            var response = await webClient.GetAsync(
                "https://www.dynadot.com/letsencrypt/download_cert?key=J806N7f9A7V7AQ7W7D9G9G8P7c7K9U7t8t6Y746g656S7i&domain=zyxhome.top");
            var bodyStream = await response.Content.ReadAsStreamAsync();
            using StreamReader reader = new StreamReader(bodyStream, Encoding.ASCII);
            return await reader.ReadToEndAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[{DateTime.Now}] [Error] Error to get the cert");
            Console.WriteLine(e);
            return string.Empty;
        }
    }

    private Kubernetes? GetKubernetesClient(string kubeConfigFileName)
    {
        try
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(kubeConfigFileName);
            var client = new Kubernetes(config);
            return client;
        }
        catch (Exception e)
        {
            Console.WriteLine("[Error] Error to get the K8S client");
            Console.WriteLine(e);
            return null;
        }
    }

    private async Task<V1Secret?> GetCertSecretAsync(Kubernetes client, string certSecretNamespace, string name)
    {
        try
        {
            var secretList = await client.ListNamespacedSecretAsync(certSecretNamespace);
            return secretList.Items.FirstOrDefault(s => s.Name() == name);
        }
        catch (Exception e)
        {
            Console.WriteLine("[Error] Error to get the cert secret");
            Console.WriteLine(e);
            return null;
        }
    }

    private async Task DeleteCertSecretAsync(Kubernetes client, string certSecretNamespace, string name)
    {
        try
        {
            await client.DeleteNamespacedSecretAsync(name, certSecretNamespace);
            Console.WriteLine($"[{DateTime.Now}] [Info] The old cert is deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine($"[{DateTime.Now}] [Error] Error to delete the cert secret");
            Console.WriteLine(e);
        }
    }

    private async Task NewCertSecretAsync(
        Kubernetes client, string certSecretNamespace, string name, string key, string cert)
    {
        try
        {
            V1Secret secret = new()
            {
                Data = new Dictionary<string, byte[]>
                {
                    { "tls.crt", Encoding.ASCII.GetBytes(cert) },
                    { "tls.key", Encoding.ASCII.GetBytes(key) }
                },
                Kind = "Secret",
                Metadata = new V1ObjectMeta
                {
                    Name = name
                },
                Type = "kubernetes.io/tls"
            };
            await client.CreateNamespacedSecretAsync(secret, certSecretNamespace);
            Console.WriteLine($"[{DateTime.Now}] [Info] New cert secret is added");
        }
        catch (Exception e)
        {
            Console.WriteLine($"[{DateTime.Now}] [Error] Error to add the cert secret");
            Console.WriteLine(e);
        }
    }
}