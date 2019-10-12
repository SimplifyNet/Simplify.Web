using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simplify.Web.Owin;

namespace SampleApp.Angular
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseSpaStaticFiles();

			app.UseSimplifyWebNonTerminal();

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
					spa.UseAngularCliServer("start");
			});
		}
	}
}