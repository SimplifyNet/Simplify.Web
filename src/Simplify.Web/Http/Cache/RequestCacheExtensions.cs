using System;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.RequestTime;

namespace Simplify.Web.Http.Cache;

public static class RequestCacheExtensions
{
	public static bool IsFileCanBeUsedFromCache(this HttpRequest request, DateTime fileLastModifiedTime) =>
		IsFileCanBeUsedFromCache(request.Headers["Cache-Control"], request.Headers.GetIfModifiedSinceTime(), fileLastModifiedTime);

	private static bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime) =>
		!cacheControlHeader.IsNoCacheRequested()
			&& ifModifiedSinceHeader != null
			&& fileLastModifiedTime <= ifModifiedSinceHeader.Value;
}