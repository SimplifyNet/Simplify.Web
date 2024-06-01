using Simplify.Web.Settings;

namespace Simplify.Web.Modules.ApplicationEnvironment;

/// <summary>
/// Provides the dynamic environment.
/// </summary>
/// <seealso cref="IDynamicEnvironment" />
public sealed class DynamicEnvironment(IEnvironment environment, ISimplifyWebSettings settings) : IDynamicEnvironment
{
	/// <summary>
	/// Gets the site current templates directory relative path.
	/// </summary>
	/// <value>
	/// The templates path.
	/// </value>
	public string TemplatesPath { get; set; } = settings.DefaultTemplatesPath;

	/// <summary>
	/// Gets the site current templates directory physical path.
	/// </summary>
	/// <value>
	/// The templates physical path.
	/// </value>
	public string TemplatesPhysicalPath => environment.AppPhysicalPath + TemplatesPath + "/";

	/// <summary>
	/// Gets the site current style.
	/// </summary>
	/// <value>
	/// The site style.
	/// </value>
	public string SiteStyle { get; set; } = settings.DefaultStyle;

	/// <summary>
	/// Gets or sets the current master page template file name.
	/// </summary>
	/// <value>
	/// The name of the master template file.
	/// </value>
	public string MasterTemplateFileName { get; set; } = settings.DefaultMasterTemplateFileName;
}