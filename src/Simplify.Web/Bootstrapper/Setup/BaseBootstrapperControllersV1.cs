using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.V1.Execution;
using Simplify.Web.Controllers.V1.Matcher;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper v1 controllers registration.
/// </summary>
public partial class BaseBootstrapper
{
	public virtual void RegisterIController1Factory()
	{
		if (TypesToExclude.Contains(typeof(IController1Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController1Factory, Controller1Factory>();
	}

	public virtual void RegisterController1PathParser()
	{
		if (TypesToExclude.Contains(typeof(IController1PathParser)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController1PathParser, Controller1PathParser>(LifetimeType.Singleton);
	}
}