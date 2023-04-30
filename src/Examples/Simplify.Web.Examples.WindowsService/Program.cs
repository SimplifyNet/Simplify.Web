using System;
using System.Diagnostics;
using Simplify.DI;
using Simplify.WindowsServices;

namespace Simplify.Web.Examples.WindowsService;

internal class Program
{
	private static void Main(string[] args)
	{
#if DEBUG
		Debugger.Launch();
#endif

		if (new BasicServiceHandler<WebApplicationStartup>(true).Start(args))
			return;

		using (var scope = DIContainer.Current.BeginLifetimeScope())
			scope.Resolver.Resolve<WebApplicationStartup>().Run();

		Console.ReadLine();
	}
}