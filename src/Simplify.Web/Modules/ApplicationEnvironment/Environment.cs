using Simplify.Web.Settings;

namespace Simplify.Web.Modules.ApplicationEnvironment;

/// <summary>
/// Provides the environment.
/// </summary>
/// <seealso cref="IEnvironment" />
public sealed class Environment : IEnvironment
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Environment" /> class.
	/// </summary>
	/// <param name="appPhysicalPath">The application physical path.</param>
	/// <param name="settings">The settings.</param>
	public Environment(string appPhysicalPath, ISimplifyWebSettings settings)
	{
		appPhysicalPath = appPhysicalPath.Replace("\\", "/");

		if (!appPhysicalPath.EndsWith("/"))
			appPhysicalPath += "/";

		AppPhysicalPath = appPhysicalPath;
		DataPath = settings.DataPath;
	}

	/// <summary>
	/// Gets the application physical path.
	/// </summary>
	/// <value>
	/// The application physical path.
	/// </value>
	public string AppPhysicalPath { get; }

	/// <summary>
	/// Gets the data path.
	/// </summary>
	/// <value>
	/// The data path.
	/// </value>
	public string DataPath { get; }

	/// <summary>
	/// Gets the data physical path.
	/// </summary>
	/// <value>
	/// The data physical path.
	/// </value>
	public string DataPhysicalPath => AppPhysicalPath + DataPath + "/";
}