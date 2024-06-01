using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Simplify.DI;
using Simplify.Web.Settings;

namespace Simplify.Web.Diagnostics.Trace;

/// <summary>
/// Provides trace extensions for requests.
/// </summary>
public static class ScopeTraceExtensions
{
	/// <summary>
	/// Traces the specified scope request.
	/// </summary>
	/// <param name="scope">The scope.</param>
	/// <param name="context">The context.</param>
	/// <param name="eventHandler">The event handler.</param>
	public static ILifetimeScope Trace(this ILifetimeScope scope, HttpContext context, TraceEventHandler? eventHandler)
	{
		if (scope.Resolver.Resolve<ISimplifyWebSettings>().ConsoleTracing)
			TraceToConsole(context);

		eventHandler?.Invoke(context);

		return scope;
	}

	private static void TraceToConsole(HttpContext context) =>
		global::System.Diagnostics.Trace.TraceInformation(
			$"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture)}] [{context.Request.Method.Replace(Environment.NewLine, "")}] {context.Request.GetDisplayUrl().Replace(Environment.NewLine, "")}");
}