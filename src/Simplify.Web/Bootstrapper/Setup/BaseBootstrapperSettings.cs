using System;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Settings;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper settings registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the Simplify.Web settings.
	/// </summary>
	public virtual void RegisterSimplifyWebSettings()
	{
		if (TypesToExclude.Contains(typeof(ISimplifyWebSettings)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ISimplifyWebSettings, SimplifyWebSettings>(LifetimeType.Singleton);
	}
}