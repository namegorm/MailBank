using System.Threading.Tasks;

namespace Products.Application.ViewModelValidators.Interfaces
{
    public interface IDescriptionValidator
    {
        Task<bool> ValidateAsync(string description);
    }
}
