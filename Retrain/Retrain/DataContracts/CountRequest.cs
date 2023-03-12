using System;
namespace Retrain.DataContracts
{
	public class CountRequest
	{
        public string StringInput { get; set; }
        public StringType InputType { get; set; }
    }

    public enum StringType
    {
        STRING = 1,
        PATH,
        URL
    }
}

