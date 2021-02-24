using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApiLearn.Data;
using WebApiLearn.Entities;

namespace WebApiLearn.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _content;

        public CompanyRepository(RoutineDbContext content)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await this._content.Companies.ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            return await this._content.Companies
                .Where(x => companyIds.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        
        {
            if (companyId == null)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await this._content.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public void AddCompany(Company company)
        {
            this._content.Companies.AddAsync(company);
        }

        public void UpdateCompany(Company company)
        {
            //下面这句话不写也行,因为 Ef Core 对 实体是进行跟踪的,所以实体如果有更改的话,Ef core是知道的.
            //this._content.Entry(company).State = EntityState.Modified;
        }

        public void DeleteCompany(Company company)
        {
            this._content.Entry(company).State = EntityState.Deleted;
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == null)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await this._content.Companies.AnyAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId)
        {
            if (companyId == null)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            
            //查看sql语句
            //var cc = this._content.Employees
            //   .Where(x => x.CompanyId == companyId)
            //   .OrderBy(x => x.EmployeeNo)
            //cc.AsQueryable();
            return await this._content.Employees
                .Where(x => x.CompanyId == companyId)
                .OrderBy(x => x.EmployeeNo)
                .ToListAsync(); 
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == null)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == null)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            return await this._content.Employees
                .Where(x => x.CompanyId == companyId && x.Id == employeeId)
                .FirstOrDefaultAsync();

        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == null)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.CompanyId = companyId;
            this._content.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            //下面这句话不写也行,因为 Ef Core 对 实体是进行跟踪的,所以实体如果有更改的话,Ef core是知道的.
            //this._content.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(Employee employee)
        {
            this._content.Employees.Remove(employee);
        }

        public async Task<bool> SaveAsync()
        {
            return await this._content.SaveChangesAsync() >= 0;
        }
    }
}
