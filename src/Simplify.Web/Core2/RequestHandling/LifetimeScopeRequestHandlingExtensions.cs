using System.Threading.Tasks;
using Simplify.DI;
using Simplify.Web.Http;

namespace Simplify.Web.Core2.RequestHandling;

/// <summary>
/// Provides core request handling by Simplify.Web.
/// </summary>
public static class LifetimeScopeRequestHandlingExtensions
{
	/// <summary>
	/// Runs the requests handling pipeline.
	/// </summary>
	/// <param name="scope">The scope.</param>
	/// <param name="context">The context.</param>
	public static Task ProcessRequestAsync(this ILifetimeScope scope, IHttpContext context) =>
		scope.Resolver.Resolve<IRequestHandlingPipeline>().ExecuteAsync(context);
}