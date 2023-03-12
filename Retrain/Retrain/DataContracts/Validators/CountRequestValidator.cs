using System;
using FluentValidation;

namespace Retrain.DataContracts.Validators
{
	public class CountRequestValidator : AbstractValidator<CountRequest>
	{
		public CountRequestValidator()
		{
            RuleFor(x => x.StringInput).NotNull().NotEmpty();
			RuleFor(x => x.InputType).NotNull().NotEmpty().IsInEnum();
        }
	}
}

