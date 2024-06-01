using System;

namespace Simplify.Web.StaticFiles.Context;

/// <summary>
/// Provides the static file processing context.
/// </summary>
/// <seealso cref="IStaticFileProcessingContext" />
public class StaticFileProcessingContext(string relativeFilePath,
	DateTime lastModificationTime,
	bool isCached) : IStaticFileProcessingContext
{
	/// <summary>
	/// Gets the relative file path of request.
	/// </summary>
	/// <value>
	/// The relative file path.
	/// </value>
	public string RelativeFilePath { get; } = relativeFilePath;

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	/// <value>
	/// The last modification time.
	/// </value>
	public DateTime LastModificationTime { get; } = lastModificationTime;

	/// <summary>
	/// Determines whether the file can be used from cached.
	/// </summary>
	/// <value>
	///   <c>true</c> if file can be cached; otherwise, <c>false</c>.
	/// </value>
	public bool CanBeCached { get; } = isCached;
}