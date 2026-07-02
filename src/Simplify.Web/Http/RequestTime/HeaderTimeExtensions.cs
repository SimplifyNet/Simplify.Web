using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.RequestTime;

/// <summary>
/// Provides the header time extensions.
/// </summary>
public static class HeaderTimeExtensions
{
	/// <summary>
	/// Gets If-Modified-Since time header from headers collection.
	/// </summary>
	/// <param name="headers">The HTTP headers.</param>
	public static DateTime? GetIfModifiedSinceTime(this IHeaderDictionary headers)
	{
		if (headers.ContainsKey("If-Modified-Since") &&
			DateTime.TryParseExact(headers["If-Modified-Since"], "r", CultureInfo.InvariantCulture, DateTimeStyles.None,
				out var ifModifiedSinceTime))
			return ifModifiedSinceTime;

		return null;
	}
}