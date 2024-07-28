using System.Collections.Concurrent;

namespace Simplify.Web.StaticFiles.Cache;

/// <summary>
/// Provides static files in-memory cache.
/// </summary>
public static class FilesInMemoryCache
{
	// /// <summary>
	// /// Gets the static files in-memory cache data.
	// /// </summary>
	// /// <value>
	// /// The static files in-memory cache data..
	// /// </value>
	// public static IDictionary<string, byte[]> Data { get; set; } = new Dictionary<string, byte[]>();

	/// <summary>
	/// Gets the static files in-memory cache data.
	/// </summary>
	public static readonly ConcurrentDictionary<string, byte[]> Items = new();
}