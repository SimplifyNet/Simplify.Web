using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Old.Core.PageAssembly;

/// <summary>
/// Provides page processor.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PageProcessor"/> class.
/// </remarks>
/// <param name="pageBuilder">The page builder.</param>
/// <param name="responseWriter">The response writer.</param>
public class PageProcessor(IPageBuilder pageBuilder, IResponseWriter responseWriter) : IPageProcessor
{
	private readonly IPageBuilder _pageBuilder = pageBuilder;
	private readonly IResponseWriter _responseWriter = responseWriter;

	/// <summary>
	/// Processes (build web-page and send to client, process current page state) the current web-page
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	public async Task<RequestHandlingStatus> ProcessPage(IDIResolver resolver, HttpContext context)
	{
		context.Response.ContentType = "text/html";

		await _responseWriter.WriteAsync(_pageBuilder.Build(resolver), context.Response);

		return RequestHandlingStatus.RequestWasHandled;
	}
}