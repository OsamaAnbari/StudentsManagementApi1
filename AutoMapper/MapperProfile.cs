using AutoMapper;
using Students_Management_Api.Models;

namespace Students_Management_Api.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentViewModel>()
                .ForMember(c => c.Firstname, map => map.MapFrom(d => d.Firstname))
                .ForMember(c => c.Surname, map => map.MapFrom(d => d.Surname))
                .ForMember(c => c.Phone, map => map.MapFrom(d => d.Phone))
                .ForMember(c => c.Birth, map => map.MapFrom(d => d.Birth))
                .ForMember(c => c.IdentityNo, map => map.MapFrom(d => d.IdentityNo))
                .ForMember(c => c.Faculty, map => map.MapFrom(d => d.Faculty))
                .ForMember(c => c.Department, map => map.MapFrom(d => d.Department))
                .ForMember(c => c.Year, map => map.MapFrom(d => d.Year))
                .ReverseMap();

            CreateMap<Teacher, TeacherViewModel>()
                .ForMember(c => c.Firstname, map => map.MapFrom(d => d.Firstname))
                .ForMember(c => c.Surname, map => map.MapFrom(d => d.Surname))
                .ForMember(c => c.Phone, map => map.MapFrom(d => d.Phone))
                .ForMember(c => c.Birth, map => map.MapFrom(d => d.Birth))
                .ForMember(c => c.IdentityNo, map => map.MapFrom(d => d.IdentityNo))
                .ForMember(c => c.Study, map => map.MapFrom(d => d.Study))
                .ReverseMap();

            CreateMap<Supervisor, SupervisorViewModel>()
                .ForMember(c => c.Firstname, map => map.MapFrom(d => d.Firstname))
                .ForMember(c => c.Surname, map => map.MapFrom(d => d.Surname))
                .ForMember(c => c.Phone, map => map.MapFrom(d => d.Phone))
                .ForMember(c => c.Birth, map => map.MapFrom(d => d.Birth))
                .ForMember(c => c.IdentityNo, map => map.MapFrom(d => d.IdentityNo))
                .ReverseMap();
        }
    }
}
