using System;
using System.IO;
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
	/// Gets the file data asynchronously.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	Task<byte[]> GetDataAsync(string relativeFilePath);

	/// <summary>
	/// Gets the file data.
	/// </summary>
	/// <param name="relativeFilePath">The relative file path.</param>
	byte[] GetData(string relativeFilePath);

	/// <summary>
	/// Copies the file content to the specified target stream asynchronously.
	/// Avoids loading the entire file into memory.
	/// </summary>
	/// <param name="target">The target stream to write to.</param>
	/// <param name="relativeFilePath">The relative file path.</param>
	Task CopyToAsync(Stream target, string relativeFilePath);
}