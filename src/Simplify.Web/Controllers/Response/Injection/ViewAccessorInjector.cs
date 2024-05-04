using Simplify.DI;
using Simplify.Web.Views;

namespace Simplify.Web.Controllers.Response.Injection;

/// <summary>
/// Provides the builder for ViewAccessor objects construction.
/// </summary>
public abstract class ViewAccessorInjector(IDIResolver resolver)
{
	/// <summary>
	/// Builds the view accessor properties.
	/// </summary>
	/// <param name="viewAccessor">The view accessor.</param>
	protected void InjectViewAccessorProperties(ViewAccessor viewAccessor)
	{
		viewAccessor.ViewFactory = resolver.Resolve<IViewFactory>();
	}
}