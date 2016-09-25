using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Pavel.WordCounter.Tests.Utils
{
    internal static class TableExtensions
    {
        public static Dictionary<string, int> ToDictionary(this Table table)
        {
            return table.Rows.ToDictionary(row => row[0], row => Convert.ToInt32(row[1]));
        }
    }
}
