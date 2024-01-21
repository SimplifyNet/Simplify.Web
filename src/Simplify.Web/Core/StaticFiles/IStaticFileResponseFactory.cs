using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Core.StaticFiles;

/// <summary>
/// Represent static file response factory.
/// </summary>
public interface IStaticFileResponseFactory
{
	/// <summary>
	/// Creates the static file response.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <returns></returns>
	IStaticFileResponse Create(HttpResponse response);
}