using System;
using System.Collections.Generic;
using Simplify.Web.Old.Modules.Data;

namespace Simplify.Web.Old.Core.PageAssembly;

/// <summary>
/// Provides string table items setter.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="StringTableItemsSetter"/> class.
/// </remarks>
/// <param name="dataCollector">The data collector.</param>
/// <param name="stringTable">The string table.</param>
public class StringTableItemsSetter(IDataCollector dataCollector, IStringTable stringTable) : IStringTableItemsSetter
{
	private const string StringTablePrefix = "StringTable.";

	/// <summary>
	/// Sets this items from string table to data collector.
	/// </summary>
	public void Set()
	{
		foreach (var item in (IDictionary<string, Object>)stringTable.Items)
			dataCollector.Add(StringTablePrefix + item.Key, item.Value.ToString());
	}
}