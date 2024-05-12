using System;
using System.IO;
using NUnit.Framework;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public class TemplateFactoryTests : TemplateFactoryTestsBase
{
	private const string FileName = "Foo.tpl";

	[Test]
	public void Load_NullFileName_ArgumentNullExceptionThrown()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

		// Act
		tf.Setup();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => tf.Load(null!));
	}

	[Test]
	public void Load_NoCache_TemplateLoadedCorrectly()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

		// Act

		tf.Setup();
		var data = tf.Load(FileName);

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));
	}

	[Test]
	public void Load_NameWithoutTpl_TemplateLoadedCorrectly()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

		// Act

		tf.Setup();
		var data = tf.Load("Foo");

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));
	}

	[Test]
	public void Load_WithCache_TemplateLoadedCorrectly()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true);

		// Act

		tf.Setup();
		var data = tf.Load(FileName);

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));

		// Arrange

		tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true);
		tf.Setup();

		File.Delete(Environment.Object.TemplatesPhysicalPath + "\\" + FileName);

		// Act
		data = tf.Load(FileName);

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));
	}

	[Test]
	public void Load_FromManifestEnabled_CalledCorrectlyPathFixedWithDots()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true, true);

		// Act

		tf.Setup();
		var result = tf.Load("Templates/Test.tpl");

		// Assert
		Assert.That(result.Get(), Is.EqualTo("Hello!"));
	}
}