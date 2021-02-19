using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApiLearn.Data;

namespace WebApiLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //restful webapi
            CreateHostBuilder(args).Build().Run();

            //using (var scop = host.Services.CreateScope())
            //{
            //    try
            //    {
            //        var dbContext = scop.ServiceProvider.GetService<RoutineDbContext>();
            //        //配置完成后,清空数据库
            //        //dbContext.Database.EnsureDeleted();
            //        //dbContext.Database.Migrate();
            //    }
            //    catch (Exception e) { 
            //        var logger = scop.ServiceProvider.GetService<ILogger<Program>>();
            //        logger.LogError(e, "DataBASE Migration Error! ");
            //    }
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
