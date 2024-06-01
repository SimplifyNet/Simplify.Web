namespace Simplify.Web.Modules.ApplicationEnvironment;

/// <summary>
/// Represents an application environment dynamic properties.
/// </summary>
public interface IDynamicEnvironment
{
	/// <summary>
	/// Gets the site current templates directory relative path.
	/// </summary>
	/// <value>
	/// The templates path.
	/// </value>
	string TemplatesPath { get; set; }

	/// <summary>
	/// Gets the site current templates directory physical path.
	/// </summary>
	/// <value>
	/// The templates physical path.
	/// </value>
	string TemplatesPhysicalPath { get; }

	/// <summary>
	/// Gets the site current style.
	/// </summary>
	/// <value>
	/// The site style.
	/// </value>
	string SiteStyle { get; set; }

	/// <summary>
	/// Gets or sets the current master page template file name.
	/// </summary>
	/// <value>
	/// The name of the master template file.
	/// </value>
	string MasterTemplateFileName { get; set; }
}