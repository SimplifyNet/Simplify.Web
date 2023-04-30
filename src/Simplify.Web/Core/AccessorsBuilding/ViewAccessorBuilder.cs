using Simplify.DI;
using Simplify.Web.Core.Views;

namespace Simplify.Web.Core.AccessorsBuilding;

/// <summary>
/// Provides builder for ViewAccessor objects construction
/// </summary>
public abstract class ViewAccessorBuilder
{
	/// <summary>
	/// Builds the view accessor properties.
	/// </summary>
	/// <param name="viewAccessor">The view accessor.</param>
	/// <param name="resolver">The DI container resolver.</param>
	protected void BuildViewAccessorProperties(ViewAccessor viewAccessor, IDIResolver resolver)
	{
		viewAccessor.Resolver = resolver;
		viewAccessor.ViewFactory = resolver.Resolve<IViewFactory>();
	}
}