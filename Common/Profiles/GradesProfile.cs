using AutoMapper;
using EduTestWebApplication.Models;
using EduTestWebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTestWebApplication.Common.Profiles
{
    public class GradesProfile : Profile
    {
        public GradesProfile()
        {
            CreateMap<Grade, GradeViewModel>();
            CreateMap<GradeViewModel, Student>();
        }
    }
}
