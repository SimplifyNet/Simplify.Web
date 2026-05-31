using System;
using System.Collections.Concurrent;

namespace Simplify.Web.StaticFiles.Cache;

/// <summary>
/// Provides static files in-memory cache.
/// </summary>
public static class FilesInMemoryCache
{
	/// <summary>
	/// Maximum number of entries kept in the in-memory cache.
	/// Once exceeded, additional files are served without being cached to avoid
	/// unbounded memory growth that can be triggered by request-path variants.
	/// </summary>
	public static int MaxItems { get; set; } = 1024;

	/// <summary>
	/// Gets the static files in-memory cache data.
	/// </summary>
	public static readonly ConcurrentDictionary<string, CachedFile> Items =
		new(StringComparer.OrdinalIgnoreCase);

	/// <summary>
	/// Represents a cached static file together with the modification timestamp it was
	/// captured at, so that stale entries can be detected on subsequent requests.
	/// </summary>
	public sealed class CachedFile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CachedFile"/> class.
		/// </summary>
		public CachedFile(byte[] data, DateTime lastModificationTime)
		{
			Data = data;
			LastModificationTime = lastModificationTime;
		}

		/// <summary>
		/// Gets the cached file data.
		/// </summary>
		public byte[] Data { get; }

		/// <summary>
		/// Gets the last modification time captured when the entry was added to the cache.
		/// </summary>
		public DateTime LastModificationTime { get; }
	}
}