using Simplify.Web.Settings;

namespace Simplify.Web.Modules.ApplicationEnvironment;

public sealed class Environment : IEnvironment
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Environment"/> class.
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

	public string AppPhysicalPath { get; }


	public string DataPath { get; }

	public string DataPhysicalPath => AppPhysicalPath + DataPath + "/";
}