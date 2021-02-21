using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLearn.Entities;
using WebApiLearn.Models;

namespace WebApiLearn.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            //从Company 映射到 Companydto
            CreateMap<Company, CompanyDto>()
                //属性名一样 则可以进行映射,但是如果属性名不一样 则不会进行赋值
                //但如果属性名不一样,但是属性的值需要相同的,如ConpanyName 和 Name 则可以用如下方法,可以使用多个ForMember
                .ForMember(
                dest => dest.CompanyName,
                opt => opt.MapFrom(src => src.Name));
        }
    }
}
