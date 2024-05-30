using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper configuration registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the configuration.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	private static void RegisterConfiguration(IConfiguration configuration) =>
		BootstrapperFactory.ContainerProvider.Register(r => configuration, LifetimeType.Singleton);
}