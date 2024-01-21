using System;
using System.Collections.Generic;
using Simplify.Templates;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Provides master page data collector.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataCollector"/> class.
/// </remarks>
/// <param name="mainContentVariableName">Name of the main content variable.</param>
/// <param name="titleVariableName">Name of the title variable.</param>
/// <param name="stringTable">The string table.</param>
public class DataCollector(string mainContentVariableName, string titleVariableName, IStringTable stringTable) : IDataCollector
{
	private readonly string _mainContentVariableName = mainContentVariableName;
	private readonly IStringTable _stringTable = stringTable;

	/// <summary>
	/// Gets the name of the title variable.
	/// </summary>
	/// <value>
	/// The name of the title variable.
	/// </value>
	public string TitleVariableName { get; } = titleVariableName;

	/// <summary>
	/// Gets the data collector items which will be inserted into master template file.
	/// </summary>
	public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

	/// <summary>
	/// List of data collector items.
	/// </summary>
	/// <param name="key">Item name.</param>
	/// <returns>Data collector item.</returns>
	public string this[string key] => Items[key];

	/// <summary>
	/// Set template variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="variableName">Variable name in master template file.</param>
	/// <param name="value">Value to set.</param>
	public void Add(string? variableName, string? value)
	{
		if (string.IsNullOrEmpty(variableName))
			throw new ArgumentException("Value cannot be null or empty.", nameof(variableName));

		value ??= "";

		if (!Items.ContainsKey(variableName!))
		{
			Items.Add(variableName!, value);
			return;
		}

		Items[variableName!] += value;
	}

	/// <summary>
	/// Set template variable value with data from template (all occurrences will be replaced).
	/// </summary>
	/// <param name="variableName">Variable name in master template file.</param>
	/// <param name="template">The template.</param>
	public void Add(string variableName, ITemplate? template)
	{
		if (template == null)
			return;

		Add(variableName, template.Get());
	}

	/// <summary>
	/// Set template main content variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="value">Value to set.</param>
	public void Add(string? value) => Add(_mainContentVariableName, value);

	/// <summary>
	/// Set template main content variable value with data from template (all occurrences will be replaced).
	/// </summary>
	/// <param name="template">The template.</param>
	public void Add(ITemplate template) => Add(_mainContentVariableName, template);

	/// <summary>
	/// Set template title variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="value">Value to set.</param>
	public void AddTitle(string? value) => Add(TitleVariableName, value);

	/// <summary>
	/// Set template variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="variableName">Variable name in master template file.</param>
	/// <param name="stringTableKey">StringTable key.</param>
	public void AddSt(string variableName, string stringTableKey) => Add(variableName, _stringTable.GetItem(stringTableKey));

	/// <summary>
	/// Set template main content variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="stringTableKey">StringTable key.</param>
	public void AddSt(string stringTableKey) => Add(_stringTable.GetItem(stringTableKey));

	/// <summary>
	/// Set template title variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="stringTableKey">StringTable key.</param>
	public void AddTitleSt(string stringTableKey) => AddTitle(_stringTable.GetItem(stringTableKey));

	/// <summary>
	/// Checking if some variable data is already exist in a data collector.
	/// </summary>
	/// <param name="variableName">Variable name.</param>
	/// <returns></returns>
	public bool IsDataExist(string variableName) => Items.ContainsKey(variableName);
}