using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BonoApp.API.Bono.Domain.Repositories;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Persistence.Repositories;
using BonoApp.API.Bono.Services;
using BonoApp.API.Shared.Domain.Repositories;
using BonoApp.API.Shared.Persistence.Contexts;
using BonoApp.API.Shared.Persistence.Repositories;
using BonoApp.API.User.Domain.Repositories;
using BonoApp.API.User.Domain.Services;
using BonoApp.API.User.Persistence.Repositories;
using BonoApp.API.User.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BonoApp.API
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
            services.AddCors();
            
            services.AddControllers();

            services.AddRouting(options => options.LowercaseUrls = true);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "BonoApp.API", Version = "v1"});
            });
            
            services.AddDbContext<AppDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBondRepository, BondRepository>();
            services.AddScoped<IBondService, BondService>();
            services.AddScoped<IBondResultService, BondResultService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BonoApp.API v1"));
            }
            
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection(); 
            
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}