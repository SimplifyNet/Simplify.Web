﻿using Simplify.DI;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Modules.Environment;

namespace Simplify.Web.Controllers.Response.Injectors;

/// <summary>
/// Provides the builder for ModulesAccessor objects.
/// </summary>
public abstract class ModulesAccessorInjector(IDIResolver resolver) : ViewAccessorInjector(resolver)
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Builds the modules accessor properties.
	/// </summary>
	/// <param name="modulesAccessor">The modules accessor.</param>
	protected void InjectModulesAccessorProperties(ModulesAccessor modulesAccessor)
	{
		InjectViewAccessorProperties(modulesAccessor);

		modulesAccessor.Environment = _resolver.Resolve<IEnvironment>();
		modulesAccessor.StringTableManager = _resolver.Resolve<IStringTable>();
		modulesAccessor.TemplateFactory = _resolver.Resolve<ITemplateFactory>();

		modulesAccessor.Html = new HtmlWrapper
		{
			ListsGenerator = _resolver.Resolve<IListsGenerator>()
		};
	}
}