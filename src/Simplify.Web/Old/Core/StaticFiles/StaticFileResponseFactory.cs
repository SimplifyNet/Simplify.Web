using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Old.Core.StaticFiles;

/// <summary>
/// Provides static file response factory.
/// </summary>
/// <seealso cref="IStaticFileResponseFactory" />
/// <remarks>
/// Initializes a new instance of the <see cref="StaticFileResponseFactory"/> class.
/// </remarks>
/// <param name="responseWriter">The response writer.</param>
public class StaticFileResponseFactory(IResponseWriter responseWriter) : IStaticFileResponseFactory
{
	/// <summary>
	/// Creates the static file response.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	public IStaticFileResponse Create(HttpResponse response) => new StaticFileResponse(response, responseWriter);
}