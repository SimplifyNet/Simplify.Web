using System;

namespace Simplify.Web.StaticFiles;

public interface IStaticFileProcessingContext
{
	/// <summary>
	/// Gets the relative file path of request.
	/// </summary>
	string RelativeFilePath { get; }

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	DateTime LastModificationTime { get; }

	/// <summary>
	/// Determines whether the file can be used from cached.
	/// </summary>
	bool IsCached { get; }
}
