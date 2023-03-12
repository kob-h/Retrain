using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Retrain.Model;
using Retrain.Persistence;

namespace Retrain.DataAccess
{
	public class WordsRepository : IWordsRepository
    {
        private readonly RetrainDb _retrainDb;

        public WordsRepository(RetrainDb retrainDb)
		{
            _retrainDb = retrainDb;
        }

        public async Task<Word?> GetAsync(string wordStr)
        {
            return await _retrainDb.Words.SingleOrDefaultAsync(word => word.WordStr == wordStr);
        }

        public async Task<List<Word?>> GetAsync(List<string> wordsStr)
        {
            return await _retrainDb.Words.Where(word => wordsStr.Contains(word.WordStr)).ToListAsync();
        }

        public async Task AddAsync(Word word)
        {
            await _retrainDb.Words.AddAsync(word);
        }

        public void Update(Word word)
        {
            _retrainDb.Words.Update(word);
        }

        public async Task SaveChangesAsync()
        {
            await _retrainDb.SaveChangesAsync();
        }
    }
}

