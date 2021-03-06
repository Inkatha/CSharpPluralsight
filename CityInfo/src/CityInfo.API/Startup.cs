﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using CityInfo.API.Services;
using Microsoft.Extensions.Configuration;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API
{
	public class Startup
	{
		public static IConfigurationRoot Configuration;

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
			.AddMvcOptions(o =>
				o.OutputFormatters.Add(
					new XmlDataContractSerializerOutputFormatter())
			);

#if DEBUG
			services.AddTransient<IMailService, LocalMailService>();
#else
			services.AddTransient<IMailService, CloudMailService>();
#endif

			var connectionString = Startup.Configuration["connectionStrings:cityInfoDBConnectionString"];
			services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));

			services.AddScoped<ICityInfoRepository, CityInfoRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
			CityInfoContext cityInfoContext)
		{
			loggerFactory.AddConsole();
			loggerFactory.AddDebug(LogLevel.Information);

			loggerFactory.AddNLog();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler();
			}

			cityInfoContext.EnsureSeedDataForContext();

			app.UseStatusCodePages();

			AutoMapper.Mapper.Initialize(config =>
			{
				config.CreateMap<Entities.City,						 Models.CityWithoutPointOfInterestDto>();
				config.CreateMap<Entities.City,						 Models.CityDto>();
				config.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
				config.CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
				config.CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
				config.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
			});

			app.UseMvc();
		}
	}
}
