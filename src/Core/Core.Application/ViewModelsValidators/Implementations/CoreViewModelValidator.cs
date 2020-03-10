using Core.Application.ViewModelsValidators.Interfaces;

using FluentValidation;

namespace Core.Application.ViewModelsValidators.Implementations
{
    public abstract class CoreViewModelValidator<TViewModel> : AbstractValidator<TViewModel>, ICoreViewModelValidator
    {
        protected CoreViewModelValidator()
        {
        }
    }
}
