using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class NewsEventMappingProfile : Profile
    {
        public NewsEventMappingProfile()
        {
            CreateMap<NewsEvent, NewsEventGetDto>().ReverseMap();
        }
    }
}
