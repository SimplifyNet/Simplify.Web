using System;
using System.Collections.Generic;
using Simplify.Templates;

namespace Simplify.Web.Modules.Data;

public class DataCollector(string mainContentVariableName, string titleVariableName, IStringTable stringTable) : IDataCollector
{
	private readonly string _mainContentVariableName = mainContentVariableName;
	private readonly IStringTable _stringTable = stringTable;

	public string TitleVariableName { get; } = titleVariableName;

	public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

	public string this[string key] => Items[key];

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

	public void Add(string variableName, ITemplate? template)
	{
		if (template == null)
			return;

		Add(variableName, template.Get());
	}

	public void Add(string? value) => Add(_mainContentVariableName, value);

	public void Add(ITemplate template) => Add(_mainContentVariableName, template);

	public void AddTitle(string? value) => Add(TitleVariableName, value);

	public void AddSt(string variableName, string stringTableKey) => Add(variableName, _stringTable.GetItem(stringTableKey));

	public void AddSt(string stringTableKey) => Add(_stringTable.GetItem(stringTableKey));

	public void AddTitleSt(string stringTableKey) => AddTitle(_stringTable.GetItem(stringTableKey));

	public bool IsDataExist(string variableName) => Items.ContainsKey(variableName);
}