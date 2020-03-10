using System;
using System.Linq;
using System.Reflection;

using AutoMapper;

using Core.Application.Mapping.Interfaces;

namespace Core.Application.Mapping.Initialization
{
    public abstract class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            var mappingToTypes = GetExecutingAssembly().GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICoreMapTo<>)))
                .ToList();

            foreach (var mappingToType in mappingToTypes)
            {
                var instance = Activator.CreateInstance(mappingToType);

                var mapToMethod = instance?.GetType().GetMethod(nameof(ICoreMapTo<object>.MapTo)) ??
                    instance?.GetType().GetInterface(typeof(ICoreMapTo<>).Name)?.GetMethod(nameof(ICoreMapTo<object>.MapTo));

                mapToMethod?.Invoke(instance, new object[] { this });
            }
        }

        protected virtual Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
