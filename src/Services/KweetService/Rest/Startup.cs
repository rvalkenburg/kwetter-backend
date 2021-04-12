using Kwetter.Services.KweetService.Application;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Services;
using Kwetter.Services.KweetService.Consumer;
using Kwetter.Services.KweetService.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kwetter.Services.KweetService.Rest
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
            services.AddScoped<IKweetService, Application.Services.KweetService>();
            services.AddScoped<IProfileService, ProfileService>();
            
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddApplication();
            services.AddPersistence(Configuration);

            services.AddConsumer(Configuration);
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title="Kwetter",
                    Version="1.0",
                    Description="Kweet API for Kwetter."                   
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
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