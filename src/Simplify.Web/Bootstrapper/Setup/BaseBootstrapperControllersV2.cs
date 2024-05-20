using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.V2.Execution;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper v1 controllers registration.
/// </summary>
public partial class BaseBootstrapper
{
	public virtual void RegisterIController2Factory()
	{
		if (TypesToExclude.Contains(typeof(IController2Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController2Factory, Controller2Factory>();
	}
}