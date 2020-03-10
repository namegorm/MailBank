using System.Threading.Tasks;

using Products.Application.ViewModelValidators.Interfaces;

namespace Products.Application.ViewModelValidators.Implementations
{
    public class FirstProductNameValidator : IFirstProductNameValidator
    {
        public async Task<bool> ValidateAsync(string name)
        {
            return true;
        }
    }
}
