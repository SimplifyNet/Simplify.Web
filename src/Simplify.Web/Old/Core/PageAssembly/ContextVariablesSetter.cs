using System.Threading;
using Simplify.DI;
using Simplify.Web.Old.Diagnostics.Measurement;
using Simplify.Web.Old.Modules;
using Simplify.Web.Old.Modules.Data;

namespace Simplify.Web.Old.Core.PageAssembly;

/// <summary>
/// Provides context variables setter.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ContextVariablesSetter" /> class.
/// </remarks>
/// <param name="dataCollector">The data collector.</param>
/// <param name="disableAutomaticSiteTitleSet">if set to <c>true</c> then automatic site title set will be disabled.</param>
public class ContextVariablesSetter(IDataCollector dataCollector, bool disableAutomaticSiteTitleSet) : IContextVariablesSetter
{
	/// <summary>
	/// The site variable name templates directory.
	/// </summary>
	public const string VariableNameTemplatesPath = "SV:TemplatesDir";

	/// <summary>
	/// The site variable name current style.
	/// </summary>
	public const string VariableNameSiteStyle = "SV:Style";

	/// <summary>
	/// The site variable name current language.
	/// </summary>
	public const string VariableNameCurrentLanguage = "SV:Language";

	/// <summary>
	/// The site variable name current language extension.
	/// </summary>
	public const string VariableNameCurrentLanguageExtension = "SV:LanguageExt";

	/// <summary>
	///  The site variable name current language culture name.
	/// </summary>
	public const string VariableNameCurrentLanguageCultureName = "SV:LanguageCultureName";

	/// <summary>
	/// The site variable name current language culture name extension.
	/// </summary>
	public const string VariableNameCurrentLanguageCultureNameExtension = "SV:LanguageCultureNameExt";

	/// <summary>
	/// The site variable name site URL.
	/// </summary>
	public const string VariableNameSiteUrl = "SV:SiteUrl";

	/// <summary>
	/// The site variable name site virtual path (returns '/yoursite' if your site is placed in virtual directory like http://yourdomain.com/yoursite/).
	/// </summary>
	public const string VariableNameSiteVirtualPath = "~";

	/// <summary>
	/// Site generation time templates variable name.
	/// </summary>
	public const string VariableNameExecutionTime = "SV:SiteExecutionTime";

	/// <summary>
	/// The site title string table variable name.
	/// </summary>
	public const string SiteTitleStringTableVariableName = "SiteTitle";

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

		dataCollector.Add(VariableNameTemplatesPath, environment.TemplatesPath);
		dataCollector.Add(VariableNameSiteStyle, environment.SiteStyle);

		if (!string.IsNullOrEmpty(languageManager.Language))
		{
			dataCollector.Add(VariableNameCurrentLanguage, languageManager.Language);
			dataCollector.Add(VariableNameCurrentLanguageExtension, "." + languageManager.Language);
			dataCollector.Add(VariableNameCurrentLanguageCultureName, Thread.CurrentThread.CurrentCulture.TextInfo.CultureName);
			dataCollector.Add(VariableNameCurrentLanguageCultureNameExtension, "." + Thread.CurrentThread.CurrentCulture.TextInfo.CultureName);
		}
		else
		{
			dataCollector.Add(VariableNameCurrentLanguage, (string?)null);
			dataCollector.Add(VariableNameCurrentLanguageExtension, (string?)null);
			dataCollector.Add(VariableNameCurrentLanguageCultureName, (string?)null);
			dataCollector.Add(VariableNameCurrentLanguageCultureNameExtension, (string?)null);
		}

		dataCollector.Add(VariableNameSiteUrl, context.SiteUrl);
		dataCollector.Add(VariableNameSiteVirtualPath, context.VirtualPath);

		if (!disableAutomaticSiteTitleSet)
			SetSiteTitleFromStringTable(context.Request.Path.Value, resolver.Resolve<IStringTable>());

		dataCollector.Add(VariableNameExecutionTime, stopWatchProvider.StopAndGetMeasurement().ToString("mm\\:ss\\:fff"));
	}

	private void SetSiteTitleFromStringTable(string? currentPath, IStringTable stringTable)
	{
		var siteTitle = stringTable.GetItem(SiteTitleStringTableVariableName);

		if (string.IsNullOrEmpty(siteTitle))
			return;

		if (string.IsNullOrEmpty(currentPath) || currentPath == "/" || currentPath!.StartsWith("/?") || !dataCollector.IsDataExist(dataCollector.TitleVariableName))
			dataCollector.Add(dataCollector.TitleVariableName, siteTitle);
		else
			dataCollector.Add(dataCollector.TitleVariableName, " - " + siteTitle);
	}
}