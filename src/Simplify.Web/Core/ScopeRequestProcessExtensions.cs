using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Core;

/// <summary>
/// Provides request execution by Simplify.Web.
/// </summary>
public static class ScopeRequestProcessExtensions
{
	/// <summary>
	/// Processes the request inside specified scope.
	/// </summary>
	/// <param name="scope">The scope.</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public static Task<RequestHandlingStatus> ProcessRequest(this ILifetimeScope scope, HttpContext context) =>
		// Run request process pipeline
		scope.Resolver.Resolve<IRequestHandler>().ProcessRequest(scope.Resolver, context);
}