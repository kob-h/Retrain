using System;
using Retrain.DataContracts;

namespace Retrain.BusinessLogic
{
    public class UrlStringProcessor : StringProcessorBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISentenceProcessor _sentenceProcessor;

        public UrlStringProcessor(
            IHttpClientFactory clientFactory,
            ISentenceProcessor sentenceProcessor) : base(sentenceProcessor)
        {
            _clientFactory = clientFactory;
            _sentenceProcessor = sentenceProcessor;
        }

        public override StringType StringType => StringType.URL;

        protected async override Task<(StreamReader, Stream)> FetchDataStream(string input)
        {
            var httpClient = _clientFactory.CreateClient();
            var httpResponse = await httpClient.GetAsync(input);
            var stream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var streamReader = new StreamReader(stream);

            return (streamReader, stream);
        }
    }
}

