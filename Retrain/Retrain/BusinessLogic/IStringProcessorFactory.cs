using System;
using Retrain.DataContracts;

namespace Retrain.BusinessLogic
{
	public interface IStringProcessorFactory
	{
        IStringProcessor Create(StringType inputType);
    }
}

