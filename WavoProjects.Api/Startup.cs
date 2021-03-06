using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WavoProjects.Api.Hubs;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Models;
using WavoProjects.Api.Workers;
using Microsoft.AspNetCore.Mvc;

namespace WavoProjects.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnv { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnv = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ResponseCacheAttribute { NoStore = true, Location = ResponseCacheLocation.None });
            });

            services.AddDbContext<WavoContext>(options =>
            {
                if (CurrentEnv.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                    if(Configuration.GetValue<bool>("ProductionDatabase"))
                    {
                        options.UseMySQL(Configuration.GetConnectionString("WavoProjectsContextProd"));
                    } else
                    {
                        options.UseSqlServer(Configuration.GetConnectionString("WavoProjectsContext"));
                    }
                } else
                {
                    //Production server uses MariaDB
                    options.UseMySQL(Configuration.GetConnectionString("WavoProjectsContextProd"));
                    //throw new NotImplementedException("Production database not set up!");
                }
            });
            services.AddSignalR();

            if (CurrentEnv.IsDevelopment())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(name: "dev", builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "http://localhost:4201");
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                    });
                });
            } else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(name: "dev", builder =>
                    {
                        builder.WithOrigins("https://wavops.waverlyrobotics.org", "http://localhost:4200");
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                    });
                });
            }


            services.AddHostedService<StartUpWorker>();
            services.AddHostedService<AutoTimesheetSignOutWorker>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseHttpsRedirection();

            app.UseCors("dev");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<WavOpsHub>("/WavOpsHub");
            });
        }
    }
}
