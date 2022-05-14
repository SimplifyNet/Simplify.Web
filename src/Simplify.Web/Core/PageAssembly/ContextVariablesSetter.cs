using System.Threading;
using Simplify.DI;
using Simplify.Web.Diagnostics.Measurement;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Core.PageAssembly;

/// <summary>
/// Provides context variables setter
/// </summary>
public class ContextVariablesSetter : IContextVariablesSetter
{
	/// <summary>
	/// The site variable name templates dir
	/// </summary>
	public const string VariableNameTemplatesPath = "SV:TemplatesDir";

	/// <summary>
	/// The site variable name current style
	/// </summary>
	public const string VariableNameSiteStyle = "SV:Style";

	/// <summary>
	/// The site variable name current language
	/// </summary>
	public const string VariableNameCurrentLanguage = "SV:Language";

	/// <summary>
	/// The site variable name current language extension
	/// </summary>
	public const string VariableNameCurrentLanguageExtension = "SV:LanguageExt";

	/// <summary>
	///  The site variable name current language culture name
	/// </summary>
	public const string VariableNameCurrentLanguageCultureName = "SV:LanguageCultureName";

	/// <summary>
	/// The site variable name current language culture name extension
	/// </summary>
	public const string VariableNameCurrentLanguageCultureNameExtension = "SV:LanguageCultureNameExt";

	/// <summary>
	/// The site variable name site URL
	/// </summary>
	public const string VariableNameSiteUrl = "SV:SiteUrl";

	/// <summary>
	/// The site variable name site virtual path (returns '/yoursite' if your site is placed in virtual directory like http://yourdomain.com/yoursite/)
	/// </summary>
	public const string VariableNameSiteVirtualPath = "~";

	/// <summary>
	/// Site generation time templates variable name
	/// </summary>
	public const string VariableNameExecutionTime = "SV:SiteExecutionTime";

	/// <summary>
	/// The site title string table variable name
	/// </summary>
	public const string SiteTitleStringTableVariableName = "SiteTitle";

	private readonly IDataCollector _dataCollector;
	private readonly bool _disableAutomaticSiteTitleSet;

	/// <summary>
	/// Initializes a new instance of the <see cref="ContextVariablesSetter" /> class.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	/// <param name="disableAutomaticSiteTitleSet">if set to <c>true</c> then automatic site title set will be disabled.</param>
	public ContextVariablesSetter(IDataCollector dataCollector, bool disableAutomaticSiteTitleSet)
	{
		_dataCollector = dataCollector;
		_disableAutomaticSiteTitleSet = disableAutomaticSiteTitleSet;
	}

	/// <summary>
	/// Sets the context variables to data collector
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	public void SetVariables(IDIResolver resolver)
	{
		var environment = resolver.Resolve<IEnvironment>();
		var languageManager = resolver.Resolve<ILanguageManagerProvider>().Get();
		var context = resolver.Resolve<IWebContextProvider>().Get();
		var stopWatchProvider = resolver.Resolve<IStopwatchProvider>();

		_dataCollector.Add(VariableNameTemplatesPath, environment.TemplatesPath);
		_dataCollector.Add(VariableNameSiteStyle, environment.SiteStyle);

		if (!string.IsNullOrEmpty(languageManager.Language))
		{
			_dataCollector.Add(VariableNameCurrentLanguage, languageManager.Language);
			_dataCollector.Add(VariableNameCurrentLanguageExtension, "." + languageManager.Language);
			_dataCollector.Add(VariableNameCurrentLanguageCultureName, Thread.CurrentThread.CurrentCulture.TextInfo.CultureName);
			_dataCollector.Add(VariableNameCurrentLanguageCultureNameExtension, "." + Thread.CurrentThread.CurrentCulture.TextInfo.CultureName);
		}
		else
		{
			_dataCollector.Add(VariableNameCurrentLanguage, (string?)null);
			_dataCollector.Add(VariableNameCurrentLanguageExtension, (string?)null);
			_dataCollector.Add(VariableNameCurrentLanguageCultureName, (string?)null);
			_dataCollector.Add(VariableNameCurrentLanguageCultureNameExtension, (string?)null);
		}

		_dataCollector.Add(VariableNameSiteUrl, context.SiteUrl);
		_dataCollector.Add(VariableNameSiteVirtualPath, context.VirtualPath);

		if (!_disableAutomaticSiteTitleSet)
			SetSiteTitleFromStringTable(context.Request.Path.Value, resolver.Resolve<IStringTable>());

		_dataCollector.Add(VariableNameExecutionTime, stopWatchProvider.StopAndGetMeasurement().ToString("mm\\:ss\\:fff"));
	}

	private void SetSiteTitleFromStringTable(string? currentPath, IStringTable stringTable)
	{
		var siteTitle = stringTable.GetItem(SiteTitleStringTableVariableName);

		if (string.IsNullOrEmpty(siteTitle))
			return;

		if (string.IsNullOrEmpty(currentPath) || currentPath == "/" || currentPath!.StartsWith("/?") || !_dataCollector.IsDataExist(_dataCollector.TitleVariableName))
			_dataCollector.Add(_dataCollector.TitleVariableName, siteTitle);
		else
			_dataCollector.Add(_dataCollector.TitleVariableName, " - " + siteTitle);
	}
}