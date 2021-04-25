using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Simplify.Web.Modules.Data
{
	/// <summary>
	/// Provides localizable files reader, reads the files from data folder
	/// </summary>
	public class FileReader : IFileReader
	{
		private static readonly IDictionary<KeyValuePair<string, string>, XDocument?> XmlCache =
			new Dictionary<KeyValuePair<string, string>, XDocument?>();

		private static readonly IDictionary<KeyValuePair<string, string>, string?> TextCache =
			new Dictionary<KeyValuePair<string, string>, string?>();

		private static readonly object Locker = new();

		private readonly string _dataPhysicalPath;
		private readonly string _defaultLanguage;
		private readonly ILanguageManagerProvider _languageManagerProvider;
		private readonly bool _disableCache;

		private ILanguageManager _languageManager = null!;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileReader" /> class.
		/// </summary>
		/// <param name="dataPhysicalPath">The data physical path.</param>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="languageManagerProvider">The language manager provider.</param>
		/// <param name="disableCache">Disable FileReader cache.</param>
		public FileReader(string dataPhysicalPath, string defaultLanguage, ILanguageManagerProvider languageManagerProvider,
			bool disableCache = false)
		{
			_dataPhysicalPath = dataPhysicalPath;
			_defaultLanguage = defaultLanguage;
			_languageManagerProvider = languageManagerProvider;
			_disableCache = disableCache;
		}

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
		public void Setup() => _languageManager = _languageManagerProvider.Get();

		#region Paths

		/// <summary>
		/// Get file path
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>
		/// File path
		/// </returns>
		public string GetFilePath(string fileName) => GetFilePath(fileName, _languageManager.Language);

		/// <summary>
		/// Get extension data file path for specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <returns>File path</returns>
		public string GetFilePath(string? fileName, string? language)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
			if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

			var indexOfPoint = fileName!.LastIndexOf(".", StringComparison.Ordinal);

			if (indexOfPoint == -1)
				return $"{_dataPhysicalPath}{fileName}.{language}";

			var fileNameFirstPart = fileName.Substring(0, indexOfPoint);
			var fileNameLastPart = fileName.Substring(indexOfPoint, fileName.Length - indexOfPoint);

			return $"{_dataPhysicalPath}{fileNameFirstPart}.{language}{fileNameLastPart}";
		}

		#endregion Paths

		#region Text

		/// <summary>
		/// Load text from a file located in data folder
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Text from a file
		/// </returns>
		public string? LoadTextDocument(string fileName, bool memoryCache = false) => LoadTextDocument(fileName, _languageManager.Language, memoryCache);

		/// <summary>
		/// Load text from a file with specific language located in data folder
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Text from a file
		/// </returns>
		/// <exception cref="ArgumentNullException">fileName</exception>
		public string? LoadTextDocument(string fileName, string language, bool memoryCache = false)
		{
			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException(nameof(fileName));

			string? data;

			if (!memoryCache || _disableCache)
			{
				if (LoadTextFileFromFileSystem(fileName, language, out data))
					return data;

				return LoadTextFileFromFileSystem(fileName, _defaultLanguage, out data) ? data : null;
			}

			if (LoadTextFileCached(fileName, language, out data))
				return data;

			return LoadTextFileCached(fileName, _defaultLanguage, out data) ? data : null;
		}

		#endregion Text

		#region XML

		/// <summary>
		/// Load xml document from a file located in data folder
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Xml document
		/// </returns>
		public XDocument? LoadXDocument(string fileName, bool memoryCache = false) => LoadXDocument(fileName, _languageManager.Language, memoryCache);

		/// <summary>
		/// Load xml document from a file with specific language located in data folder
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Xml document
		/// </returns>
		/// <exception cref="ArgumentNullException">fileName</exception>
		public XDocument? LoadXDocument(string fileName, string language, bool memoryCache = false)
		{
			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException(nameof(fileName));

			if (!fileName.EndsWith(".xml"))
				fileName = fileName + ".xml";

			XDocument? data;

			if (!memoryCache || _disableCache)
			{
				if (LoadXDocumentFromFileSystem(fileName, language, out data))
					return data;

				return LoadXDocumentFromFileSystem(fileName, _defaultLanguage, out data) ? data : null;
			}

			if (LoadXDocumentCached(fileName, language, out data))
				return data;

			return LoadXDocumentCached(fileName, _defaultLanguage, out data) ? data : null;
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
}