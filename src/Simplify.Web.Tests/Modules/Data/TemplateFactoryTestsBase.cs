﻿using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Localization;

namespace Simplify.Web.Tests.Modules.Data;

public class TemplateFactoryTestsBase
{
	protected Mock<IDynamicEnvironment> Environment = null!;
	protected Mock<ILanguageManagerProvider> LanguageManagerProvider = null!;
	protected Mock<ILanguageManager> LanguageManager = null!;

	[OneTimeSetUp]
	public void Initialize()
	{
		Environment = new Mock<IDynamicEnvironment>();
		LanguageManagerProvider = new Mock<ILanguageManagerProvider>();
		LanguageManager = new Mock<ILanguageManager>();

		Environment.SetupGet(x => x.TemplatesPhysicalPath).Returns("Templates");
		LanguageManagerProvider.Setup(x => x.Get()).Returns(LanguageManager.Object);
		LanguageManager.SetupGet(x => x.Language).Returns("en");
	}
}