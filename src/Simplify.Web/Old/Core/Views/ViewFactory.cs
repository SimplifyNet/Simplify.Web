using System;
using Simplify.DI;
using Simplify.Web.Core.AccessorsBuilding;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Core.Views;

/// <summary>
/// View factory.
/// </summary>
public class ViewFactory : ModulesAccessorBuilder, IViewFactory
{
	/// <summary>
	/// Creates the view.
	/// </summary>
	/// <param name="viewType">Type of the view.</param>
	/// <param name="resolver"></param>
	/// <returns></returns>
	public View CreateView(Type viewType, IDIResolver resolver)
	{
		var view = (View)resolver.Resolve(viewType);

		BuildModulesAccessorProperties(view, resolver);

		view.Language = resolver.Resolve<ILanguageManagerProvider>().Get().Language;
		view.SiteUrl = resolver.Resolve<IWebContextProvider>().Get().SiteUrl;
		view.StringTable = resolver.Resolve<IStringTable>().Items;

		return view;
	}
}