using System;

namespace Simplify.Web.StaticFiles.Context;

/// <summary>
/// Represents a static file processing context.
/// </summary>
public interface IStaticFileProcessingContext
{
	/// <summary>
	/// Gets the relative file path of request.
	/// </summary>
	/// <value>
	/// The relative file path.
	/// </value>
	string RelativeFilePath { get; }

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	/// <value>
	/// The last modification time.
	/// </value>
	DateTime LastModificationTime { get; }

	/// <summary>
	/// Determines whether the file can be used from cached.
	/// </summary>
	/// <value>
	///   <c>true</c> if file can be cached; otherwise, <c>false</c>.
	/// </value>
	bool CanBeCached { get; }
}