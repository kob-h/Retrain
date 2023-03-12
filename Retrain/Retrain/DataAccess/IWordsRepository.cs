using System;
using Retrain.Model;
using Retrain.Persistence;

namespace Retrain.DataAccess
{
	public interface IWordsRepository
	{
		Task<Word?> GetAsync(string wordStr);
        Task<List<Word?>> GetAsync(List<string> wordsStr);
        Task AddAsync(Word word);
        void Update(Word word);
        Task SaveChangesAsync();
    }
}

