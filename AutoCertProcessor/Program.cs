using System.Net;
using System.Text;
using k8s;
using k8s.Models;
using Quartz;
using Quartz.Impl;

namespace AutoCertProcessor;

class Program
{
    private const string ScheduleJobCron = "*/1 * * * * ?"; 
    
    
    
    static async Task Main()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        var schedulerFactory = new StdSchedulerFactory();
        var scheduler = await schedulerFactory.GetScheduler();
        await scheduler.Start();
        Console.WriteLine("Schedule started");

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Canceling from docker...");
                scheduler.Shutdown(true, cancellationToken).Wait(cancellationToken);
                cancellationTokenSource.Cancel();
                Console.WriteLine("canneled");
            }
        };

        Console.CancelKeyPress += (s, e) =>
        {
            Console.WriteLine("Canceling...");
            scheduler.Shutdown(true, cancellationToken).Wait(cancellationToken);
            cancellationTokenSource.Cancel();
            Console.WriteLine("canneled");
        };

        // create job and trigger
        var jobDetail = JobBuilder.Create<HelloQuartZJob>().Build();
        var trigger = TriggerBuilder.Create()
            .WithCronSchedule(ScheduleJobCron)
            .Build();

        // append to the scheduler
        await scheduler.ScheduleJob(jobDetail, trigger);
        cancellationToken.WaitHandle.WaitOne();

        // string content = "";
        // using (var webClient = new WebClient())
        // {
        //     var stream = webClient.OpenRead(
        //         "https://www.dynadot.com/letsencrypt/download_cert?key=J806N7f9A7V7AQ7W7D9G9G8P7c7K9U7t8t6Y746g656S7i&domain=zyxhome.top");
        //
        //     using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
        //     {
        //         content = reader.ReadToEnd();
        //     }
        // }
        //
        // string key = File.ReadAllText("zyxhome.top.key");
        // string cert = content;
        //
        // var config = KubernetesClientConfiguration.BuildConfigFromConfigFile("demo.yaml");
        // var client = new Kubernetes(config);
        //
        // var secretList = client.ListNamespacedSecret("istio-system");
        // foreach (var v1Secret in secretList)
        // {
        //     Console.WriteLine(v1Secret.Name());
        // }
        //
        // var secret = new V1Secret
        // {
        //     Data = new Dictionary<string, byte[]>
        //     {
        //         { "ca-cert.pem", Encoding.ASCII.GetBytes(cert) },
        //         { "ca-key.pem", Encoding.ASCII.GetBytes(key) }
        //     },
        //     Kind = "Secret",
        //     Metadata = new V1ObjectMeta
        //     {
        //         Name = "test-secret"
        //     }
        // };
        // client.CreateNamespacedSecret(secret, "istio-system");
        // Console.WriteLine("added");
        //
        // client.DeleteNamespacedSecret("test-secret", "istio-system");
        // Console.WriteLine("deleted");
    }
}