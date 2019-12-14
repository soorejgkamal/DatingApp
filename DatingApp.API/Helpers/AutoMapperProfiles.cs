using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForDetailedDto>()
            .ForMember(d=>d.PhotoUrl, opt=> 
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p=> p.isMain).Url))
                .ForMember(dest=> dest.Age , opt=> opt.MapFrom(src=> 
                src.DateofBirth.CalculateAge()));
            CreateMap<User,UserForListDto>()
                .ForMember(d=>d.PhotoUrl, opt=> 
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p=> p.isMain).Url))
                .ForMember(dest=> dest.Age , opt=> opt.MapFrom(src=> 
                src.DateofBirth.CalculateAge()));
            CreateMap<Photo,PhotosForDetailedDto>();

            CreateMap<UserForUpdateDto,User>();
        }  
    }
}