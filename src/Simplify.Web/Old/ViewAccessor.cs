using Simplify.DI;
using Simplify.Web.Old.Core.Views;

namespace Simplify.Web.Old;

/// <summary>
/// View accessor base class.
/// </summary>
public abstract class ViewAccessor
{
	internal IViewFactory ViewFactory { get; set; } = null!;
	internal IDIResolver Resolver { get; set; } = null!;

	/// <summary>
	/// Gets view instance.
	/// </summary>
	/// <typeparam name="T">View instance to get</typeparam>
	/// <returns>View instance.</returns>
	public T GetView<T>()
		where T : View
	{
		var type = typeof(T);

		return (T)ViewFactory.CreateView(type, Resolver);
	}
}