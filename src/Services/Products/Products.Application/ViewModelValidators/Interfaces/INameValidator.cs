using System.Threading.Tasks;

namespace Products.Application.ViewModelValidators.Interfaces
{
    public interface INameValidator
    {
        Task<bool> ValidateAsync(string name);
    }
}
