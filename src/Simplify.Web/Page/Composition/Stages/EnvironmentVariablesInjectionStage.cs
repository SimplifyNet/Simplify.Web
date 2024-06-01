using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

/// <summary>
/// Provides the environment variables injection stage.
/// </summary>
/// <seealso cref="Simplify.Web.Page.Composition.IPageCompositionStage" />
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

	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	public void Execute(IDataCollector dataCollector)
	{
		dataCollector.Add(VariableNameTemplatesPath, dynamicEnvironment.TemplatesPath);
		dataCollector.Add(VariableNameSiteStyle, dynamicEnvironment.SiteStyle);
	}
}