﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Simplify.Web.Modules.Localization;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Provides the localizable files reader, reads the files from data folder.
/// </summary>
/// <seealso cref="IFileReader" />
/// <remarks>
/// Initializes a new instance of the <see cref="FileReader" /> class.
/// </remarks>
/// <param name="dataPhysicalPath">The data physical path.</param>
/// <param name="defaultLanguage">The default language.</param>
/// <param name="languageManagerProvider">The language manager provider.</param>
/// <param name="disableCache">Disables the FileReader cache.</param>
public sealed class FileReader(string dataPhysicalPath, string defaultLanguage, ILanguageManagerProvider languageManagerProvider,
	bool disableCache = false) : IFileReader
{
	private static readonly IDictionary<KeyValuePair<string, string>, XDocument?> XmlCache =
		new Dictionary<KeyValuePair<string, string>, XDocument?>();

	private static readonly IDictionary<KeyValuePair<string, string>, string?> TextCache =
		new Dictionary<KeyValuePair<string, string>, string?>();

	private static readonly object Locker = new();

	private ILanguageManager _languageManager = null!;

	/// <summary>
	/// Clears the cache.
	/// </summary>
	public static void ClearCache()
	{
		lock (Locker)
		{
			XmlCache.Clear();
			TextCache.Clear();
		}
	}

	/// <summary>
	/// Setups the file reader.
	/// </summary>
	public void Setup() => _languageManager = languageManagerProvider.Get();

	#region Paths

	/// <summary>
	/// Gets the file path.
	/// </summary>
	/// <param name="fileName">Name of the file.</param>
	public string GetFilePath(string fileName) => GetFilePath(fileName, _languageManager.Language);

	/// <summary>
	/// Gets the file path.
	/// </summary>
	/// <param name="fileName">Name of the file.</param>
	/// <param name="language">The language.</param>
	/// <exception cref="ArgumentNullException">
	/// fileName
	/// or
	/// language
	/// </exception>
	public string GetFilePath(string? fileName, string? language)
	{
		if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
		if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

		var indexOfPoint = fileName!.LastIndexOf(".", StringComparison.Ordinal);

		if (indexOfPoint == -1)
			return $"{dataPhysicalPath}{fileName}.{language}";

		var fileNameFirstPart = fileName.Substring(0, indexOfPoint);
		var fileNameLastPart = fileName.Substring(indexOfPoint, fileName.Length - indexOfPoint);

		return $"{dataPhysicalPath}{fileNameFirstPart}.{language}{fileNameLastPart}";
	}

	#endregion Paths

	#region Text

	/// <summary>
	/// Loads a text from a file located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	public string? LoadTextDocument(string fileName, bool memoryCache = false) => LoadTextDocument(fileName, _languageManager.Language, memoryCache);

	/// <summary>
	/// Load a text from a file with specific language located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="language">File language.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	/// <exception cref="ArgumentNullException">fileName</exception>
	public string? LoadTextDocument(string fileName, string language, bool memoryCache = false)
	{
		if (string.IsNullOrEmpty(fileName))
			throw new ArgumentNullException(nameof(fileName));

		string? data;

		if (!memoryCache || disableCache)
		{
			if (LoadTextFileFromFileSystem(fileName, language, out data))
				return data;

			return LoadTextFileFromFileSystem(fileName, defaultLanguage, out data)
				? data
				: null;
		}

		if (LoadTextFileCached(fileName, language, out data))
			return data;

		return LoadTextFileCached(fileName, defaultLanguage, out data)
			? data
			: null;
	}

	#endregion Text

	#region XML

	/// <summary>
	/// Loads a xml document from a file located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	public XDocument? LoadXDocument(string fileName, bool memoryCache = false) => LoadXDocument(fileName, _languageManager.Language, memoryCache);

	/// <summary>
	/// Loads a xml document from a file with specific language located in data folder.
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="language">File language.</param>
	/// <param name="memoryCache">Load file from memory cache if possible.</param>
	/// <exception cref="ArgumentNullException">fileName</exception>
	public XDocument? LoadXDocument(string fileName, string language, bool memoryCache = false)
	{
		if (string.IsNullOrEmpty(fileName))
			throw new ArgumentNullException(nameof(fileName));

		if (!fileName.EndsWith(".xml"))
			fileName += ".xml";

		XDocument? data;

		if (!memoryCache || disableCache)
		{
			if (LoadXDocumentFromFileSystem(fileName, language, out data))
				return data;

			return LoadXDocumentFromFileSystem(fileName, defaultLanguage, out data)
				? data
				: null;
		}

		if (LoadXDocumentCached(fileName, language, out data))
			return data;

		return LoadXDocumentCached(fileName, defaultLanguage, out data)
			? data
			: null;
	}

	#endregion XML

	private static bool TryToLoadTextFileFromCache(string fileName, string language, out string? data)
	{
		data = null;

		var cacheItem = TextCache.FirstOrDefault(x => x.Key.Key == fileName && x.Key.Value == language);

		if (cacheItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>)))
			return false;

		data = cacheItem.Value;

		return true;
	}

	private static bool TryToLoadXDocumentFromCache(string fileName, string language, out XDocument? data)
	{
		data = null;

		var cacheItem = XmlCache.FirstOrDefault(x => x.Key.Key == fileName && x.Key.Value == language);

		if (cacheItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, XDocument>)))
			return false;

		data = cacheItem.Value;

		return true;
	}

	private bool LoadTextFileFromFileSystem(string fileName, string language, out string? data)
	{
		data = null;

		var filePath = GetFilePath(fileName, language);

		if (!File.Exists(filePath))
			return false;

		data = File.ReadAllText(filePath);

		return true;
	}

	private bool LoadTextFileCached(string fileName, string language, out string? data)
	{
		if (TryToLoadTextFileFromCache(fileName, language, out data))
			return true;

		lock (Locker)
		{
			if (TryToLoadTextFileFromCache(fileName, language, out data))
				return true;

			if (!LoadTextFileFromFileSystem(fileName, language, out data))
				return false;

			TextCache.Add(new KeyValuePair<string, string>(fileName, language), data);

			return true;
		}
	}

	private bool LoadXDocumentFromFileSystem(string fileName, string language, out XDocument? data)
	{
		data = null;

		if (!LoadTextFileFromFileSystem(fileName, language, out var internalData))
			return false;

		data = XDocument.Parse(internalData!);

		return true;
	}

	private bool LoadXDocumentCached(string fileName, string language, out XDocument? data)
	{
		if (TryToLoadXDocumentFromCache(fileName, language, out data))
			return true;

		lock (Locker)
		{
			if (TryToLoadXDocumentFromCache(fileName, language, out data))
				return true;

			if (!LoadXDocumentFromFileSystem(fileName, language, out data))
				return false;

			XmlCache.Add(new KeyValuePair<string, string>(fileName, language), data);

			return true;
		}
	}
}