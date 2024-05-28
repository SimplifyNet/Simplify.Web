using System;
using Microsoft.Extensions.Configuration;
using Simplify.System;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the `IConfiguration` factory.
/// </summary>
public static class ConfigurationFactory
{
	/// <summary>
	/// Creates the configuration.
	/// </summary>
	public static IConfiguration Create()
	{
		var environmentName = Environment.GetEnvironmentVariable(ApplicationEnvironment.EnvironmentVariableName);

		var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile($"appsettings.{environmentName}.json", true);

		return builder.Build();
	}
}