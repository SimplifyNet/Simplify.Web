#nullable disable

using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;

namespace Simplify.Web.Tests.Modules.Data
{
	[TestFixture]
	public class TemplateFactoryTestsBase
	{
		protected Mock<IEnvironment> Environment;
		protected Mock<ILanguageManagerProvider> LanguageManagerProvider;
		protected Mock<ILanguageManager> LanguageManager;

		[OneTimeSetUp]
		public void Initialize()
		{
			Environment = new Mock<IEnvironment>();
			LanguageManagerProvider = new Mock<ILanguageManagerProvider>();
			LanguageManager = new Mock<ILanguageManager>();

			Environment.SetupGet(x => x.TemplatesPhysicalPath).Returns("Templates");
			LanguageManagerProvider.Setup(x => x.Get()).Returns(LanguageManager.Object);
			LanguageManager.SetupGet(x => x.Language).Returns("en");
		}
	}
}