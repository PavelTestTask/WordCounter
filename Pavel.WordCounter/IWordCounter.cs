using System.Collections.Generic;

namespace Pavel.WordCounter
{
    public interface IWordCounter
    {
        IDictionary<string, int> CountWords(string s);
    }
}