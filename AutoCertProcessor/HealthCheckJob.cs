using Quartz;

namespace AutoCertProcessor;

public class HealthCheckJob : IJob
{
    public const string ScheduleJobCron = "0 0 */1 * * ?"; 

    public Task Execute(IJobExecutionContext context)
    {
        return Task.Factory.StartNew(() => { Console.WriteLine($"[{DateTime.Now}] [Info] I am healthy"); });
    }
}