﻿using Simplify.DI;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Core.PageAssembly;

/// <summary>
/// Provides web-page builder
/// </summary>
public class PageBuilder : IPageBuilder
{
	private readonly ITemplateFactory _templateFactory;
	private readonly IDataCollector _dataCollector;

	/// <summary>
	/// Initializes a new instance of the <see cref="PageBuilder" /> class.
	/// </summary>
	/// <param name="templateFactory">The template factory.</param>
	/// <param name="dataCollector">The data collector.</param>
	public PageBuilder(ITemplateFactory templateFactory, IDataCollector dataCollector)
	{
		_templateFactory = templateFactory;
		_dataCollector = dataCollector;
	}

	/// <summary>
	/// Builds a web page
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <returns></returns>
	public string Build(IDIResolver resolver)
	{
		resolver.Resolve<IStringTableItemsSetter>().Set();
		resolver.Resolve<IContextVariablesSetter>().SetVariables(resolver);

		var tpl = _templateFactory.Load(resolver.Resolve<IEnvironment>().MasterTemplateFileName);

		foreach (var item in _dataCollector.Items.Keys)
			tpl.Set(item, _dataCollector.Items[item]);

		return tpl.Get();
	}
}