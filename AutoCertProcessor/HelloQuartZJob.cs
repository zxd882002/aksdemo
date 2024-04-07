using Quartz;

namespace AutoCertProcessor;

public class HelloQuartZJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Task.Factory.StartNew(() => { Console.WriteLine("Hello Quartz.Net"); });
    }
}