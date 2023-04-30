using Owin;
using Simplify.Web.Meta;
using Simplify.Web.Owin;

namespace Simplify.Web.Examples.Katana;

public class Startup
{
	public void Configuration(IAppBuilder app)
	{
		// Exclude Simplify.Web from exclude assemblies to be able to load example controllers
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");

		app.UseSimplifyWeb();
	}
}