using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{
    public class DinnerMappingProfile : Profile
    {
        public DinnerMappingProfile()
        {
            CreateMap<Dinner, DinnerGetDto>().ReverseMap();
        }
    }
}
