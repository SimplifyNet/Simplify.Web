using System;

namespace Simplify.Web.StaticFiles;

public interface IStaticFileProcessingContext
{
	/// <summary>
	/// Gets If-Modified-Since time header from headers collection.
	/// </summary>
	DateTime? GetIfModifiedSinceTime { get; }

	/// <summary>
	/// Determines whether the file can be used from cached.
	/// </summary>
	bool IsCached { get; }

	/// <summary>
	/// Gets the relative file path of request.
	/// </summary>
	string RelativeFilePath { get; }

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	DateTime FileLastModificationTime { get; }
}
