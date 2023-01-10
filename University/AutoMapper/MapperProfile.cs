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
            CreateMap<Student, StudentEditViewModel>().ReverseMap();
            CreateMap<Student, StudentDetailsViewModel>()
                .ForMember(
                    dest => dest.NrOfEnrollments,
                    from => from.MapFrom(s => s.Enrollments.Count));
                
        }  
    }
}
