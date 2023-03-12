using System;
using System.Text;
using Retrain.DataContracts;
using static System.Net.Mime.MediaTypeNames;

namespace Retrain.BusinessLogic
{
	public class PlainStringProcessor : StringProcessorBase
    {
        private readonly ISentenceProcessor _sentenceProcessor;

        public override StringType StringType => StringType.STRING;

        public PlainStringProcessor(ISentenceProcessor sentenceProcessor) : base(sentenceProcessor)
        {
            _sentenceProcessor = sentenceProcessor;
        }

        protected async override Task<(StreamReader, Stream)> FetchDataStream(string input)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            StreamReader streamReader = new StreamReader(memoryStream);

            return await Task.FromResult((streamReader, memoryStream));
        }
    }
}

