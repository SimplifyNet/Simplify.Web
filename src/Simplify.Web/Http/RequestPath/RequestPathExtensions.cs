using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.RequestPath;

/// <summary>
/// Provides the request path extensions.
/// </summary>
public static class RequestPathExtensions
{
	/// <summary>
	/// Gets the relative file path.
	/// </summary>
	/// <param name="request">The request.</param>
	public static string GetRelativeFilePath(this HttpRequest request)
	{
		if (string.IsNullOrEmpty(request.Path.Value))
			return "";

		var path = request.Path.Value;

		// Normalize multiple leading slashes (e.g. "//etc/passwd" -> "/etc/passwd")
		// to prevent path traversal attempts through URL encoding tricks.
		var startIndex = 0;

		while (startIndex < path.Length && path[startIndex] == '/')
			startIndex++;

		if (startIndex >= path.Length)
			return "";

#if NETSTANDARD2_0
		return path.Substring(startIndex);
#else
		return path[startIndex..];
#endif
	}

	/// <summary>
	/// Gets the split path from string.
	/// </summary>
	/// <param name="path">The path.</param>
	public static IList<string> GetSplitPath(this string? path) =>
		path != null
			? path!.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
			: [];
}