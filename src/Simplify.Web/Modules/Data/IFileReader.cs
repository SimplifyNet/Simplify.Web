using System.Xml.Linq;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Represents localizable files reader
/// </summary>
public interface IFileReader
{
	/// <summary>
	/// Setups the file reader.
	/// </summary>
	void Setup();

	/// <summary>
	/// Load xml document from a file located in data folder
	/// </summary>
	/// <param name="fileName">File name</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	/// <returns>Xml document</returns>
	XDocument? LoadXDocument(string fileName, bool memoryCache = false);

	/// <summary>
	/// Load xml document from a file with specific language located in data folder
	/// </summary>
	/// <param name="fileName">File name</param>
	/// <param name="language">File language</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	/// <returns>Xml document</returns>
	XDocument? LoadXDocument(string fileName, string language, bool memoryCache = false);

	/// <summary>
	/// Load text from a file located in data folder
	/// </summary>
	/// <param name="fileName">File name</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	/// <returns>Text from a file</returns>
	string? LoadTextDocument(string fileName, bool memoryCache = false);

	/// <summary>
	/// Load text from a file with specific language located in data folder
	/// </summary>
	/// <param name="fileName">File name</param>
	/// <param name="language">File language</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	/// <returns>Text from a file</returns>
	string? LoadTextDocument(string fileName, string language, bool memoryCache = false);
}