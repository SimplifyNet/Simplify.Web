namespace Simplify.Web.Modules.Environment;

/// <summary>
/// Represents a site environment properties.
/// </summary>
public interface IEnvironment
{
	/// <summary>
	/// Gets the site physical path.
	/// </summary>
	/// <value>
	/// The site physical path.
	/// </value>
	string SitePhysicalPath { get; }

	/// <summary>
	/// Gets the site current templates directory relative path.
	/// </summary>
	string TemplatesPath { get; set; }

	/// <summary>
	/// Gets the site current templates directory physical path.
	/// </summary>
	string TemplatesPhysicalPath { get; }

	/// <summary>
	/// Gets the data path.
	/// </summary>
	string DataPath { get; }

	/// <summary>
	/// Gets the data physical path.
	/// </summary>
	string DataPhysicalPath { get; }

	/// <summary>
	/// Gets the site current style.
	/// </summary>
	string SiteStyle { get; set; }

	/// <summary>
	/// Gets or sets the current master page template file name.
	/// </summary>
	string MasterTemplateFileName { get; set; }
}