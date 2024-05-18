using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.RequestPath;

public static class RequestPathExtensions
{
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

	public static IList<string> GetSplitPath(this string? path) =>
		path != null
			? path!.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
			: [];
}