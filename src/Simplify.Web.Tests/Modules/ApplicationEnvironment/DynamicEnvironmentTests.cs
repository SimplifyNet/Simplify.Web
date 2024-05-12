using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Modules.ApplicationEnvironment;

[TestFixture]
public class DynamicEnvironmentTests
{
	private ISimplifyWebSettings _settings = null!;

	[SetUp]
	public void Initialize()
	{
		var settings = new Mock<ISimplifyWebSettings>();

		settings.SetupGet(x => x.DefaultTemplatesPath).Returns("Templates");
		settings.SetupGet(x => x.DefaultStyle).Returns("Main");
		settings.SetupGet(x => x.DefaultMasterTemplateFileName).Returns("Master.tpl");
		settings.SetupGet(x => x.DataPath).Returns("App_Data");

		_settings = settings.Object;
	}

	[Test]
	public void Constructor_DefaultParameters_PropertiesSetCorrectly()
	{
		// Act
		var env = new DynamicEnvironment(new Environment("C:/Test", _settings), _settings);

		// Assert

		Assert.That(env.SiteStyle, Is.EqualTo("Main"));
		Assert.That(env.TemplatesPath, Is.EqualTo("Templates"));
		Assert.That(env.MasterTemplateFileName, Is.EqualTo("Master.tpl"));
		Assert.That(env.TemplatesPath, Is.EqualTo("Templates"));
		Assert.That(env.TemplatesPhysicalPath, Is.EqualTo("C:/Test/Templates/"));
	}

	[Test]
	public void Constructor_BackslashPath_BackslashAdded()
	{
		// Act
		var env = new DynamicEnvironment(new Environment(@"C:\Test\", _settings), _settings);

		// Assert
		Assert.That(env.TemplatesPhysicalPath, Is.EqualTo("C:/Test/Templates/"));
	}
}