﻿using System.Threading.Tasks;
using Simplify.DI;
using Simplify.Web.Core2.Http;
using Simplify.Web.Core2.RequestHandling;

namespace Simplify.Web.Core2;

/// <summary>
/// Provides core request handling by Simplify.Web.
/// </summary>
public static class ScopeRequestProcessExtensions
{
	/// <summary>
	/// Runs the requests handling pipeline.
	/// </summary>
	/// <param name="scope">The scope.</param>
	/// <param name="context">The context.</param>
	public static Task<RequestHandlingStatus> ProcessRequest(this ILifetimeScope scope, IHttpContext context) =>
		scope.Resolver.Resolve<IRequestHandlingPipeline>().Execute(context);
}