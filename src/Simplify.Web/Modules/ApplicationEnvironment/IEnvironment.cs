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
	string DataPath { get; }

	/// <summary>
	/// Gets the data physical path.
	/// </summary>
	string DataPhysicalPath { get; }
}