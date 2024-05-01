using System;
using System.Threading.Tasks;

namespace Simplify.Web.StaticFiles.IO;

/// <summary>
/// Represents a static file.
/// </summary>
public interface IStaticFile
{
	/// <summary>
	/// Determines whether relative file path is a path for existing file and it is valid
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	bool IsValidPath(string relativeFilePath);

	/// <summary>
	/// Gets the file last modification time.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	DateTime GetLastModificationTime(string relativeFilePath);

	/// <summary>
	/// Gets the file data.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	/// <returns></returns>
	Task<byte[]> GetDataAsync(string relativeFilePath);
}