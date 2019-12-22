using System;
using System.Threading;
using System.Threading.Tasks;

namespace top_words
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cancelTokenSource = new CancellationTokenSource();
            var token = cancelTokenSource.Token;

            Console.WriteLine("Started.");

            var serviceTask = new TopWordService().GetTopWordsAsync(token);
            var consoleTask = WaitConsoleAsync();

            var result = await Task.WhenAny(serviceTask, consoleTask);
            
            if(result == consoleTask)
            {
                Console.WriteLine("Cancelled by user.");
                cancelTokenSource.Cancel();
            }
            else
            {
                var topWords = await serviceTask;
                Console.WriteLine(topWords);
            }
        }

        public static Task WaitConsoleAsync()
        {
            return Task.Run(() =>
            {
                while (Console.ReadLine() != "q")
                {
                    Console.WriteLine("For exit click 'q'. Continued.");
                }
            });
        }
    }
}
