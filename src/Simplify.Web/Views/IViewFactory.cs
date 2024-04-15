using System;

namespace Simplify.Web.Views;

/// <summary>
/// Represents a view factory.
/// </summary>
public interface IViewFactory
{
	/// <summary>
	/// Creates the view.
	/// </summary>
	/// <param name="viewType">Type of the view.</param>
	View CreateView(Type viewType);
}