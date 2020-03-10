using System.Threading.Tasks;

using Products.Application.ViewModelValidators.Interfaces;

namespace Products.Application.ViewModelValidators.Implementations
{
    public class SecondProductNameValidator : ISecondProductNameValidator
    {
        public async Task<bool> ValidateAsync(string name)
        {
            return true;
        }
    }
}
