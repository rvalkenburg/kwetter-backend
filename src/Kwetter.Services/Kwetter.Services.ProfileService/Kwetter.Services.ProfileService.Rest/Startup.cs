using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.ProfileService.Application;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kwetter.Services.ProfileService.Rest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title="Kwetter",
                    Version="1.0",
                    Description="Profile API for Kwetter."                   
                });
            });
            services.AddScoped<IProfileService, Application.Services.ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c=> {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kwetter");
                });
            }


            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}