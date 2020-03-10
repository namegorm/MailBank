
using Core.Application.Mapping.Interfaces;
using Core.Application.ViewModels.Interfaces;

using Products.Domain.Entities;

namespace Products.Application.ViewModels
{
    public class ProductViewModel : ICoreViewModel, ICoreMapTo<Product>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
