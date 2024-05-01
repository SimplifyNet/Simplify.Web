using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http.RequestTime;

public static class HeaderTimeExtensions
{
	/// <summary>
	/// Gets If-Modified-Since time header from headers collection.
	/// </summary>
	/// <param name="headers">The HTTP headers.</param>
	public static DateTime? GetIfModifiedSinceTime(this IHeaderDictionary headers)
	{
		DateTime? ifModifiedSinceTime = null;

		if (headers.ContainsKey("If-Modified-Since"))
			ifModifiedSinceTime = DateTime.ParseExact(headers["If-Modified-Since"], "r",
				CultureInfo.InvariantCulture);

		return ifModifiedSinceTime;
	}
}