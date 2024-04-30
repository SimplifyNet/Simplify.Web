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

		TemplatesPath = settings.DefaultTemplatesPath;
		DataPath = settings.DataPath;
		SiteStyle = settings.DefaultStyle;
		MasterTemplateFileName = settings.DefaultMasterTemplateFileName;
	}

	public string AppPhysicalPath { get; }

	public string TemplatesPath { get; set; }

	public string TemplatesPhysicalPath => AppPhysicalPath + TemplatesPath + "/";

	public string DataPath { get; }

	public string DataPhysicalPath => AppPhysicalPath + DataPath + "/";

	public string SiteStyle { get; set; }

	public string MasterTemplateFileName { get; set; }
}