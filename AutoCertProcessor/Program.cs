using Quartz;
using Quartz.Impl;

namespace AutoCertProcessor;

class Program
{
    static async Task Main()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        var schedulerFactory = new StdSchedulerFactory();
        var scheduler = await schedulerFactory.GetScheduler();
        await scheduler.Start(cancellationToken);
        Console.WriteLine($"[{DateTime.Now}] [Info] Schedule started");

        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"[{DateTime.Now}] [Info] Canceling from docker...");
                scheduler.Shutdown(true, cancellationToken).Wait(cancellationToken);
                cancellationTokenSource.Cancel();
                Console.WriteLine($"[{DateTime.Now}] [Info] Canneled");
            }
        };

        Console.CancelKeyPress += (_, _) =>
        {
            Console.WriteLine($"[{DateTime.Now}] [Info] Canceling...");
            scheduler.Shutdown(true, cancellationToken).Wait(cancellationToken);
            cancellationTokenSource.Cancel();
            Console.WriteLine($"[{DateTime.Now}] [Info] Canneled");
        };

        // health check job
        var healthCheckJob = JobBuilder.Create<HealthCheckJob>().Build();
        var healthCheckTrigger = TriggerBuilder.Create().WithCronSchedule(HealthCheckJob.ScheduleJobCron).Build();
        await scheduler.ScheduleJob(healthCheckJob, healthCheckTrigger, cancellationToken);

        // certJob
        var certSync = JobBuilder
            .Create<CertSyncJob>()
            .Build();
        var certSyncTrigger = TriggerBuilder
            .Create()
            .WithCronSchedule(CertSyncJob.ScheduleJobCron)
            //.WithCronSchedule("* * * * * ?")
            .Build();
        await scheduler.ScheduleJob(certSync, certSyncTrigger, cancellationToken);

        cancellationToken.WaitHandle.WaitOne();
    }
}