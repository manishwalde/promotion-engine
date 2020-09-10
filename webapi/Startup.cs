using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using webapi.Data;
using webapi.Services;
using SwaggerOptions = webapi.Options.SwaggerOptions;

namespace webapi
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
         services.AddDbContext<DataContext>(options =>
             options.UseSqlite(
                 Configuration.GetConnectionString("DefaultConnection")));

         services.AddScoped<ISkuService, SkuService>();
         services.AddScoped<IPromotionService, PromotionService>();

         services.AddControllers();

         services.AddAutoMapper(typeof(Startup));

         services.AddSwaggerGen(x =>
         {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "promotion-engine-api", Version = "v1" });
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });

         var swaggerOptions = new SwaggerOptions();
         Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

         app.UseSwagger(options => { options.RouteTemplate = swaggerOptions.JsonRoute; });

         app.UseSwaggerUI(options =>
         {
            options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
         });
      }
   }
}
