using System.Linq;
using Simplify.DI;
using Simplify.Web.Views;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper view registrations.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the view factory.
	/// </summary>
	public virtual void RegisterViewFactory()
	{
		if (TypesToExclude.Contains(typeof(IViewFactory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IViewFactory>(r => new ViewFactory(r));
	}
}