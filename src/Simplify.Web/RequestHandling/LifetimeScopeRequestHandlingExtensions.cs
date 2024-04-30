using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.RequestHandling;

/// <summary>
/// Provides ILifetimeScope extension for launching Simplify.Web request handling pipeline
/// </summary>
public static class LifetimeScopeRequestHandlingExtensions
{
	/// <summary>
	/// Processes the request via Simplify.Web requests handling pipeline.
	/// </summary>
	/// <param name="scope">The scope.</param>
	/// <param name="context">The context.</param>
	public static Task ProcessRequestAsync(this ILifetimeScope scope, HttpContext context) =>
		scope.Resolver.Resolve<IRequestHandlingPipeline>().ExecuteAsync(context);
}