using AutoMapper;

namespace CleanArchitecture.Blazor.Application.Common.Mappings;

public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType(), MemberList.None);
        profile.CreateMap(GetType(), typeof(T), MemberList.None);
    }
}
