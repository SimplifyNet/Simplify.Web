using System.IO.Abstractions;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public class FileReaderTests2
{
	private const string DataPath = "";

	private Mock<ILanguageManagerProvider> _languageManagerProvider;
	private Mock<ILanguageManager> _languageManager;

	private Mock<IFileSystem> _fs;

	private FileReader _fileReader;

	[SetUp]
	public void Initialize()
	{
		_languageManager = new Mock<ILanguageManager>();
		_languageManager.SetupGet(x => x.Language).Returns("ru");

		_languageManagerProvider = new Mock<ILanguageManagerProvider>();
		_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);

		_fs = new Mock<IFileSystem>();
		FileReader.FileSystem = _fs.Object;

		_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
		_fileReader.Setup();
	}

	#region LoadTextDocument

	[Test]
	public void LoadTextDocument_DefaultThenNonDefault_NonDefaultLoaded()
	{
		// Assign

		_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "Foo.en.txt"))).Returns(true);
		_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "Foo.en.txt"))).Returns("en data");

		_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "Foo.ru.txt"))).Returns(true);
		_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "Foo.ru.txt"))).Returns("ru data");

		// Act & Assert

		Assert.AreEqual("en data", _fileReader.LoadTextDocument("Foo.txt", "en", true));
		Assert.AreEqual("ru data", _fileReader.LoadTextDocument("Foo.txt", "ru", true));
	}

	#endregion LoadTextDocument

	#region LoadXDocument

	[Test]
	public void LoadXDocument_DefaultThenNonDefault_NonDefaultLoaded()
	{
		// Assign

		_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "Foo.en.xml"))).Returns(true);
		_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "Foo.en.xml"))).Returns("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>en data</data>");

		_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "Foo.ru.xml"))).Returns(true);
		_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "Foo.ru.xml"))).Returns("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>");

		// Act & Assert

		Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>en data</data>", _fileReader.LoadTextDocument("Foo.xml", "en", true));
		Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>", _fileReader.LoadTextDocument("Foo.xml", "ru", true));
	}

	#endregion LoadXDocument
}