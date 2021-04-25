using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Core.StaticFiles
{
	/// <summary>
	/// Provides static file response factory
	/// </summary>
	/// <seealso cref="IStaticFileResponseFactory" />
	public class StaticFileResponseFactory : IStaticFileResponseFactory
	{
		private readonly IResponseWriter _responseWriter;

		/// <summary>
		/// Initializes a new instance of the <see cref="StaticFileResponseFactory"/> class.
		/// </summary>
		/// <param name="responseWriter">The response writer.</param>
		public StaticFileResponseFactory(IResponseWriter responseWriter) => _responseWriter = responseWriter;

		/// <summary>
		/// Creates the static file response.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public IStaticFileResponse Create(HttpResponse response) => new StaticFileResponse(response, _responseWriter);
	}
}