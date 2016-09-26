using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pavel.WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No sentence provided. Please, enter sentence as a parameter.");
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("Too many parameters provided. Please, enter input sentence as an only parameter.");
            }
            else
            {
                IWordCounter wordCounter = new SentenceWordCounter();

                try
                {
                    var words = wordCounter.CountWords(args[0]);

                    Console.WriteLine("Words from sentence:");

                    foreach (KeyValuePair<string, int> keyValuePair in words)
                    {
                        Console.WriteLine("{0} - {1}", keyValuePair.Key, keyValuePair.Value);
                    }
                }
                catch (Exception ex)
                {
                    //There is not much we could do here, but Let's be gentle, and don't crash the program by rethrow.

                    //Let's try to catch all and help user to understand what happened.
                    Console.WriteLine("Exception has occurred:");
                    Console.WriteLine(ex.Message);

                    //Log the exception to help ourselves. 
                    Debug.WriteLine(ex);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Please, press any key to exit.");
            Console.ReadKey();
        }
    }
}
