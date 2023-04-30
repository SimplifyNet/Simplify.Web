using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.XPath;
using Simplify.Xml;

#nullable disable

namespace Simplify.Web.Modules.Data
{
	/// <summary>
	/// Localizable text items string table.
	/// </summary>
	public sealed class StringTable : IStringTable
	{
		private static readonly IDictionary<string, IDictionary<string, object>> Cache = new Dictionary<string, IDictionary<string, object>>();
		private static readonly object Locker = new object();

		private readonly IList<string> _stringTableFiles;
		private readonly string _defaultLanguage;
		private readonly ILanguageManagerProvider _languageManagerProvider;
		private readonly IFileReader _fileReader;
		private readonly bool _memoryCache;
		private ILanguageManager _languageManager;

		/// <summary>
		/// Load string table with current language
		/// </summary>
		/// <param name="stringTableFiles">The string table files.</param>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="languageManagerProvider">The language manager provider.</param>
		/// <param name="fileReader">The file reader.</param>
		/// <param name="memoryCache">Enable memory cache.</param>
		public StringTable(IList<string> stringTableFiles, string defaultLanguage, ILanguageManagerProvider languageManagerProvider, IFileReader fileReader, bool memoryCache = false)
		{
			_stringTableFiles = stringTableFiles;
			_defaultLanguage = defaultLanguage;
			_languageManagerProvider = languageManagerProvider;
			_fileReader = fileReader;
			_memoryCache = memoryCache;
		}

		/// <summary>
		/// String table items
		/// </summary>
		public dynamic Items { get; private set; }

		/// <summary>
		/// Setups this string table.
		/// </summary>
		public void Setup()
		{
			_languageManager = _languageManagerProvider.Get();

			TryLoad();
		}

		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="enumValue">Enum value</param>
		/// <returns>associated value</returns>
		public string GetAssociatedValue<T>(T enumValue) where T : struct
		{
			var currentItems = (IDictionary<string, object>)Items;
			var enumItemName = enumValue.GetType().Name + "." + Enum.GetName(typeof(T), enumValue);

			return currentItems.ContainsKey(enumItemName) ? currentItems[enumItemName] as string : null;
		}

		/// <summary>
		/// Gets the item from string table.
		/// </summary>
		/// <param name="itemName">Name of the item.</param>
		/// <returns></returns>
		public string GetItem(string itemName)
		{
			var currentItems = (IDictionary<string, object>)Items;

			if (currentItems.ContainsKey(itemName))
				return currentItems[itemName] as string;

			return null;
		}

		/// <summary>
		/// Loads string table.
		/// </summary>
		private static void Load(string fileName, string defaultLanguage, string currentLanguage, IFileReader fileReader, IDictionary<string, object> currentItems)
		{
			var stringTable = fileReader.LoadXDocument(fileName);

			// Loading current culture strings
			if (stringTable?.Root != null)
				foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
					currentItems.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());

			if (currentLanguage == defaultLanguage)
				return;

			// Loading default culture strings

			stringTable = fileReader.LoadXDocument(fileName, defaultLanguage);

			if (stringTable?.Root == null)
				return;

			foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes && !currentItems.ContainsKey((string)x.Attribute("name"))))
				currentItems.Add((string)item.Attribute("name"), string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());
		}

		private void TryLoad()
		{
			if (!_memoryCache)
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
			if (!Cache.ContainsKey(_languageManager.Language)) return false;

			Items = Cache[_languageManager.Language];

			return true;
		}

		private IDictionary<string, object> Load()
		{
			IDictionary<string, object> currentItems = new ExpandoObject();

			foreach (var file in _stringTableFiles)
				Load(file, _defaultLanguage, _languageManager.Language, _fileReader, currentItems);

			return currentItems;
		}
	}
}