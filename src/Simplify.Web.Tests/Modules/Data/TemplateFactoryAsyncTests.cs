using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data
{
	[TestFixture]
	public class TemplateFactoryAsyncTests : TemplateFactoryTestsBase
	{
		private const string FileName = "FooAsync.tpl";

		[Test]
		public async Task LoadAsync_FromManifestEnabled_CalledCorrectlyPathFixedWithDots()
		{
			// Assign
			var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true, true);

			// Act

			tf.Setup();
			var result = await tf.LoadAsync("Templates/Test.tpl");

			// Assert
			Assert.AreEqual("Hello!", result.Get());
		}

		[Test]
		public async Task LoadAsync_NameWithoutTpl_TemplateLoadedCorrectly()
		{
			// Assign
			var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

			// Act

			tf.Setup();
			var data = await tf.LoadAsync("FooAsync");

			// Assert
			Assert.AreEqual("Dummy data", data.Get());
		}

		[Test]
		public async Task LoadAsync_NoCache_TemplateLoadedCorrectly()
		{
			// Assign
			var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

			// Act

			tf.Setup();
			var data = await tf.LoadAsync(FileName);

			// Assert
			Assert.AreEqual("Dummy data", data.Get());
		}

		[Test]
		public void LoadAsync_NullFileName_ArgumentNullExceptionThrown()
		{
			// Assign
			var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en");

			// Act
			tf.Setup();

			// Act & Assert
			var ex = Assert.Throws<AggregateException>(() => tf.LoadAsync(null!).Wait());

			// Assert
			Assert.AreEqual(typeof(ArgumentNullException), ex?.InnerException?.GetType());
		}

		[Test]
		public async Task LoadAsync_WithCache_TemplateLoadedCorrectly()
		{
			// Assign
			var tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true);

			// Act

			tf.Setup();
			var data = await tf.LoadAsync(FileName);

			// Asset
			Assert.AreEqual("Dummy data", data.Get());

			// Assign

			tf = new TemplateFactory(Environment.Object, LanguageManagerProvider.Object, "en", true);
			tf.Setup();

			File.Delete(Environment.Object.TemplatesPhysicalPath + "\\" + FileName);

			// Act
			data = await tf.LoadAsync(FileName);

			// Assert

			Assert.AreEqual("Dummy data", data.Get());
		}
	}
}