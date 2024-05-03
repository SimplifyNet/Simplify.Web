using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.System;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper configuration registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the configuration.
	/// </summary>
	public virtual void RegisterConfiguration()
	{
		if (TypesToExclude.Contains(typeof(IConfiguration)))
			return;

		var environmentName = Environment.GetEnvironmentVariable(ApplicationEnvironment.EnvironmentVariableName);

		var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile($"appsettings.{environmentName}.json", true);

		BootstrapperFactory.ContainerProvider.Register<IConfiguration>(r => builder.Build(), LifetimeType.Singleton);
	}
}