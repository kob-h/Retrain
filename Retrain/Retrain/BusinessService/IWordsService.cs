using System;
using Retrain.DataContracts;

namespace Retrain.BusinessService
{
	public interface IWordsService
	{
        Task<int> GetStatistics(string wordStr);
        Task Count(CountRequest countRequest);
    }
}

