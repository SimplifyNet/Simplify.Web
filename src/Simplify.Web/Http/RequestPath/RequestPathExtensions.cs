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

#if NETSTANDARD2_0
		return request.Path.Value.Substring(1);
#else
		return request.Path.Value[1..];
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