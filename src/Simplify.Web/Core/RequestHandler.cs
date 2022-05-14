using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.StaticFiles;

namespace Simplify.Web.Core;

/// <summary>
/// Provides OWIN HTTP request handler
/// </summary>
public class RequestHandler : IRequestHandler
{
	private readonly IControllersRequestHandler _controllersRequestHandler;
	private readonly IStaticFilesRequestHandler _staticFilesRequestHandler;
	private readonly bool _staticFilesHandling;

	/// <summary>
	/// Initializes a new instance of the <see cref="RequestHandler" /> class.
	/// </summary>
	/// <param name="controllersRequestHandler">The controllers request handler.</param>
	/// <param name="staticFilesRequestHandler">The static files request handler.</param>
	/// <param name="staticFilesHandling">Sets a value indicating whether Simplify.Web static files processing is enabled or controllers requests should be processed only.</param>
	public RequestHandler(IControllersRequestHandler controllersRequestHandler, IStaticFilesRequestHandler staticFilesRequestHandler, bool staticFilesHandling)
	{
		_controllersRequestHandler = controllersRequestHandler;
		_staticFilesRequestHandler = staticFilesRequestHandler;
		_staticFilesHandling = staticFilesHandling;
	}

	/// <summary>
	/// Processes the OWIN HTTP request.
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public Task<RequestHandlingStatus> ProcessRequest(IDIResolver resolver, HttpContext context) =>
		_staticFilesHandling && _staticFilesRequestHandler.IsStaticFileRoutePath(context)
			? _staticFilesRequestHandler.ProcessRequest(context)
			: _controllersRequestHandler.ProcessRequest(resolver, context);
}