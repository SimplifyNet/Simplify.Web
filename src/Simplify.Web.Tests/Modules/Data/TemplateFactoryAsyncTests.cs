using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public class TemplateFactoryAsyncTests : TemplateFactoryTestsBase
{
	private const string FileName = "FooAsync.tpl";

	[Test]
	public async Task LoadAsync_FromManifestEnabled_CalledCorrectlyPathFixedWithDots()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true, true);

		// Act

		tf.Setup();
		var result = await tf.LoadAsync("Templates/Test.tpl");

		// Assert
		Assert.That(result.Get(), Is.EqualTo("Hello!"));
	}

	[Test]
	public async Task LoadAsync_NameWithoutTpl_TemplateLoadedCorrectly()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

		// Act

		tf.Setup();
		var data = await tf.LoadAsync("FooAsync");

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));
	}

	[Test]
	public async Task LoadAsync_NoCache_TemplateLoadedCorrectly()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

		// Act

		tf.Setup();
		var data = await tf.LoadAsync(FileName);

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));
	}

	[Test]
	public void LoadAsync_NullFileName_ArgumentNullExceptionThrown()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

		// Act
		tf.Setup();

		// Act & Assert
		var ex = Assert.Throws<AggregateException>(() => tf.LoadAsync(null!).Wait());

		// Assert
		Assert.That(ex?.InnerException?.GetType(), Is.EqualTo(typeof(ArgumentNullException)));
	}

	[Test]
	public async Task LoadAsync_WithCache_TemplateLoadedCorrectly()
	{
		// Arrange
		var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true);

		// Act

		tf.Setup();
		var data = await tf.LoadAsync(FileName);

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));

		// Arrange

		tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true);
		tf.Setup();

		File.Delete(Environment.Object.TemplatesPhysicalPath + "\\" + FileName);

		// Act
		data = await tf.LoadAsync(FileName);

		// Assert
		Assert.That(data.Get(), Is.EqualTo("Dummy data"));
	}
}