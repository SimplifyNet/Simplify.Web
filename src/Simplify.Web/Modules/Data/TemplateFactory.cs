﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Simplify.Templates;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Localization;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Provides the Web-site cacheable text templates loader.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TemplateFactory" /> class.
/// </remarks>
/// <param name="environment">The environment.</param>
/// <param name="languageManagerProvider">The language manager provider.</param>
/// <param name="defaultLanguage">The default language.</param>
/// <param name="templatesMemoryCache">if set to <c>true</c> them loaded templates will be cached in memory.</param>
/// <param name="loadTemplatesFromAssembly">if set to <c>true</c> then all templates will be loaded from assembly.</param>
public sealed class TemplateFactory(IDynamicEnvironment environment,
	ILanguageManagerProvider languageManagerProvider,
	string defaultLanguage,
	bool templatesMemoryCache = false,
	bool loadTemplatesFromAssembly = false) : ITemplateFactory
{
	private static readonly IDictionary<KeyValuePair<string, string>, string> Cache = new Dictionary<KeyValuePair<string, string>, string>();
	private static readonly object Locker = new();
	private readonly SemaphoreSlim _cacheSemaphore = new(1, 1);

	private ILanguageManager _languageManager = null!;

	/// <summary>
	/// Setups the template factory.
	/// </summary>
	public void Setup() => _languageManager = languageManagerProvider.Get();

	/// <summary>
	/// Loads s template from a file.
	/// </summary>
	/// <param name="fileName">Template file name.</param>
	/// <returns>
	/// Template class with loaded template.
	/// </returns>
	public ITemplate Load(string fileName)
	{
		var filePath = BuildFilePath(fileName);

		if (!templatesMemoryCache)
			return LoadFromFile(filePath);

		var tpl = TryLoadFromCache(filePath);

		if (tpl != null)
			return tpl;

		lock (Locker)
		{
			tpl = TryLoadFromCache(filePath);

			if (tpl != null)
				return tpl;

			tpl = !loadTemplatesFromAssembly
				? LoadFromFile(filePath)
				: LoadFromAssembly(filePath, Assembly.GetCallingAssembly());

			Cache.Add(new KeyValuePair<string, string>(filePath, _languageManager.Language), tpl.Get());

			return tpl;
		}
	}

	/// <summary>
	/// Loads a web-site template from a file asynchronously.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	public Task<ITemplate> LoadAsync(string fileName) => LoadAsyncInternal(fileName, Assembly.GetCallingAssembly());

	private async Task<ITemplate> LoadAsyncInternal(string fileName, Assembly assembly)
	{
		var filePath = BuildFilePath(fileName);

		if (!templatesMemoryCache)
			return await LoadFromFileAsync(filePath);

		var tpl = TryLoadFromCache(filePath);

		if (tpl != null)
			return tpl;

		await _cacheSemaphore.WaitAsync();

		try
		{
			tpl = TryLoadFromCache(filePath);

			if (tpl != null)
				return tpl;

			tpl = !loadTemplatesFromAssembly
				? await LoadFromFileAsync(filePath)
				: await LoadFromAssemblyAsync(filePath, assembly);

			Cache.Add(new KeyValuePair<string, string>(filePath, _languageManager.Language), tpl.Get());

			return tpl;
		}
		finally
		{
			_cacheSemaphore.Release();
		}
	}

	private string BuildFilePath(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
			throw new ArgumentNullException(nameof(fileName));

		if (!fileName.EndsWith(".tpl"))
			fileName += ".tpl";

		return !loadTemplatesFromAssembly ? Path.Combine(environment.TemplatesPhysicalPath, fileName) : fileName;
	}

	private ITemplate LoadFromFile(string filePath) =>
		TemplateBuilder.FromFile(filePath)
			.Localizable(_languageManager.Language, defaultLanguage)
			.Build();

	private Task<ITemplate> LoadFromFileAsync(string filePath) =>
		TemplateBuilder.FromFile(filePath)
			.Localizable(_languageManager.Language, defaultLanguage)
			.BuildAsync();

	private ITemplate LoadFromAssembly(string filePath, Assembly assembly) =>
		TemplateBuilder.FromAssembly(filePath, assembly)
			.Localizable(_languageManager.Language, defaultLanguage)
			.Build();

	private Task<ITemplate> LoadFromAssemblyAsync(string filePath, Assembly assembly) =>
		TemplateBuilder.FromAssembly(filePath, assembly)
			.Localizable(_languageManager.Language, defaultLanguage)
			.BuildAsync();

	private ITemplate? TryLoadFromCache(string filePath)
	{
		var existingItem = Cache.FirstOrDefault(x => x.Key.Key == filePath && x.Key.Value == _languageManager.Language);

		return !existingItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>))
			? TemplateBuilder.FromString(existingItem.Value).Build()
			: null;
	}
}