using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.Mime;

/// <summary>
/// Provides the response MIME extensions.
/// </summary>
public static class ResponseMimeExtensions
{
	/// <summary>
	/// Sets the type of the content MIME.
	/// </summary>
	/// <param name="response">The response.</param>
	/// <param name="fileName">Name of the file.</param>
	public static void SetContentMimeType(this HttpResponse response, string fileName) =>
		response.ContentType = MimeTypeAssistant.GetMimeTypeByFilePath(fileName);
}