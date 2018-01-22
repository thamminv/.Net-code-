using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LongestParentWord.Model;


namespace LongestParentWord
{
    /// <summary>
    /// The Program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// The program main entry function.
        /// </summary>
        /// <param name="args">Arguments for the program.</param>
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 1)
            {
                throw new Exception("Invalid program execution. Please call the program with WordsList file fullpath as the only argument");
            }
            if (!File.Exists(args[0]))
            {
                throw new Exception("Invalid input. The given input file " + args[0] + " doesn't exist. Please check and try again.");
            }
            try
            {
                Execute(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An internal error occured while running this program.  ErrorMessage: " + ex.Message);
            }
        }

        /// <summary>
        /// The program logic exuection.
        /// </summary>
        /// <param name="filePath">The wordlist file full path.</param>
        private static void Execute(string filePath)
        {
            string[] wordsArray = File.ReadAllLines(filePath);
            if (wordsArray.Length == 0)
            {
                Console.WriteLine("The given input file has no words list. Therefore, no longest words can be found.");
            }
            // sort by words length to have long words in the front of the list.
            var wordList = wordsArray.ToList().OrderByDescending(x => x.Length).Distinct().ToList();
            var resultList = GetLargestParentWordList(wordList);
            if (!resultList.Any())
            {
                Console.WriteLine("No largest parent words found in the list!");
            }
            else
            {
                Console.WriteLine("Output:");
                Console.WriteLine("=========");
                Console.WriteLine("The first longest word in the list = {0}", resultList[0].Name);
                Console.WriteLine("The second longest word in the list = {0}", resultList[1].Name);
                Console.WriteLine("The count of other words in the longest word = {0}", resultList[0].OtherWordsCount);
            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit the program.");
            Console.ReadKey();
        }

        /// <summary>
        /// Confirms if the given word is made of other listed words or not.
        /// </summary>
        /// <param name="word">the input word.</param>
        /// <param name="wordList">The whole wordlist.</param>
        /// <returns></returns>
        private static bool IsValidFormation(string word, List<string> wordList)
        {
            if (word.Length == 1)
            {
                if (wordList.Contains(word))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (wordList.Contains(word))
            {
                return true;
            }
            foreach (var map in GetPossibleCombinations(word))
            {
                if (wordList.Contains(map.Item1))
                {
                    if (wordList.Contains(map.Item2))
                    {
                        // both separated words are in the list, therefore valid.
                        return true;
                    }
                    else
                    {
                        if (!IsValidFormation(map.Item2, wordList))
                        {
                            // continue here to check for any other possible match.
                            continue;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            // if we reached this point, the word is invalid to our purpose.
            return false;
        }

        /// <summary>
        /// Identifies the largest parent word from the given words list.
        /// </summary>
        /// <param name="wordList">The words list.</param>
        /// <returns>List of Word object.</returns>
        private static List<Word> GetLargestParentWordList(List<string> wordList)
        {
            List<Word> result = new List<Word>();
            int count = wordList.Count();
            int validLongestWordsCount = 0;
            foreach (var item in wordList)
            {
                int otherWordsCount = 0;
                string tempWord = item;
                Word word = new Word();
                int i = 1;
                while (i <= tempWord.Length)
                {
                    var word1 = tempWord.Substring(0, i);
                    var word2 = string.IsNullOrWhiteSpace(tempWord.Substring(i)) ? word1 : tempWord.Substring(i);
                    if (wordList.Contains(word1) && (word2 != item) && IsValidFormation(word2, wordList))
                    {
                        tempWord = tempWord.Substring(i);
                        i = 0;
                        otherWordsCount++;
                        if (word1 == word2)
                        {
                            validLongestWordsCount++;
                        }
                    }
                    i++;
                }
                word.Name = item;
                word.Length = item.Length;
                word.OtherWordsCount = otherWordsCount;
                result.Add(word);
                // Condition satisfied per our requirement, thus avoiding the other unnecessary execution.
                // Nth largest thresold can be configured as the input param for the parameters.
                if (validLongestWordsCount >= 2 && result.Count >= 2)
                {
                    break;
                }
            }
            var resultList = result.Where(x => x.OtherWordsCount > 0).OrderByDescending(x => x.OtherWordsCount).ToList();
            return resultList;
        }

        /// <summary>
        /// Computes all possible combinations by splitting the given word.
        /// </summary>
        /// <param name="word">Input word.</param>
        /// <returns></returns>
        private static List<Tuple<string, string>> GetPossibleCombinations(string word)
        {
            List<Tuple<string, string>> combinationMap = new List<Tuple<string, string>>();
            for (int i = 1; i < word.Length; i++)
            {
                combinationMap.Add(Tuple.Create(word.Substring(0, i), word.Substring(i)));
            }
            return combinationMap;
        }
    }
}
