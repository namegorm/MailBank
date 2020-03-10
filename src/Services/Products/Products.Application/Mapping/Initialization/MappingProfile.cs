using System.Reflection;

using Core.Application.Mapping.Initialization;

namespace Products.Application.Mapping.Initialization
{
    public class MappingProfile : CoreMappingProfile
    {
        protected override Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
