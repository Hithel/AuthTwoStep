

using ApiAuth.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiAuth.Profiles;
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterDto>().ReverseMap();

        }
    }
