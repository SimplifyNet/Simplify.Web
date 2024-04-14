using System.Xml.Linq;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Represents a localizable files reader.
/// </summary>
public interface IFileReader
{
	/// <summary>
	/// Setups a file reader.
	/// </summary>
	void Setup();

	/// <summary>
	/// Loads a xml document from a file located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	XDocument? LoadXDocument(string fileName, bool memoryCache = false);

	/// <summary>
	/// Loads a xml document from a file with specific language located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="language">File language.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	XDocument? LoadXDocument(string fileName, string language, bool memoryCache = false);

	/// <summary>
	/// Loads a text from a file located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	string? LoadTextDocument(string fileName, bool memoryCache = false);

	/// <summary>
	/// Load a text from a file with specific language located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="language">File language.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	string? LoadTextDocument(string fileName, string language, bool memoryCache = false);
}