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

        // health check job -- no needed and skip
        // var healthCheckJob = JobBuilder
        //     .Create<HealthCheckJob>()
        //     .Build();
        // var healthCheckTrigger = TriggerBuilder
        //     .Create()
        //     .StartNow()
        //     .WithCronSchedule(HealthCheckJob.ScheduleJobCron)
        //     .Build();
        // await scheduler.ScheduleJob(healthCheckJob, healthCheckTrigger, cancellationToken);

        // certJob - scheduled job
        var certSync1 = JobBuilder
            .Create<CertSyncJob>()
            .Build();
        var certSyncTrigger1 = TriggerBuilder
            .Create()
            .WithCronSchedule(CertSyncJob.ScheduleJobCron)
            .Build();
        await scheduler.ScheduleJob(certSync1, certSyncTrigger1, cancellationToken);

        // certJob - start now job
        var certSync2 = JobBuilder
            .Create<CertSyncJob>()
            .Build();
        var certSyncTrigger2 = TriggerBuilder
            .Create()
            .StartNow()
            .Build();
        await scheduler.ScheduleJob(certSync2, certSyncTrigger2, cancellationToken);

        cancellationToken.WaitHandle.WaitOne();
    }
}