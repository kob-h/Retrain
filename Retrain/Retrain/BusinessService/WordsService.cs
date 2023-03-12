using System;
using System.Text.RegularExpressions;
using Retrain.BusinessLogic;
using Retrain.DataAccess;
using Retrain.DataContracts;
using static System.Net.Mime.MediaTypeNames;

namespace Retrain.BusinessService
{
	public class WordsService : IWordsService
    {
        private readonly IStringProcessorFactory _stringProcessorFactory;
        private readonly IWordsRepository _wordsRepository;

        public WordsService(
            IStringProcessorFactory stringProcessorFactory,
            IWordsRepository wordsRepository)
		{
            _stringProcessorFactory = stringProcessorFactory;
            _wordsRepository = wordsRepository;
        }

        public async Task<int> GetStatistics(string wordStr)
        {
            var word = await _wordsRepository.GetAsync(wordStr);

            return word?.Count ?? 0;
        }

        public async Task Count(CountRequest countRequest)
        {
            var processor = _stringProcessorFactory.Create(countRequest.InputType);
            if (processor == null)
            {
                throw new Exception("No such string processor!");
            }

            await processor.Execute(countRequest.StringInput);
        }
    }
}

