using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simplify.DI;
using Simplify.Web;
using Simplify.Web.Auth;

namespace SampleApp.Classic;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = new PathString("/login");
				options.Cookie.Name = "AppCookie";
			});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
			app.UseDeveloperExceptionPage();

		app.UseAuthentication();
		app.UseAuthRedirect("/login");

		app.UseSimplifyWeb();

		DIContainer.Current.Verify();
	}
}