using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiLearn.Entities;
using WebApiLearn.Models;
using WebApiLearn.Services;

namespace WebApiLearn.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public EmployeesController(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> 
            GetEmployeesForCompany(Guid companyId)
        {
            if (! await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employees = await _companyRepository.GetEmployeesAsync(companyId);

            var employeesDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeesDtos);
        }

        [HttpGet]
        [Route("{userid}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByUserID(Guid companyId, Guid userid) 
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            if (await _companyRepository.GetEmployeeAsync(companyId, userid) == null)
            {
                return NotFound();
            }

            var employee = await _companyRepository.GetEmployeeAsync(companyId, userid);

            var employeesDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeesDto);
        }
    }
}
