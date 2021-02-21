using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApiLearn.Entities;
using WebApiLearn.Models;
using WebApiLearn.Services;

namespace WebApiLearn.Controllers
{

    [ApiController]
    [Route("api/companyies")]
    // [Route("api/[Controller]")]  = api/CompanysController 去掉controller
    public class CompanysController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        //webapi 需要继承 ControllerBase这个类即可
        //webapi 和 mvc web 则需要继承 Controller 这个类

        public CompanysController(ICompanyRepository companyRepository, IMapper mapper)
        {
            // 2个问号 代表为 第一个问号为空 则第二个?抛出异常,进行依赖注入,如果为空则抛出异常
            _companyRepository = companyRepository ??
                throw new ArgumentNullException(nameof(companyRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        //Task<ActionResult<IEnumerable<CompanyDto>>>  Task<IActionResult>
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetConpanies()
        {
            //数据库层的model
            var companies = await _companyRepository.GetCompaniesAsync();
            //给前台传的model
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            //404 notfound(); 空集合 不一定是404
            return Ok(companyDtos);
        }

        [HttpGet] //api/Companies/{companyId}
        [Route("{companyId}")]
        public async Task<ActionResult<CompanyDto>> GetConpanies(Guid companyId)
        {
            //是否存在
            //var exist = await _companyRepository.CompanyExistsAsync(companyID);
            ////如果并发量过大,则可能出现错误
            //if (!exist)
            //{
            //    return NotFound();
            //}
            //比上述方法要好一些
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            var companie = _mapper.Map<CompanyDto>(company);
            //404 notfound(); 空集合 不一定是404
            return Ok(companie);
            //return new JsonResult(companie); 返回json
        }
    }
}
