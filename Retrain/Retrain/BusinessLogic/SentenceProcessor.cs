using System;
using System.Text.RegularExpressions;
using Retrain.DataAccess;
using Retrain.DataContracts;

namespace Retrain.BusinessLogic
{
	public class SentenceProcessor : ISentenceProcessor
    {
        private readonly IWordsRepository _wordsRepository;
        public SentenceProcessor(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
        }

        const string SANITIZATION_NON_ALPHABETIC_PATTERN_REGEX = "[^a-zA-Z]";
        const string SANITIZATION_ESCAPE_CHRACTER_QUOTATION_MARK = @"\""";

        public void ProcessLine(string line, Dictionary<string, int> wordCounters)
        {
            line = Regex.Replace(line, SANITIZATION_ESCAPE_CHRACTER_QUOTATION_MARK, " ");
            var unsanitizedWords = line.Split(" ");

            foreach (var unsanitizedWord in unsanitizedWords)
            {
                string sanitizedWord = SanitizeWord(unsanitizedWord);
                if (!string.IsNullOrEmpty(sanitizedWord))
                {
                    UpdateWordCounter(wordCounters, sanitizedWord);
                }
            }
        }

        public async Task PersistWordCounters(Dictionary<string, int> wordCounters)
        {
            var existingWords = await _wordsRepository.GetAsync(wordCounters.Keys.ToList());
            var existingWordsDict = existingWords.ToDictionary(w => w.WordStr);

            foreach (var wordAtCount in wordCounters.Keys)
            {
                if (existingWordsDict.ContainsKey(wordAtCount))
                {
                    var currentWord = existingWordsDict[wordAtCount];
                    currentWord.Count = currentWord.Count + wordCounters[wordAtCount];
                    _wordsRepository.Update(currentWord);
                }
                else
                {
                    await _wordsRepository.AddAsync(new Model.Word()
                    {
                        WordStr = wordAtCount,
                        Count = wordCounters[wordAtCount]
                    });
                }
            }

            await _wordsRepository.SaveChangesAsync();
        }

        private static string SanitizeWord(string unsanitizedWord)
        {
            return Regex.Replace(unsanitizedWord.Trim().ToLower(), SANITIZATION_NON_ALPHABETIC_PATTERN_REGEX, String.Empty);
        }

        private static void UpdateWordCounter(Dictionary<string, int> dict, string sanitizedWord)
        {
            if (dict.ContainsKey(sanitizedWord))
            {
                dict[sanitizedWord] = ++dict[sanitizedWord];
            }
            else
            {
                dict[sanitizedWord] = 1;
            }
        }
    }
}

