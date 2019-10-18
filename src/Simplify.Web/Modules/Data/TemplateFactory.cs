#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Simplify.Templates;

namespace Simplify.Web.Modules.Data
{
	/// <summary>
	/// Web-site cacheable text templates loader
	/// </summary>
	public sealed class TemplateFactory : ITemplateFactory
	{
		private static readonly IDictionary<KeyValuePair<string, string>, string> Cache = new Dictionary<KeyValuePair<string, string>, string>();
		private static readonly object Locker = new object();

		private readonly IEnvironment _environment;
		private readonly ILanguageManagerProvider _languageManagerProvider;
		private readonly string _defaultLanguage;
		private readonly bool _templatesMemoryCache;
		private readonly bool _loadTemplatesFromAssembly;
		private ILanguageManager _languageManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateFactory" /> class.
		/// </summary>
		/// <param name="environment">The environment.</param>
		/// <param name="languageManagerProvider">The language manager provider.</param>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="templatesMemoryCache">if set to <c>true</c> them loaded templates will be cached in memory.</param>
		/// <param name="loadTemplatesFromAssembly">if set to <c>true</c> then all templates will be loaded from assembly.</param>
		public TemplateFactory(IEnvironment environment, ILanguageManagerProvider languageManagerProvider, string defaultLanguage, bool templatesMemoryCache = false, bool loadTemplatesFromAssembly = false)
		{
			_environment = environment;
			_languageManagerProvider = languageManagerProvider;
			_defaultLanguage = defaultLanguage;
			_templatesMemoryCache = templatesMemoryCache;
			_loadTemplatesFromAssembly = loadTemplatesFromAssembly;
		}

		/// <summary>
		/// Setups the template factory.
		/// </summary>
		public void Setup()
		{
			_languageManager = _languageManagerProvider.Get();
		}

		/// <summary>
		/// Load web-site template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		public ITemplate Load(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException(nameof(fileName));

			if (!fileName.EndsWith(".tpl"))
				fileName = fileName + ".tpl";

			var filePath = !_loadTemplatesFromAssembly ? Path.Combine(_environment.TemplatesPhysicalPath, fileName) : fileName;

			if (!_templatesMemoryCache)
				return new Template(filePath, _languageManager.Language, _defaultLanguage);

			var tpl = TryLoadExistingTemplate(filePath);

			if (tpl != null)
				return tpl;

			lock (Locker)
			{
				tpl = TryLoadExistingTemplate(filePath);

				if (tpl != null)
					return tpl;

				tpl = !_loadTemplatesFromAssembly
					? new Template(filePath, _languageManager.Language, _defaultLanguage)
					: new Template(Assembly.GetCallingAssembly(), filePath.Replace("/", "."), _languageManager.Language, _defaultLanguage);

				Cache.Add(new KeyValuePair<string, string>(filePath, _languageManager.Language), tpl.Get());

				return tpl;
			}
		}

		/// <summary>
		/// Load web-site template from a file asynchronously.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		public Task<ITemplate> LoadAsync(string filename)
		{
			return Task.Run(() => Load(filename));
		}

		private ITemplate TryLoadExistingTemplate(string filePath)
		{
			// ReSharper disable once InconsistentlySynchronizedField
			var existingItem = Cache.FirstOrDefault(x => x.Key.Key == filePath && x.Key.Value == _languageManager.Language);

			if (!existingItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>)))
				return new Template(existingItem.Value, _languageManager.Language, false);

			return null;
		}
	}
}