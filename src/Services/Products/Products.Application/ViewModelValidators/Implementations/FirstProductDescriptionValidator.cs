using System.Threading.Tasks;

using Products.Application.ViewModelValidators.Interfaces;

namespace Products.Application.ViewModelValidators.Implementations
{
    public class FirstProductDescriptionValidator : IFirstProductDescriptionValidator
    {
        public async Task<bool> ValidateAsync(string description)
        {
            return true;
        }
    }
}
