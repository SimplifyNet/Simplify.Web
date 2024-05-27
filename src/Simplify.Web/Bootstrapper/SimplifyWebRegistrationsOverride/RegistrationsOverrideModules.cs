using System;
using Simplify.DI;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web modules override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IDataCollector` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideDataCollector(Action<IDIRegistrator> action) => AddAction<IDataCollector>(action);

	/// <summary>
	/// Overrides the `IDynamicEnvironment` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideDynamicEnvironment(Action<IDIRegistrator> action) => AddAction<IDynamicEnvironment>(action);

	/// <summary>
	/// Overrides the `IEnvironment` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideEnvironment(Action<IDIRegistrator> action) => AddAction<IEnvironment>(action);

	/// <summary>
	/// Overrides the `IFileReader` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideFileReader(Action<IDIRegistrator> action) => AddAction<IFileReader>(action);

	/// <summary>
	/// Overrides the `ILanguageManagerProvider` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideLanguageManagerProvider(Action<IDIRegistrator> action) => AddAction<ILanguageManagerProvider>(action);

	/// <summary>
	/// Overrides the `IListsGenerator` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideListsGenerator(Action<IDIRegistrator> action) => AddAction<IListsGenerator>(action);

	/// <summary>
	/// Overrides the `IRedirector` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideRedirector(Action<IDIRegistrator> action) => AddAction<IRedirector>(action);

	/// <summary>
	/// Overrides the `IStringTable` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideStringTable(Action<IDIRegistrator> action) => AddAction<IStringTable>(action);

	/// <summary>
	/// Overrides the `ITemplateFactory` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideTemplateFactory(Action<IDIRegistrator> action) => AddAction<ITemplateFactory>(action);

	/// <summary>
	/// Overrides the `IWebContextProvider` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideWebContextProvider(Action<IDIRegistrator> action) => AddAction<IWebContextProvider>(action);
}