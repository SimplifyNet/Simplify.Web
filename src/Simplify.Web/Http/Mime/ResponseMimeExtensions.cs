using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.Mime;

public static class ResponseMimeExtensions
{
	public static void SetContentMimeType(this HttpResponse response, string fileName) =>
		response.ContentType = MimeTypeAssistant.GetMimeTypeByFilePath(fileName);
}