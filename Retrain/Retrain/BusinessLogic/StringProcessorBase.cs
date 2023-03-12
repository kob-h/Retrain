using System;
using Retrain.DataContracts;

namespace Retrain.BusinessLogic
{
	public abstract class StringProcessorBase : IStringProcessor
    {
        private readonly ISentenceProcessor _sentenceProcessor;

        public StringProcessorBase(ISentenceProcessor sentenceProcessor)
        {
            _sentenceProcessor = sentenceProcessor;
        }

        public abstract StringType StringType { get; }
        protected abstract Task<(StreamReader, Stream)> FetchDataStream(string input);

        public async Task Execute(string input)
        {
            var (streamReader, stream) = await FetchDataStream(input);
            await ProcessStream(streamReader);
            ReleaseResources(streamReader, stream);
        }

        public async Task ProcessStream(StreamReader streamReader)
        {
            var line = String.Empty;
            var stringCounter = new Dictionary<string, int>();
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                _sentenceProcessor.ProcessLine(line, stringCounter);
            }

            await _sentenceProcessor.PersistWordCounters(stringCounter);
        }

        private static void ReleaseResources(StreamReader streamReader, Stream stream)
        {
            streamReader?.Dispose();
            stream?.Dispose();
        }
    }
}

