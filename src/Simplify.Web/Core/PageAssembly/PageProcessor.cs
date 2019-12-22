using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Simplify.DI;
using Simplify.Web.Modules;

namespace Simplify.Web.Core.PageAssembly
{
	/// <summary>
	/// Provides page processor
	/// </summary>
	public class PageProcessor : IPageProcessor
	{
		private readonly IPageBuilder _pageBuilder;
		private readonly IResponseWriter _responseWriter;
		private readonly IRedirector _redirector;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageProcessor"/> class.
		/// </summary>
		/// <param name="pageBuilder">The page builder.</param>
		/// <param name="responseWriter">The response writer.</param>
		/// <param name="redirector">The redirector.</param>
		public PageProcessor(IPageBuilder pageBuilder, IResponseWriter responseWriter, IRedirector redirector)
		{
			_pageBuilder = pageBuilder;
			_responseWriter = responseWriter;
			_redirector = redirector;
		}

		/// <summary>
		/// Processes (build web-page and send to client, process current page state) the current web-page
		/// </summary>
		/// <param name="resolver">The DI container resolver.</param>
		/// <param name="context">The context.</param>
		public async Task<RequestHandlingStatus> ProcessPage(IDIResolver resolver, HttpContext context)
		{
			context.Response.ContentType = "text/html";
			_redirector.PreviousPageUrl = context.Request.GetEncodedUrl();

			await _responseWriter.WriteAsync(_pageBuilder.Build(resolver), context.Response);

			return RequestHandlingStatus.RequestWasHandled;
		}
	}
}