﻿using System.Collections.Generic;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.PageComposition.Stages;

public class StringTableItemsInjectionStage(IStringTable stringTable) : IPageCompositionStage
{
	private const string StringTablePrefix = "StringTable.";

	private readonly IStringTable _stringTable = stringTable;

	public void Execute(IDataCollector dataCollector)
	{
		foreach (var item in (IDictionary<string, object>)_stringTable.Items)
			dataCollector.Add(StringTablePrefix + item.Key, item.Value.ToString());
	}
}