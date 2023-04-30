using Simplify.DI;

namespace Simplify.Web.Core.PageAssembly;

/// <summary>
/// Represent web-page builder
/// </summary>
public interface IPageBuilder
{
	/// <summary>
	/// Buids a web page
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <returns></returns>
	string Build(IDIResolver resolver);
}