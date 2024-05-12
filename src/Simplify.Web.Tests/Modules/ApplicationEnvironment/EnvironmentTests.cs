using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Modules.ApplicationEnvironment;

[TestFixture]
public class EnvironmentTests
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
		var env = new Environment("C:/Test", _settings);

		// Assert

		Assert.That(env.DataPath, Is.EqualTo("App_Data"));
		Assert.That(env.DataPhysicalPath, Is.EqualTo("C:/Test/App_Data/"));
	}
}