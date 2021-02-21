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
            //ע���м��,�ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "JYHApi",
                    Description = "v1��JYHApi"
                });
                c.IncludeXmlComments("WebApiLearn.xml", true);
            });

            //ע�����,ÿһ��http���󶼴���һ��
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddDbContext<RoutineDbContext>(option =>
            {
                //option.UseSqlite(connectionString: "Data Source=routine.db");
                option.UseSqlServer(Configuration.GetConnectionString("SchoolContext"));

            });

            //services.AddControllers();
            services.AddControllers(setup =>
            {
                //Ĭ�������λfalse
                //�������������������Ͳ�һ��,��ô�ͻ᷵��406״̬��,���������ֻ֧��json,����webapi�����߸������Ϊxml,��᷵��406״̬��
                setup.ReturnHttpNotAcceptable = true;
                //Ĭ�ϵ�Ϊ����ĵ�һ��Ԫ�صĸ�ʽ,��json,������������׷��xml��ʽ,������֧��,��Ĭ�ϵĻ���json
                //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                //���������������еĵ�0��λ�����xml,��xmlΪĬ�ϵĸ�ʽ��
                //setup.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());
                //.net 3.0֮�� ����ֱ������д,��ͬ���ϱߵ�׷������
            }).AddXmlDataContractSerializerFormatters();
            //����ӳ��
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //����
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

            //����
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
