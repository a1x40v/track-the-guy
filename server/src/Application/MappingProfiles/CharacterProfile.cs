using Application.DTO.Character;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<Character, CharacterDto>()
                .ForMember(d => d.CreatorName, o => o.MapFrom(s => s.Creator.UserName));
        }
    }
}