using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pavel.WordCounter.Tests.Utils;
using TechTalk.SpecFlow;

namespace Pavel.WordCounter.Tests
{
    [Binding]
    public class WordCounterSteps
    {
        private const string ExceptionContextKey = "ExceptionContextKey";

        private readonly IWordCounter _wordCounter = new SentenceWordCounter();

        private string _input;

        private IDictionary<string, int> _result;

        [Given(@"a sentence '(.*)'")]
        public void GivenASentence(string s)
        {
            _input = s;
        }

        [When(@"the program is run")]
        public void WhenTheProgramIsRun()
        {
            try
            {
                _result = _wordCounter.CountWords(_input);
            }
            catch (ArgumentException ex)
            {
                ScenarioContext.Current.Add(ExceptionContextKey, ex);
            }
        }

        [Then(@"I'm returned a distinct list of words in the sentence and the number of times they have occurred")]
        public void ThenIMReturnedADistinctListOfWordsInTheSentence(Table expectedTable)
        {
            var expected = expectedTable.ToDictionary();
            Assert.AreEqual(expected.Count, _result.Count);
            foreach (KeyValuePair<string, int> expectedPair in expected)
            {
                Assert.AreEqual(expectedPair.Value, _result[expectedPair.Key]);
            }
        }

        [Then(@"multiple sentences are not allowed")]
        public void ThenMultipleSentencesAreNotAllowed()
        {
            object o;
            if (ScenarioContext.Current.TryGetValue(ExceptionContextKey, out o))
            {
                var ex = o as ArgumentException;
                Assert.IsNotNull(ex);
                Assert.IsTrue(ex.Message.Contains("is not a single sentence"));
            }
            else
            {
                Assert.Fail("Expected exception was not thrown.");
            }        
        }

        [Then(@"empty input is not allowed")]
        public void ThenEmptyInputIsNotAllowed()
        {
            object o;
            if (ScenarioContext.Current.TryGetValue(ExceptionContextKey, out o))
            {
                var ex = o as ArgumentException;
                Assert.IsNotNull(ex);
                Assert.IsTrue(ex.Message.Contains("should not be empty"));
            }
            else
            {
                Assert.Fail("Expected exception was not thrown.");
            }
        }
    }
}
