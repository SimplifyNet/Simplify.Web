namespace Simplify.Web.Modules.ApplicationEnvironment;

/// <summary>
/// Represents an application environment properties.
/// </summary>
public interface IEnvironment
{
	/// <summary>
	/// Gets the application physical path.
	/// </summary>
	/// <value>
	/// The application physical path.
	/// </value>
	string AppPhysicalPath { get; }

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
}