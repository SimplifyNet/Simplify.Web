using System;
using Microsoft.AspNetCore.Http;
using Simplify.System;
using Simplify.Web.Http.Mime;
using Simplify.Web.Http.ResponseTime;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles;

/// <summary>
/// Provides the static file response extensions.
/// </summary>
public static class StaticFileResponseExtensions
{
	/// <summary>
	/// Sets the new returning file attributes.
	/// </summary>
	/// <param name="response">The HTTP response.</param>
	/// <param name="context">The static file processing context.</param>
	public static void SetNewReturningFileAttributes(this HttpResponse response, IStaticFileProcessingContext context)
	{
		response.SetContentMimeType(context.RelativeFilePath);
		response.SetLastModifiedTime(context.LastModificationTime);
		response.Headers["Expires"] = new DateTimeOffset(TimeProvider.Current.Now.AddYears(1)).ToString("R");
	}
}