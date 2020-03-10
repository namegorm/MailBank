using AutoMapper;

namespace Core.Application.Mapping.Interfaces
{
    public interface ICoreMapTo<T>
    {
        void MapTo(Profile profile)
        {
            profile.CreateMap(GetType(), typeof(T));
        }
    }
}
