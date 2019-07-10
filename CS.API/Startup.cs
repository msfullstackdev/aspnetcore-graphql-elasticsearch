using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS.API.Models;
using CS.DependencyInjection;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CS.API
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            Config.UpdateServiceCollection(services,Configuration);

            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<UserQuery>();
            services.AddSingleton<UserType>();
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new UserSchema(new FuncDependencyResolver(type => sp.GetService(type))));

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseGraphiQl("/graphql");

            app.UseCors(builder => builder.AllowAnyOrigin()
            //WithOrigins("http://localhost:3000/")
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
