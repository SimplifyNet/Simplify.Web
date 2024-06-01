using System;
using System.Collections.Generic;
using Simplify.Templates;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Provides the data collector.
/// </summary>
/// <seealso cref="IDataCollector" />
public class DataCollector(string mainContentVariableName, string titleVariableName, IStringTable stringTable) : IDataCollector
{
	/// <summary>
	/// Gets the name of the title variable.
	/// </summary>
	/// <value>
	/// The name of the title variable.
	/// </value>
	public string TitleVariableName { get; } = titleVariableName;

	/// <summary>
	/// Gets a data collector items which will be inserted into master template file.
	/// </summary>
	/// <value>
	/// The items.
	/// </value>
	public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

	/// <summary>
	/// Gets the <see cref="string"/> with the specified key.
	/// </summary>
	/// <value>
	/// The <see cref="string"/>.
	/// </value>
	/// <param name="key">The key.</param>
	public string this[string key] => Items[key];

	/// <summary>
	/// Set the template variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="variableName">Variable name in master template file.</param>
	/// <param name="value">Value to set.</param>
	/// <exception cref="ArgumentException">Value cannot be null or empty. - variableName</exception>
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
	/// Set the template variable value with a data from template (all occurrences will be replaced).
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
	/// Set the template main content variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="value">Value to set.</param>
	public void Add(string? value) => Add(mainContentVariableName, value);

	/// <summary>
	/// Set the template main content variable value with a data from template (all occurrences will be replaced).
	/// </summary>
	/// <param name="template">The template.</param>
	public void Add(ITemplate template) => Add(mainContentVariableName, template);

	/// <summary>
	/// Set the template title variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="value">Value to set.</param>
	public void AddTitle(string? value) => Add(TitleVariableName, value);

	/// <summary>
	/// Set the template variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="variableName">Variable name in master template file.</param>
	/// <param name="stringTableKey">StringTable key.</param>
	public void AddSt(string variableName, string stringTableKey) => Add(variableName, stringTable.GetItem(stringTableKey));

	/// <summary>
	/// Set the template main content variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="stringTableKey">StringTable key.</param>
	public void AddSt(string stringTableKey) => Add(stringTable.GetItem(stringTableKey));

	/// <summary>
	/// Set the template title variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="stringTableKey">StringTable key.</param>
	public void AddTitleSt(string stringTableKey) => AddTitle(stringTable.GetItem(stringTableKey));

	/// <summary>
	/// Checking if some variable data is already exist in a data collector.
	/// </summary>
	/// <param name="variableName">Variable name.</param>
	/// <returns>
	///   <c>true</c> if the variable name exists; otherwise, <c>false</c>.
	/// </returns>
	public bool IsDataExist(string variableName) => Items.ContainsKey(variableName);
}