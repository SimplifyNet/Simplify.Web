using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.V2.Execution;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper v2 controllers registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the controller v2 factory.
	/// </summary>
	public virtual void RegisterController2Factory()
	{
		if (TypesToExclude.Contains(typeof(IController2Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController2Factory>(r => new Controller2Factory(r));
	}
}