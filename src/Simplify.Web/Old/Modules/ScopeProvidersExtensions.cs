using Simplify.DI;
using Simplify.Web.Http;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Modules;

/// <summary>
/// Simplify.Web providers extensions.
/// </summary>
public static class ScopeProvidersExtensions
{
	/// <summary>
	/// Setups the Simplify.Web providers for specified scope request.
	/// </summary>
	/// <param name="scope">The scope.</param>
	/// <param name="context">The context.</param>
	public static ILifetimeScope SetupProviders(this ILifetimeScope scope, IHttpContext context)
	{
		scope.Resolver.Resolve<IWebContextProvider>().Setup(context.Context);
		scope.Resolver.Resolve<ILanguageManagerProvider>().Setup(context.Context);
		scope.Resolver.Resolve<ITemplateFactory>().Setup();
		scope.Resolver.Resolve<IFileReader>().Setup();
		scope.Resolver.Resolve<IStringTable>().Setup();

		return scope;
	}
}