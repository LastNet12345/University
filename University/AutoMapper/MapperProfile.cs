using AutoMapper;
using University.Core;
using University.Models;

namespace University.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Student, StudentIndexViewModel>();
            CreateMap<Student, StudentCreateViewModel>().ReverseMap();
        }  
    }
}
