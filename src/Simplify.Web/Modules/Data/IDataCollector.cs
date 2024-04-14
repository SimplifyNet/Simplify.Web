using System.Collections.Generic;
using Simplify.Templates;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Represents a web-site master page data collector.
/// </summary>
public interface IDataCollector
{
	/// <summary>
	/// Gets the name of the title variable.
	/// </summary>
	string TitleVariableName { get; }

	/// <summary>
	/// Gets a data collector items which will be inserted into master template file.
	/// </summary>
	IDictionary<string, string> Items { get; }

	/// <summary>
	/// List of a data collector items.
	/// </summary>
	/// <param name="key">Item name.</param>
	string this[string key] { get; }

	/// <summary>
	///  Set the template variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="variableName">Variable name in master template file.</param>
	/// <param name="value">Value to set.</param>
	void Add(string variableName, string? value);

	/// <summary>
	/// Set the template variable value with a data from template (all occurrences will be replaced).
	/// </summary>
	/// <param name="variableName">Variable name in master template file.</param>
	/// <param name="template">The template.</param>
	void Add(string variableName, ITemplate? template);

	/// <summary>
	/// Set the template main content variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="value">Value to set.</param>
	void Add(string? value);

	/// <summary>
	/// Set the template main content variable value with a data from template (all occurrences will be replaced).
	/// </summary>
	/// <param name="template">The template.</param>
	void Add(ITemplate template);

	/// <summary>
	/// Set the template title variable value (all occurrences will be replaced).
	/// </summary>
	/// <param name="value">Value to set.</param>
	void AddTitle(string? value);

	/// <summary>
	/// Set the template variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="stringTableKey">StringTable key.</param>
	/// <param name="variableName">Variable name in master template file.</param>
	void AddSt(string variableName, string stringTableKey);

	/// <summary>
	/// Set the template main content variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="stringTableKey">StringTable key.</param>
	void AddSt(string stringTableKey);

	/// <summary>
	/// Set the template title variable value from StringTable (all occurrences will be replaced).
	/// </summary>
	/// <param name="stringTableKey">StringTable key.</param>
	void AddTitleSt(string stringTableKey);

	/// <summary>
	/// Checking if some variable data is already exist in a data collector.
	/// </summary>
	/// <param name="variableName">Variable name.</param>
	bool IsDataExist(string variableName);
}