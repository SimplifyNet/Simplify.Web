using System;
using SampleApp.WindowsServiceHosted.Setup;
using Simplify.DI;
using Simplify.WindowsServices;

namespace SampleApp.WindowsServiceHosted;

internal static class Program
{
	public static string[] Args { get; private set; }

	private static void Main(string[] args)
	{
#if DEBUG
		global::System.Diagnostics.Debugger.Launch();
#endif

		Args = args;

		DIContainer.Current
			.RegisterAll()
			.Verify();

		using var handler = new BasicServiceHandler<WebApplicationStartup>();

		if (!handler.Start(args))
			RunAsAConsoleApplication();
	}

	private static void RunAsAConsoleApplication()
	{
		using var scope = DIContainer.Current.BeginLifetimeScope();

		scope.Resolver.Resolve<WebApplicationStartup>().Run();

		Console.ReadLine();
	}
}