using System;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public partial class FileReaderTests
{
	public const string DataPath = "WebSites/FooSite/App_Data/";

	private FileReader _fileReader = null!;

	private Mock<ILanguageManagerProvider> _languageManagerProvider = null!;
	private Mock<ILanguageManager> _languageManager = null!;

	[SetUp]
	public void Initialize()
	{
		_languageManagerProvider = new Mock<ILanguageManagerProvider>();
		_languageManager = new Mock<ILanguageManager>();

		_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);
		_languageManager.SetupGet(x => x.Language).Returns("ru");

		_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
		_fileReader.Setup();
	}

	[Test]
	public void GetFilePath_NullsPassed_ArgumentNullExceptionsThrown()
	{
		// Act
		_fileReader.Setup();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => _fileReader.GetFilePath(null, null));
		Assert.Throws<ArgumentNullException>(() => _fileReader.GetFilePath("File", null));
	}

	[Test]
	public void GetFilePath_CommonFile_PathIsCorrect()
	{
		// Arrange
		_languageManager.SetupGet(x => x.Language).Returns("en");

		// Act & Assert
		Assert.That(_fileReader.GetFilePath("My.Project\\Foo.xml"), Is.EqualTo(DataPath + "My.Project\\Foo.en.xml"));
	}

	[Test]
	public void GetFilePath_FileWithoutExtension_PathIsCorrect() =>
		// Act & Assert
		Assert.That(_fileReader.GetFilePath("MyProject\\Foo", "en"), Is.EqualTo(DataPath + "MyProject\\Foo.en"));
}