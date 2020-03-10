using System.Linq;
using System.Threading.Tasks;

using Core.Application.ViewModelsValidators.Implementations;

using FluentValidation;

using Products.Application.ViewModels;
using Products.Application.ViewModelValidators.Interfaces;

namespace Products.Application.ViewModelValidators
{
    public class ProductViewModelValidator : CoreViewModelValidator<ProductViewModel>
    {
        public ProductViewModelValidator(
            IFirstProductNameValidator firstProductNameValidator, ISecondProductNameValidator secondProductNameValidator,
            IFirstProductDescriptionValidator firstProductDescriptionValidator, ISecondProductDescriptionValidator secondProductDescriptionValidator)
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Product name is required.");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Max product name is 200 symbols.");

            RuleFor(x => x.Name).MustAsync(async (value, cancellation) =>
            {
                var firstProductNameValidatorTask = firstProductNameValidator.ValidateAsync(value);
                var secondProductNameValidatorTask = secondProductNameValidator.ValidateAsync(value);

                var nameValidatorResults = await Task.WhenAll(firstProductNameValidatorTask, secondProductNameValidatorTask);
                return nameValidatorResults.All(x => x == true);
            })
            .WithMessage("Product name error.");

            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Max product description is 500 symbols.");
            RuleFor(x => x.Description).MustAsync(async (value, cancellation) =>
            {
                var firstProductDescriptionValidatorTask = firstProductDescriptionValidator.ValidateAsync(value);
                var secondProductDescriptionValidatorTask = secondProductDescriptionValidator.ValidateAsync(value);

                var descriptionValidatorResults = await Task.WhenAll(firstProductDescriptionValidatorTask, secondProductDescriptionValidatorTask);
                return descriptionValidatorResults.All(x => x == true);
            })
            .WithMessage("Product description error.");
        }
    }
}
