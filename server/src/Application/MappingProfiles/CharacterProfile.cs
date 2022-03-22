using Application.DTO.Character;
using Application.Features.Characters.Requests.Commands;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<UpdateCharacterCommand, Character>();
            CreateMap<Character, CharacterDto>()
                .ForMember(d => d.CreatorName, o => o.MapFrom(s => s.Creator.UserName));
        }
    }
}