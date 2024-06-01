using Simplify.DI;
using Simplify.System;
using Simplify.Web;

namespace SampleApp.Api.Setup;

public static class IocRegistrations
{
	private static IConfiguration Configuration { get; set; } = null!;

	public static IDIContainerProvider RegisterAll(this IDIContainerProvider provider)
	{
		provider.RegisterCustomConfiguration(config => Configuration = config)
			.RegisterSimplifyWeb(Configuration);

		return provider;
	}

	private static IDIRegistrator RegisterCustomConfiguration(this IDIRegistrator registrator, Action<IConfiguration>? config = null)
	{
		var environmentName = Environment.GetEnvironmentVariable(ApplicationEnvironment.EnvironmentVariableName);

		var configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile($"appsettings.{environmentName}.json", true)
			.Build();

		registrator.Register<IConfiguration>(_ => configuration, LifetimeType.Singleton);

		config?.Invoke(configuration);

		return registrator;
	}
}