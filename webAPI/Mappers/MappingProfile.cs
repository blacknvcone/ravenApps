using AutoMapper;
using Entities;
using Entities.Models;

namespace webAPI.Mappers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles(){
            
            CreateMap<ProjectUpdateDTO, Project>().ReverseMap();

        }
    }
}