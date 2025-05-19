// A utility to analyze text files and provide statistics

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace Prn212_HW.HW2
{
    class Program
    {
        public static void FileAnalyzer(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }

            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");

                // Read the file content
                string content = File.ReadAllText(filePath);

                // TODO: Implement analysis functionality
                // 1. Count words
                string[] wordList = content.Split(' ');
                int wordCount = wordList.Length;
                Console.WriteLine($"number of words: {wordCount}");
                // 2. Count characters (with and without whitespace)
                int characterCountWithSpace = content.Length;
                //count only the character which is not white space
                int characterCountWithoutSpace = content.Count(c => !char.IsWhiteSpace(c));
                Console.WriteLine($"number of character with white space: {characterCountWithSpace}");
                Console.WriteLine($"number of character with out white space: {characterCountWithoutSpace}");
                // 3. Count sentences
                string[] sentenceArray = content.Split('.');
                int sentenceCount = sentenceArray.Length;
                Console.WriteLine($"number of sentences: {sentenceCount}");
                // 4. Identify most common words
                // a dictionary to store word and its frequency as key and string
                // allow case-insesitive string comparisons
                Dictionary<string, int> wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var word in wordList)
                {
                    string cleanWord = new string(word.ToLower().Where(c => !char.IsPunctuation(c)).ToArray());
                    if (string.IsNullOrEmpty(cleanWord)) continue;
                    if (wordFrequency.ContainsKey(cleanWord))
                    {
                        wordFrequency[cleanWord]++;
                    }
                    else
                    {
                        wordFrequency[cleanWord] = 1;
                    }
                }

                var mostCommonWord = wordFrequency.OrderByDescending(pair => pair.Value).Take(1);
                foreach (var pair in mostCommonWord)
                {
                    Console.WriteLine($"most common word: {pair.Key}");
                }
                // 5. Average word length
                List<int> wordLengthList = new List<int>();
                foreach (var word in wordList)
                {
                    string cleanWord = new string(word.ToLower().Where(c => !char.IsPunctuation(c)).ToArray());
                    wordLengthList.Add(cleanWord.Length);
                }

                /*foreach (var length in wordLengthList)
                {
                    Console.WriteLine(length);
                }*/

                double averageWordLength = wordLengthList.AsQueryable().Average();
                Console.WriteLine($"average word length: {averageWordLength}");

                // Example implementation for counting lines:
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");

                // TODO: Additional analysis to be implemented
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}