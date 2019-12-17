using System.Threading.Tasks;
using Simplify.Templates;

namespace Simplify.Web.Modules.Data
{
	/// <summary>
	/// Represent web-site text templates loader
	/// </summary>
	public interface ITemplateFactory
	{
		/// <summary>
		/// Setups the template factory.
		/// </summary>
		void Setup();

		/// <summary>
		/// Load template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		ITemplate Load(string fileName);

		/// <summary>
		/// Load web-site template from a file asynchronously.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <returns></returns>
		Task<ITemplate> LoadAsync(string fileName);
	}
}