using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLearn.Entities
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
