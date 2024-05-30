using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.V1.Execution;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper v1 controllers registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the controller v1 factory.
	/// </summary>
	public virtual void RegisterController1Factory()
	{
		if (TypesToExclude.Contains(typeof(IController1Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController1Factory>(r => new Controller1Factory(r));
	}
}