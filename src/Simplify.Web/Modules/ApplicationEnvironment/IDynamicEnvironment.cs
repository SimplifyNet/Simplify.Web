namespace Simplify.Web.Modules.ApplicationEnvironment;

/// <summary>
/// Represents an application environment dynamic properties.
/// </summary>
public interface IDynamicEnvironment
{
	/// <summary>
	/// Gets the site current templates directory relative path.
	/// </summary>
	string TemplatesPath { get; set; }

	/// <summary>
	/// Gets the site current templates directory physical path.
	/// </summary>
	string TemplatesPhysicalPath { get; }

	/// <summary>
	/// Gets the site current style.
	/// </summary>
	string SiteStyle { get; set; }

	/// <summary>
	/// Gets or sets the current master page template file name.
	/// </summary>
	string MasterTemplateFileName { get; set; }
}