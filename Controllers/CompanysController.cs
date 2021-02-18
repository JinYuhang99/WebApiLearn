using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApiLearn.Entities;
using WebApiLearn.Services;

namespace WebApiLearn.Controllers
{

    [ApiController]
    public class CompanysController: ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        //webapi 需要继承 ControllerBase这个类即可
        //webapi 和 mvc web 则需要继承 Controller 这个类

        public CompanysController(ICompanyRepository companyRepository)
        {
            // 2个问号 代表为 第一个问号为空 则第二个?抛出异常,进行依赖注入,如果为空则抛出异常
            _companyRepository = companyRepository ?? 
                throw new ArgumentNullException(nameof(companyRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetConpanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            return new JsonResult(companies);
        }
    }
}
