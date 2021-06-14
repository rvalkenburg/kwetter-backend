using System.Linq;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Kwetter.Services.AuthorizationService.Application;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Application.Services;
using Kwetter.Services.AuthorizationService.Infrastructure;
using Kwetter.Services.AuthorizationService.Infrastructure.Authorization;
using Kwetter.Services.AuthorizationService.Persistence;
using Kwetter.Services.AuthorizationService.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Kwetter.Services.AuthorizationService.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddScoped<ITokenVerifier, FirebaseTokenVerifier>();

            services.AddApplicationInsightsTelemetry(
                Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

            var firebaseSettings = Configuration.GetSection("FirebaseConfig").GetChildren();
            var configurationSections = firebaseSettings.ToList();
            var json = JsonConvert.SerializeObject(configurationSections.AsEnumerable()
                .ToDictionary(k => k.Key, v => v.Value));
            var firebaseApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(json)
            });
            services.AddSingleton(firebaseApp);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kwetter",
                    Version = "1.0",
                    Description = "Authorization API for Kwetter."
                });
            });
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/s64-1-vetis";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/s64-1-vetis",
                        ValidateAudience = true,
                        ValidAudience = "s64-1-vetis",
                        ValidateLifetime = true
                    };
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
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kwetter"); });
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AuthContext>();
                context.Database.EnsureCreated();
            }

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}