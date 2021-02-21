using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using WebApiLearn.Data;
using WebApiLearn.Services;
namespace WebApiLearn
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册中间件,文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "JYHApi",
                    Description = "v1的JYHApi"
                });
                c.IncludeXmlComments("WebApiLearn.xml", true);
            });

            //注册服务,每一个http请求都触发一次
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddDbContext<RoutineDbContext>(option =>
            {
                //option.UseSqlite(connectionString: "Data Source=routine.db");
                option.UseSqlServer(Configuration.GetConnectionString("SchoolContext"));

            });

            //services.AddControllers();
            services.AddControllers(setup =>
            {
                //默认情况下位false
                //如果服务器和请求的类型不一致,那么就会返回406状态码,如果服务器只支持json,但是webapi消费者给传入的为xml,则会返回406状态码
                setup.ReturnHttpNotAcceptable = true;
                //默认的为数组的第一个元素的格式,即json,可以在数组中追加xml格式,两个都支持,但默认的还是json
                //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                //这样就是在数组中的第0个位置添加xml,即xml为默认的格式了
                //setup.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());
                //.net 3.0之后 可以直接这样写,等同于上边的追加数组
            }).AddXmlDataContractSerializerFormatters();
            //属性映射
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "CorsPolicy",
                     builder => builder.WithOrigins("http://localhost:44325/")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //跨域
            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
