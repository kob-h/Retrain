using System;
namespace Retrain.BusinessLogic
{
	public interface ISentenceProcessor
	{
		void ProcessLine(string line, Dictionary<string, int> wordCounters);
		Task PersistWordCounters(Dictionary<string, int> wordCounters);

    }
}

