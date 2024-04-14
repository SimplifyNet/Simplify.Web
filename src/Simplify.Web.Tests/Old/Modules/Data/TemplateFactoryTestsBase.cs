using Moq;
using NUnit.Framework;
using Simplify.Web.Old.Modules;

namespace Simplify.Web.Tests.Old.Modules.Data;

public class TemplateFactoryTestsBase
{
	protected Mock<IEnvironment> Environment = null!;
	protected Mock<ILanguageManagerProvider> LanguageManagerProvider = null!;
	protected Mock<ILanguageManager> LanguageManager = null!;

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