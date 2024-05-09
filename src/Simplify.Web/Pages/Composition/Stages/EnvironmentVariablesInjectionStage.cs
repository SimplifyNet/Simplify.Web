using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Pages.Composition.Stages;

public class EnvironmentVariablesInjectionStage(IDynamicEnvironment dynamicEnvironment) : IPageCompositionStage
{
	/// <summary>
	/// The site variable name templates directory.
	/// </summary>
	public const string VariableNameTemplatesPath = "SV:TemplatesDir";

	/// <summary>
	/// The site variable name current style.
	/// </summary>
	public const string VariableNameSiteStyle = "SV:Style";

	public void Execute(IDataCollector dataCollector)
	{
		dataCollector.Add(VariableNameTemplatesPath, dynamicEnvironment.TemplatesPath);
		dataCollector.Add(VariableNameSiteStyle, dynamicEnvironment.SiteStyle);
	}
}