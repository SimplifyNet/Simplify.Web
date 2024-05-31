using System;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.RequestTime;

namespace Simplify.Web.Http.Cache;

/// <summary>
/// Provides the request cache extensions.
/// </summary>
public static class RequestCacheExtensions
{
	/// <summary>
	/// Determines whether a  file can be used from cache.
	/// </summary>
	/// <param name="request">The request.</param>
	/// <param name="fileLastModifiedTime">The file last modified time.</param>
	/// <returns>
	///   <c>true</c> if a  file can be used from cache; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsFileCanBeUsedFromCache(this HttpRequest request, DateTime fileLastModifiedTime) =>
		IsFileCanBeUsedFromCache(request.Headers["Cache-Control"]!, request.Headers.GetIfModifiedSinceTime(), fileLastModifiedTime);

	private static bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime) =>
		!cacheControlHeader.IsNoCacheRequested()
			&& ifModifiedSinceHeader != null
			&& fileLastModifiedTime <= ifModifiedSinceHeader.Value;
}