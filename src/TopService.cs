using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace top_words
{
    public class TopWordService
    {
        private readonly string _filePath = "text.txt";

        public async Task<ScanResult> GetTopWordsAsync(CancellationToken token = default)
        {
            var results = new ConcurrentDictionary<string, long>();

            using var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs);

            var watch = new Stopwatch();
            watch.Start();
            var linesCount = 0;
            while (!sr.EndOfStream || !token.IsCancellationRequested)
            {
                var line = await sr.ReadLineAsync();

                if (line == null)
                {
                    watch.Stop();
                    break;
                }

                linesCount++;

                var words = line
                    .Split(' ')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim());

                foreach (var word in words)
                {
                    results.AddOrUpdate(word, s => 1, (s, l) => l + 1);
                }
            }

            return new ScanResult
            {
                Statistic = results,
                FileName = _filePath,
                TotalMilliseconds = watch.ElapsedMilliseconds,
                TotalRows = linesCount
            };
        }
    }
}