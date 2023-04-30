namespace Simplify.Web.Modules;

/// <summary>
/// Represent site environment properties
/// </summary>
public interface IEnvironment : IHideObjectMembers
{
	/// <summary>
	/// Gets the site physical path.
	/// </summary>
	/// <value>
	/// The site physical path.
	/// </value>
	string SitePhysicalPath { get; }

	/// <summary>
	/// Site current templates directory relative path
	/// </summary>
	string TemplatesPath { get; set; }

	/// <summary>
	/// Site current templates directory physical path
	/// </summary>
	string TemplatesPhysicalPath { get; }

	/// <summary>
	/// Gets the data path.
	/// </summary>
	/// <value>
	/// The data path.
	/// </value>
	string DataPath { get; }

	/// <summary>
	/// Gets the data physical path.
	/// </summary>
	/// <value>
	/// The data physical path.
	/// </value>
	string DataPhysicalPath { get; }

	/// <summary>
	/// Site current style
	/// </summary>
	string SiteStyle { get; set; }

	/// <summary>
	/// Gets or sets the current master page template file name
	/// </summary>
	/// <value>
	/// The name of the master page template file
	/// </value>
	string MasterTemplateFileName { get; set; }
}