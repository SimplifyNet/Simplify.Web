using Simplify.Web.Settings;

namespace Simplify.Web.Modules.ApplicationEnvironment;

/// <summary>
/// Initializes a new instance of the <see cref="Environment"/> class.
/// </summary>
/// <param name="appPhysicalPath">The application physical path.</param>
/// <param name="settings">The settings.</param>
public sealed class DynamicEnvironment(IEnvironment environment, ISimplifyWebSettings settings) : IDynamicEnvironment
{
	public string TemplatesPath { get; set; } = settings.DefaultTemplatesPath;

	public string TemplatesPhysicalPath => environment.AppPhysicalPath + TemplatesPath + "/";

	public string SiteStyle { get; set; } = settings.DefaultStyle;

	public string MasterTemplateFileName { get; set; } = settings.DefaultMasterTemplateFileName;
}