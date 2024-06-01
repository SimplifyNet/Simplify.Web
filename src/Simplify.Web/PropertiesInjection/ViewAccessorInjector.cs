using Simplify.DI;
using Simplify.Web.Views;

namespace Simplify.Web.PropertiesInjection;

/// <summary>
/// Provides the view accessor injector.
/// </summary>
public abstract class ViewAccessorInjector(IDIResolver resolver)
{
	/// <summary>
	/// Builds the view accessor properties.
	/// </summary>
	/// <param name="viewAccessor">The view accessor.</param>
	protected void InjectViewAccessorProperties(ViewAccessor viewAccessor)
	{
		viewAccessor.Resolver = resolver;
		viewAccessor.ViewFactory = resolver.Resolve<IViewFactory>();
	}
}