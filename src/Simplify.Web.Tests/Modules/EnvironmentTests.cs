#nullable disable

using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Modules
{
	[TestFixture]
	public class EnvironmentTests
	{
		private ISimplifyWebSettings _settings;

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
}