using System.Threading.Tasks;
using Simplify.Templates;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Represents a web-site text templates loader.
/// </summary>
public interface ITemplateFactory
{
	/// <summary>
	/// Setups a template factory.
	/// </summary>
	void Setup();

	/// <summary>
	/// Loads s template from a file.
	/// </summary>
	/// <param name="fileName">Template file name.</param>
	/// <returns>Template class with loaded template.</returns>
	ITemplate Load(string fileName);

	/// <summary>
	/// Loads a web-site template from a file asynchronously.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	Task<ITemplate> LoadAsync(string fileName);
}