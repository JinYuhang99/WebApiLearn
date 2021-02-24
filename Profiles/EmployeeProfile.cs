﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLearn.Entities;
using WebApiLearn.Models;

namespace WebApiLearn.Profiles
{
    public class EmployeeProfile : Profile
    {
        //从Employeey映射到EmployeeDto
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                ).ForMember(
                    dest => dest.GenderDisplay,
                    opt => opt.MapFrom(src => src.Gender.ToString())
                ).ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year)
                );
        }
    }
}