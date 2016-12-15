﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using CityInfo.API.Services;

namespace CityInfo.API
{
	public class Startup
	{
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
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

			app.UseStatusCodePages();

			app.UseMvc();
		}
	}
}
