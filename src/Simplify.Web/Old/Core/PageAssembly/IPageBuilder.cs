using Simplify.DI;

namespace Simplify.Web.Old.Core.PageAssembly;

/// <summary>
/// Represent web-page builder.
/// </summary>
public interface IPageBuilder
{
	/// <summary>
	/// Builds the web page.
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <returns></returns>
	string Build(IDIResolver resolver);
}