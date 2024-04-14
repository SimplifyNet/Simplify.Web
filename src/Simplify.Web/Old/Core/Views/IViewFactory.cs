using System;
using Simplify.DI;

namespace Simplify.Web.Old.Core.Views;

/// <summary>
/// Represent view factory.
/// </summary>
public interface IViewFactory
{
	/// <summary>
	/// Creates the view.
	/// </summary>
	/// <param name="viewType">Type of the view.</param>
	/// <param name="resolver">The DI container resolver.</param>
	/// <returns></returns>
	View CreateView(Type viewType, IDIResolver resolver);
}