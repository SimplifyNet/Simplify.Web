using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Old;

namespace SampleApp.WindowsServiceHosted;

public class Startup
{
	public void Configure(IApplicationBuilder app, IHostingEnvironment env)
	{
		if (env.IsDevelopment())
			app.UseDeveloperExceptionPage();

		app.UseSimplifyWebWithoutRegistrations();

		BootstrapperFactory.ContainerProvider.Verify();
	}
}