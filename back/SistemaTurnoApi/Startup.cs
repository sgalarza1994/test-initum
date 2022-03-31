using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SistemaTurnoApi.Models;
using SistemaTurnoApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaTurnoApi
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
            AddCors(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SistemaTurnoApi", Version = "v1" });
            });

            #region Db
            services.AddDbContext<TurnQueueDb>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
                options.EnableSensitiveDataLogging();

            }, ServiceLifetime.Transient);
            #endregion

            #region InyeccionDependecia
            services.AddScoped<IQueueRepository, QueueRepository>();
            services.AddScoped<ITurnoRepository, TurnoRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaTurnoApi v1"));
            }
            app.UseCors("MyPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private static void AddCors(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
            {


                //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod();

                builder.WithOrigins(
                    "https://localhost:8080",
                    "http://localhost:8080"
                    )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();


            }));
        }
    }
}
