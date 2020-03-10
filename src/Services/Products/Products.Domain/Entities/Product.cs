using Core.Domain.Entities.Interfaces;

namespace Products.Domain.Entities
{
    public class Product : ICoreEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
