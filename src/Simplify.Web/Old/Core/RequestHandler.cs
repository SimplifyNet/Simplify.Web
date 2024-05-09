using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Old.Core.Controllers;
using Simplify.Web.Old.Core.StaticFiles;

namespace Simplify.Web.Old.Core;

/// <summary>
/// Provides OWIN HTTP request handler.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RequestHandler" /> class.
/// </remarks>
/// <param name="controllersRequestHandler">The controllers request handler.</param>
/// <param name="staticFilesRequestHandler">The static files request handler.</param>
/// <param name="staticFilesHandling">Sets a value indicating whether Simplify.Web static files processing is enabled or controllers requests should be processed only.</param>
public class RequestHandler(IControllersRequestHandler controllersRequestHandler,
	IStaticFilesRequestHandler staticFilesRequestHandler,
	bool staticFilesHandling) : IRequestHandler
{
	/// <summary>
	/// Processes the OWIN HTTP request.
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public Task<RequestHandlingStatus> ProcessRequest(IDIResolver resolver, HttpContext context) =>
		staticFilesHandling && staticFilesRequestHandler.IsStaticFileRoutePath(context)
			? staticFilesRequestHandler.ProcessRequest(context)
			: controllersRequestHandler.ProcessRequest(resolver, context);
}