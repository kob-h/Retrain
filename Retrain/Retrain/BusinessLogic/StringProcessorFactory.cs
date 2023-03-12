using System;
using Retrain.DataContracts;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Retrain.BusinessLogic
{
	public class StringProcessorFactory : IStringProcessorFactory
    {
        private Dictionary<StringType, IStringProcessor> _stringProcessors;
		public StringProcessorFactory(IEnumerable<IStringProcessor> processors)
		{
            _stringProcessors = processors.ToDictionary(keySelector: sp => sp.StringType);
        }

        public IStringProcessor Create(StringType stringType)
        {
            if (_stringProcessors.ContainsKey(stringType))
            {
                switch (stringType)
                {
                    case StringType.STRING:
                        return _stringProcessors[StringType.STRING];
                    case StringType.PATH:
                        return _stringProcessors[StringType.PATH];
                    case StringType.URL:
                        return _stringProcessors[StringType.URL];
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}

