using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Old.Modules.Data;

[TestFixture]
public class FileReaderTests2
{
	private Mock<ILanguageManagerProvider> _languageManagerProvider = null!;
	private Mock<ILanguageManager> _languageManager = null!;

	private FileReader _fileReader = null!;

	[SetUp]
	public void Initialize()
	{
		_languageManager = new Mock<ILanguageManager>();
		_languageManager.SetupGet(x => x.Language).Returns("ru");

		_languageManagerProvider = new Mock<ILanguageManagerProvider>();
		_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);

		_fileReader = new FileReader(FileReaderTests.DataPath, "en", _languageManagerProvider.Object);
		_fileReader.Setup();
	}

	#region LoadTextDocument

	[Test]
	public void LoadTextDocument_DefaultThenNonDefault_NonDefaultLoaded()
	{
		// Act & Assert

		Assert.AreEqual("en data", _fileReader.LoadTextDocument("Foo.txt", "en", true));
		Assert.AreEqual("ru data", _fileReader.LoadTextDocument("Foo.txt", "ru", true));
	}

	#endregion LoadTextDocument

	#region LoadXDocument

	[Test]
	public void LoadXDocument_DefaultThenNonDefault_NonDefaultLoaded()
	{
		// Act & Assert

		Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\"?><data>en data</data>", _fileReader.LoadTextDocument("Foo.xml", "en", true));
		Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\"?><data>ru data</data>", _fileReader.LoadTextDocument("Foo.xml", "ru", true));
	}

	#endregion LoadXDocument
}