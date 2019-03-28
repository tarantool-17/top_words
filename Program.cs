using System;
using System.Threading;
using System.Threading.Tasks;

namespace top_words
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancelTokenSource = new CancellationTokenSource();
            var token = cancelTokenSource.Token;

            Console.WriteLine("Started.");

            var serviceTask = new TopService().StartAsync(token);
            var consoleTask = WaitConsoleAsync();

            var result = Task.WhenAny(serviceTask, consoleTask).GetAwaiter().GetResult();
            if(result == consoleTask)
            {
                Console.WriteLine("Cancelled by user.");
                cancelTokenSource.Cancel();
            }
        }

        public static async Task WaitConsoleAsync()
        {
            var s = "";
            while(s != "q")
            {
                Console.WriteLine("For exit click 'q'. Continued.");
                s = Console.ReadLine();
            }
        }
    }
}
