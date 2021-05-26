using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Kwetter.Services.AuthorizationService.Application;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Application.Services;
using Kwetter.Services.AuthorizationService.Infrastructure;
using Kwetter.Services.AuthorizationService.Infrastructure.Authorization;
using Kwetter.Services.AuthorizationService.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kwetter.Services.AuthorizationService.Rest
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
            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddScoped<ITokenVerifier, FirebaseTokenVerifier>();
            
            FirebaseApp firebaseApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("firebase-settings.json"),
            });
            services.AddSingleton(firebaseApp);
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title="Kwetter",
                    Version="1.0",
                    Description="Authorization API for Kwetter."                   
                });
            });
            services.AddScoped<IAuthService, AuthService>();

            services.AddControllers();
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