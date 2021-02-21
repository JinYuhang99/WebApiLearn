using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLearn.Entities;

namespace WebApiLearn.Data
{
    public class RoutineDbContext : DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options)
            : base(options)
        {

        }
        //注册到数据库的两个表
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //这段代码为 给Company 的名称是必填的(IsRequired),并且最大长度为100
        //    modelBuilder.Entity<Company>()
        //        .Property(x => x.Name).IsRequired().HasMaxLength(100);
        //    modelBuilder.Entity<Company>()
        //        .Property(x => x.Introduction).HasMaxLength(500);

        //    modelBuilder.Entity<Employee>()
        //        .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
        //    modelBuilder.Entity<Employee>()
        //        .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        //    modelBuilder.Entity<Employee>()
        //        .Property(x => x.LastName).IsRequired().HasMaxLength(50);

        //    modelBuilder.Entity<Employee>()
        //        .HasOne(x => x.Company) //指明Employee 类的导航属性是Company
        //        .WithMany(x => x.Employees) // Company 类中的导航属性是 Employees
        //        .HasForeignKey(x => x.CompanyId) //外键是CompanyId
        //        .OnDelete(DeleteBehavior.Restrict); //在做删除的时候Company下面存在Employee时,是不能删除的.
        //    /*
        //     * 我们还可以在 OnModelCreating 方法中,配置一些种子数据,在做数据迁移的时候,我们就可以使用到这些.
        //     * 种子数据.
        //     */

        //    modelBuilder.Entity<Company>().HasData(
        //        new Company
        //        {
        //            Id = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716c"),
        //            Name = "Microsoft",
        //            Introduction = "Great Company"
        //        },
        //        new Company
        //        {
        //            Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716440"),
        //            Name = "Google",
        //            Introduction = "Don't be evil"

        //        },
        //        new Company
        //        {
        //            Id = Guid.Parse("5efc910b-2f45-43df-afae-620d40542853"),
        //            Name = "Alipapa",
        //            Introduction = "Fubao Company"
        //        }
        //    );
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(
        //        @"Server=(localdb)\mssqllocaldb;Database=Blogging;Integrated Security=True");
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string str = "data source=.; Initial Catalog=NetCore_TestDB ; uid=sa; pwd=qwertyuiop";
        //    optionsBuilder.UseSqlServer(str);
        //}
    }
}
