using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Modules;

[TestFixture]
public class EnvironmentTests
{
	private SimplifyWebSettings _settings;

	[SetUp]
	public void Initialize()
	{
		_settings = new SimplifyWebSettings();
	}

	[Test]
	public void Constructor_DefaultParameters_PropertiesSetCorrectly()
	{
		// Act
		var env = new Environment("C:/Test", _settings);

		// Assert

		Assert.AreEqual("Main", env.SiteStyle);
		Assert.AreEqual("Templates", env.TemplatesPath);
		Assert.AreEqual("Master.tpl", env.MasterTemplateFileName);
		Assert.AreEqual("Templates", env.TemplatesPath);
		Assert.AreEqual("C:/Test/Templates/", env.TemplatesPhysicalPath);
		Assert.AreEqual("App_Data", env.DataPath);
		Assert.AreEqual("C:/Test/App_Data/", env.DataPhysicalPath);
	}

	[Test]
	public void Constructor_BackslashPath_BackslashAdded()
	{
		// Act
		var env = new Environment(@"C:\Test\", _settings);

		// Assert
		Assert.AreEqual("C:/Test/Templates/", env.TemplatesPhysicalPath);
	}
}