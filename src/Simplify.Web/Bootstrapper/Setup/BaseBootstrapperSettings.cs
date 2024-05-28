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
	private static void RegisterSimplifyWebSettings(ISimplifyWebSettings settings) =>
		BootstrapperFactory.ContainerProvider.Register(r => settings, LifetimeType.Singleton);
}