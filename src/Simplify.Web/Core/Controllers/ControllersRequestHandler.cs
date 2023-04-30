using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Modules;

namespace Simplify.Web.Core.Controllers;

/// <summary>
/// Provides controllers HTTP request handler
/// </summary>
public class ControllersRequestHandler : IControllersRequestHandler
{
	private readonly IControllersProcessor _controllersProcessor;
	private readonly IPageProcessor _pageProcessor;
	private readonly IRedirector _redirector;

	/// <summary>
	/// Initializes a new instance of the <see cref="ControllersRequestHandler" /> class.
	/// </summary>
	/// <param name="controllersProcessor">The controllers request handler.</param>
	/// <param name="pageProcessor">The page processor.</param>
	/// <param name="redirector">The redirector.</param>
	public ControllersRequestHandler(IControllersProcessor controllersProcessor, IPageProcessor pageProcessor, IRedirector redirector)
	{
		_controllersProcessor = controllersProcessor;
		_pageProcessor = pageProcessor;
		_redirector = redirector;
	}

	/// <summary>
	/// Processes the HTTP request for controllers.
	/// </summary>
	/// <param name="resolver">THE DI container resolver</param>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public Task ProcessRequest(IDIResolver resolver, IOwinContext context)
	{
		var result = _controllersProcessor.ProcessControllers(resolver, context);

		switch (result)
		{
			case ControllersProcessorResult.Ok:
				return _pageProcessor.ProcessPage(resolver, context);

			case ControllersProcessorResult.Http401:
				context.Response.StatusCode = 401;
				_redirector.SetLoginReturnUrlFromCurrentUri();
				break;

			case ControllersProcessorResult.Http403:
				context.Response.StatusCode = 403;
				break;

			case ControllersProcessorResult.Http404:
				context.Response.StatusCode = 404;
				break;
		}

		return Task.Delay(0);
	}
}