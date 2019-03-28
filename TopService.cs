using System;
using System.Threading;
using System.Threading.Tasks;

public class TopService
{
    public Task StartAsync(CancellationToken token)
    {
        return Task.Factory.StartNew(() =>
        {        
            while(true)
            {
                Console.WriteLine(DateTime.UtcNow.Ticks);
                Thread.Sleep(1000);
            }
        }, token);
    }
}