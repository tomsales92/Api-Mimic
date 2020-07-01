using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimicAPI.Database;
using MimicAPI.V1.Repositories;
using MimicAPI.V1.Repositories.Interfaces;
using AutoMapper;
using MimicAPI.Helpers;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace MimicAPI
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
            #region AutoMapper - Config
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOMapperProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddDbContext<MimicContext>(opt =>
            {
                opt.UseSqlite("Data Source=Database\\mimic.db");
            });
            services.AddMvc();
            services.AddScoped<IPalavraRepository, PalavraRepository>();
            services.AddApiVersioning(cfg => {
                //Gera a informação de versão do cabeçalho
                cfg.ReportApiVersions = true;
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(cfg => {
                cfg.ResolveConflictingActions(apiDescription => apiDescription.First());
                cfg.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Title = "MimicAPI - V1",
                    Version = "V1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePages();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "MIMICAPI");
                cfg.RoutePrefix = String.Empty;
            });
           
        }
    }
}
