using Simplify.DI;
using Simplify.Web.Core.Views;

namespace Simplify.Web.Core2.Controllers.Response.Injectors;

/// <summary>
/// Provides builder for ViewAccessor objects construction.
/// </summary>
public abstract class ViewAccessorInjector(IDIResolver resolver)
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Builds the view accessor properties.
	/// </summary>
	/// <param name="viewAccessor">The view accessor.</param>
	protected void InjectViewAccessorProperties(ViewAccessor viewAccessor)
	{
		viewAccessor.Resolver = _resolver;
		viewAccessor.ViewFactory = _resolver.Resolve<IViewFactory>();
	}
}