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
        Console.WriteLine("Schedule started");

        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Canceling from docker...");
                scheduler.Shutdown(true, cancellationToken).Wait(cancellationToken);
                cancellationTokenSource.Cancel();
                Console.WriteLine("canneled");
            }
        };

        Console.CancelKeyPress += (_, _) =>
        {
            Console.WriteLine("Canceling...");
            scheduler.Shutdown(true, cancellationToken).Wait(cancellationToken);
            cancellationTokenSource.Cancel();
            Console.WriteLine("canneled");
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
            .Build();
        await scheduler.ScheduleJob(certSync, certSyncTrigger, cancellationToken);

        cancellationToken.WaitHandle.WaitOne();
    }
}