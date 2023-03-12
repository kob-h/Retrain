using System;
using Retrain.DataContracts;

namespace Retrain.BusinessLogic
{
	public interface IStringProcessor
	{
        public StringType StringType { get;  }
        Task Execute(string input);
    }
}

