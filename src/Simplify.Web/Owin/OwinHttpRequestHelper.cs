using System;
using System.Globalization;
using Microsoft.Owin;

namespace Simplify.Web.Owin;

/// <summary>
/// Provides OWIN and HTTP related utility functions
/// </summary>
public static class OwinHttpRequestHelper
{
	/// <summary>
	/// Gets If-Modified-Since time header from headers collection.
	/// </summary>
	/// <param name="headers">The headers.</param>
	/// <returns></returns>
	public static DateTime? GetIfModifiedSinceTime(IHeaderDictionary headers)
	{
		DateTime? ifModifiedSinceTime = null;

		if (headers.ContainsKey("If-Modified-Since"))
			ifModifiedSinceTime = DateTime.ParseExact(headers["If-Modified-Since"], "r",
				CultureInfo.InvariantCulture);

		return ifModifiedSinceTime;
	}

	/// <summary>
	/// Determines whether no cache is requested
	/// </summary>
	/// <param name="cacheControlHeader">The cache control header.</param>
	/// <returns></returns>
	public static bool IsNoCacheRequested(string cacheControlHeader)
	{
		return !string.IsNullOrEmpty(cacheControlHeader) && cacheControlHeader.Contains("no-cache");
	}

	/// <summary>
	/// Gets the relative file path of request.
	/// </summary>
	/// <param name="request">The request.</param>
	/// <returns></returns>
	public static string GetRelativeFilePath(IOwinRequest request)
	{
		return request.Path.ToString().Substring(1);
	}
}