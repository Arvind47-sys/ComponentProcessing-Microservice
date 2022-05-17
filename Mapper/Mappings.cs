using Api.DTOs;
using Api.Entities;
using AutoMapper;

namespace api.Mapper
{
    /// <summary>
    /// Contains all mapping configurations used in the project
    /// </summary>
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ProcessRequest, ProcessRequestDto>().ReverseMap();
            CreateMap<ProcessResponse, ProcessResponseDto>().ReverseMap();

        }
    }
}
