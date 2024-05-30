using Simplify.DI;
using Simplify.Web.Views;

namespace Simplify.Web;

/// <summary>
/// Provides the view accessor base class.
/// </summary>
public abstract class ViewAccessor
{
	internal IViewFactory ViewFactory { get; set; } = null!;

	internal IDIResolver Resolver { get; set; } = null!;

	/// <summary>
	/// Gets a view instance.
	/// </summary>
	/// <typeparam name="T">View type</typeparam>
	public T GetView<T>()
		where T : View =>
		(T)ViewFactory.CreateView(typeof(T));
}