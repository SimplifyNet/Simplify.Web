using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Settings;

[TestFixture]
public class SimplifyWebSettingsTests
{
	[Test]
	public void Ctor_AllSettingsOverride_Loaded()
	{
		// Arrange

		var items = new Dictionary<string, string?>
		{
			{ "SimplifyWebSettings:DefaultLanguage", "th" },
			{ "SimplifyWebSettings:AcceptCookieLanguage", "true" },
			{ "SimplifyWebSettings:AcceptHeaderLanguage", "true" },
			{ "SimplifyWebSettings:DefaultTemplatesPath", "Foo" },
			{ "SimplifyWebSettings:LoadTemplatesFromAssembly", "true" },
			{ "SimplifyWebSettings:DefaultMasterTemplateFileName", "Bar" },
			{ "SimplifyWebSettings:DefaultMainContentVariableName", "Cont" },
			{ "SimplifyWebSettings:DefaultTitleVariableName", "HelloSite" },
			{ "SimplifyWebSettings:DefaultStyle", "Stl" },
			{ "SimplifyWebSettings:DataPath", "SiteDt" },
			{ "SimplifyWebSettings:StaticFilesEnabled", "true" },
			{ "SimplifyWebSettings:StaticFilesPaths", "stl,img" },
			{ "SimplifyWebSettings:StringTableFiles", "MyStr.xml" },
			{ "SimplifyWebSettings:DisableAutomaticSiteTitleSet", "true" },
			{ "SimplifyWebSettings:HideExceptionDetails", "true" },
			{ "SimplifyWebSettings:ErrorPageDarkStyle", "true" },
			{ "SimplifyWebSettings:TemplatesMemoryCache", "true" },
			{ "SimplifyWebSettings:StringTableMemoryCache", "true" },
			{ "SimplifyWebSettings:DisableFileReaderCache", "true" },
			{ "SimplifyWebSettings:MeasurementsEnabled", "true" },
			{ "SimplifyWebSettings:ConsoleTracing", "true" }
		};

		var configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(items)
			.Build();

		// Act
		var settings = new SimplifyWebSettings(configuration);

		// Assert

		Assert.That(settings.DefaultLanguage, Is.EqualTo("th"));
		Assert.That(settings.AcceptCookieLanguage, Is.True);
		Assert.That(settings.AcceptHeaderLanguage, Is.True);
		Assert.That(settings.DefaultTemplatesPath, Is.EqualTo("Foo"));
		Assert.That(settings.LoadTemplatesFromAssembly, Is.True);
		Assert.That(settings.DefaultMasterTemplateFileName, Is.EqualTo("Bar"));
		Assert.That(settings.DefaultMainContentVariableName, Is.EqualTo("Cont"));
		Assert.That(settings.DefaultTitleVariableName, Is.EqualTo("HelloSite"));
		Assert.That(settings.DefaultStyle, Is.EqualTo("Stl"));
		Assert.That(settings.DataPath, Is.EqualTo("SiteDt"));
		Assert.That(settings.StaticFilesEnabled, Is.True);
		Assert.That(settings.StaticFilesPaths.Count, Is.EqualTo(2));
		Assert.That(settings.StaticFilesPaths[0], Is.EqualTo("stl"));
		Assert.That(settings.StaticFilesPaths[1], Is.EqualTo("img"));
		Assert.That(settings.StringTableFiles.Count, Is.EqualTo(1));
		Assert.That(settings.StringTableFiles[0], Is.EqualTo("MyStr.xml"));
		Assert.That(settings.DisableAutomaticSiteTitleSet, Is.True);
		Assert.That(settings.HideExceptionDetails, Is.True);
		Assert.That(settings.ErrorPageDarkStyle, Is.True);
		Assert.That(settings.TemplatesMemoryCache, Is.True);
		Assert.That(settings.StringTableMemoryCache, Is.True);
		Assert.That(settings.DisableFileReaderCache, Is.True);
		Assert.That(settings.MeasurementsEnabled, Is.True);
		Assert.That(settings.ConsoleTracing, Is.True);
	}
}