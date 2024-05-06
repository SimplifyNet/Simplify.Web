using System;
using Simplify.DI;
using Simplify.Web.Controllers.Response.Injection;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;

namespace Simplify.Web.Views;

/// <summary>
/// Provides the view factory.
/// </summary>
public class ViewFactory(IDIResolver resolver) : ModulesAccessorInjector(resolver), IViewFactory
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Creates the view.
	/// </summary>
	/// <param name="viewType">Type of the view.</param>
	public View CreateView(Type viewType)
	{
		var view = (View)_resolver.Resolve(viewType);

		InjectModulesAccessorProperties(view);

		view.Language = _resolver.Resolve<ILanguageManagerProvider>().Get().Language;
		view.SiteUrl = _resolver.Resolve<IWebContextProvider>().Get().SiteUrl;
		view.StringTable = _resolver.Resolve<IStringTable>().Items;

		return view;
	}
}