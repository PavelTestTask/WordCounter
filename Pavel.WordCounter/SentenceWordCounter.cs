using System;
using System.Collections.Generic;
using System.Linq;

namespace Pavel.WordCounter
{
    public class SentenceWordCounter : IWordCounter
    {
        private static readonly char[] SentenceEndPunctuation = { '.', '!', '?' };
        private static readonly char[] WordEndPunctuation = { ' ' };
        private static readonly char[] WordTrimPunctuation = { ',', '.', ';', '!', '?', '"', '\'', '(', ')', ' ', '-' };

        public IDictionary<string, int> CountWords(string s)
        {
            ValidateInput(s);

            var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (string word in s.Split(WordEndPunctuation, StringSplitOptions.RemoveEmptyEntries))
            {
                var trimmedWord = TrimPunctuation(word).ToLowerInvariant();

                if (string.IsNullOrWhiteSpace(trimmedWord))
                {
                    continue;
                }

                int originalValue;
                if (result.TryGetValue(trimmedWord, out originalValue))
                {
                    result[trimmedWord] = ++originalValue;
                }
                else
                {
                    result[trimmedWord] = 1;
                }
            }

            return result;
        }

        private bool IsSentence(string text)
        {
            var sentanceCount = text
                .TrimEnd(SentenceEndPunctuation)
                .Count(x => SentenceEndPunctuation.Contains(x));

            return sentanceCount == 0;
        }

        private string TrimPunctuation(string word)
        {
            return word.Trim(WordTrimPunctuation);
        }

        private void ValidateInput(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Input string should not be empty.", nameof(s));
            }

            if (!IsSentence(s))
            {
                throw new ArgumentException("Provided string is not a single sentence.", nameof(s));
            }
        }
    }
}