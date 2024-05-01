namespace Simplify.Web.Http.Cache;

/// <summary>
/// Provides the cache control header extensions
/// </summary>
public static class CacheControlHeaderExtensions
{
	/// <summary>
	/// Determines whether no cache is requested
	/// </summary>
	/// <param name="cacheControlHeader">The cache control header.</param>
	public static bool IsNoCacheRequested(this string cacheControlHeader) =>
		!string.IsNullOrEmpty(cacheControlHeader) && cacheControlHeader.Contains("no-cache");
}