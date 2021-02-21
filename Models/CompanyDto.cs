using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLearn.Models
{
    //提供给外部的model层
    public class CompanyDto
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; }

    }
}
