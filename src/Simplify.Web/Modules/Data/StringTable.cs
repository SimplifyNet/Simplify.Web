using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.XPath;
using Simplify.Web.Modules.Localization;
using Simplify.Xml;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Provides the localizable text items string table.
/// </summary>
/// <param name="stringTableFiles">The string table files.</param>
/// <param name="defaultLanguage">The default language.</param>
/// <param name="languageManagerProvider">The language manager provider.</param>
/// <param name="fileReader">The file reader.</param>
/// <param name="memoryCache">Enables the memory cache.</param>
public sealed class StringTable(IReadOnlyList<string> stringTableFiles,
	string defaultLanguage,
	ILanguageManagerProvider languageManagerProvider,
	IFileReader fileReader,
	bool memoryCache = false) : IStringTable
{
	private static readonly IDictionary<string, IDictionary<string, object?>> Cache = new Dictionary<string, IDictionary<string, object?>>();
	private static readonly object Locker = new();

	private ILanguageManager _languageManager = null!;

	public IDictionary<string, object?> Items { get; private set; } = null!;

	public void Setup()
	{
		_languageManager = languageManagerProvider.Get();

		TryLoad();
	}

	public string? GetAssociatedValue<T>(T enumValue) where T : struct
	{
		var currentItems = (IDictionary<string, object>)Items;
		var enumItemName = enumValue.GetType().Name + "." + Enum.GetName(typeof(T), enumValue);

		return currentItems.ContainsKey(enumItemName)
			? currentItems[enumItemName] as string
			: null;
	}

	public string? GetItem(string itemName)
	{
		var currentItems = (IDictionary<string, object>)Items;

		if (currentItems.ContainsKey(itemName))
			return currentItems[itemName] as string;

		return null;
	}

	private static void Load(string fileName, string defaultLanguage, string currentLanguage, IFileReader fileReader, IDictionary<string, object?> currentItems)
	{
		var stringTable = fileReader.LoadXDocument(fileName);

		// Loading current culture strings
		if (stringTable?.Root != null)
			foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
			{
				var nameAttribute = (string?)item.Attribute("name");

				if (nameAttribute != null)
					currentItems.Add(nameAttribute, string.IsNullOrEmpty(item.Value) ? (string?)item.Attribute("value") : item.InnerXml().Trim());
			}

		if (currentLanguage == defaultLanguage)
			return;

		// Loading default culture strings

		stringTable = fileReader.LoadXDocument(fileName, defaultLanguage);

		if (stringTable?.Root == null)
			return;

		foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x =>
				 {
					 var key = (string?)x.Attribute("name");

					 return x.HasAttributes && key != null && !currentItems.ContainsKey(key);
				 }))
			currentItems.Add((string)item.Attribute("name")!, string.IsNullOrEmpty(item.Value) ? (string?)item.Attribute("value") : item.InnerXml().Trim());
	}

	private void TryLoad()
	{
		if (!memoryCache)
		{
			Items = Load();
			return;
		}

		if (TryGetStringTableFromCache())
			return;

		lock (Locker)
		{
			if (TryGetStringTableFromCache())
				return;

			var currentItems = Load();
			Cache.Add(_languageManager.Language, currentItems);
			Items = currentItems;
		}
	}

	private bool TryGetStringTableFromCache()
	{
		if (!Cache.ContainsKey(_languageManager.Language))
			return false;

		Items = Cache[_languageManager.Language];

		return true;
	}

	private IDictionary<string, object?> Load()
	{
		IDictionary<string, object?> currentItems = new ExpandoObject()!;

		foreach (var file in stringTableFiles)
			Load(file, defaultLanguage, _languageManager.Language, fileReader, currentItems);

		return currentItems;
	}
}