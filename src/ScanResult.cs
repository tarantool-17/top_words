using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace top_words
{
    public class ScanResult
    {
        private static int TopWordsCountToShow = 20; 
        
        public string FileName { get; set; }
        public ConcurrentDictionary<string, long> Statistic { get; set; }
        public long TotalWords => Statistic?.Keys.Count ?? 0;
        public long TotalRows { get; set; }
        public long TotalMilliseconds { get; set; }

        public override string ToString()
        {
            var results = Statistic?.OrderByDescending(x => x.Value).Take(TopWordsCountToShow);
            var top = string.Join($"{Environment.NewLine}    ", results ?? new Dictionary<string, long>());
            
            return $"File: {FileName} {Environment.NewLine}" +
                   $"Top {TopWordsCountToShow} words: {top} {Environment.NewLine}" +
                   $"Total rows: {TotalRows} {Environment.NewLine}" +
                   $"Total words: {TotalWords} {Environment.NewLine}" +
                   $"Total spent (ms): {TotalMilliseconds}";
        }
    }
}