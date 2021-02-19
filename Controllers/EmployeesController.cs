using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiLearn.Entities;

namespace WebApiLearn.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        [Route("employess")]
        public List<Employee> GetEmployees()
        {
            Employee emp = new Employee();
            List<Employee> employees = new List<Employee>();
            for(int i = 0; i < 5; i++) {
                emp.CompanyId = Guid.NewGuid(); 
                employees.Add(emp);
            }
            return employees;
        }

        [HttpGet]
        [Route("employess/{userid}")]
        public Employee GetEmployeesByUserID(int userid)
        {
            Employee emp = new Employee();
            List<Employee> employees = new List<Employee>();
            for (int i = 0; i < 5; i++)
            {
                emp.CompanyId = Guid.NewGuid();
                employees.Add(emp);
            }
            return employees[userid];
        }
    }
}
