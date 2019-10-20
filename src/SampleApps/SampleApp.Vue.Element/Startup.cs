using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Simplify.Web;

namespace SampleApp.Vue.Element
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true
				});
			}

			var fileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "wwwroot/dist"));

			app.UseDefaultFiles(new DefaultFilesOptions
			{
				FileProvider = fileProvider
			});

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = fileProvider
			});

			app.UseSimplifyWeb();
		}
	}
}