using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                .ReverseMap();
            CreateMap<Employee, EditEmployeeDto>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                .ReverseMap();
        }
    }
}
