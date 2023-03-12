using System;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using Retrain.DataContracts;

namespace Retrain.BusinessLogic
{
    public class PathStringProcessor : StringProcessorBase
    {
        private readonly ISentenceProcessor _sentenceProcessor;

        public PathStringProcessor(
            ISentenceProcessor sentenceProcessor) : base(sentenceProcessor)
        {
            _sentenceProcessor = sentenceProcessor;
        }

        public override StringType StringType => StringType.PATH;

        public override async Task<(StreamReader, Stream)> FetchDataStream(string input)
        {
            var fileStream = new FileStream(input, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream, Encoding.UTF8);

            return await Task.FromResult((streamReader, fileStream));
        }
    }
}

