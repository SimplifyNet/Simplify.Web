using System.Xml.Linq;
using NUnit.Framework;
using Simplify.Web.Modules.Data;
using Simplify.Xml;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public partial class FileReaderTests
{
	[Test]
	public void LoadXDocument_FileNotExist_Null() =>
		// Act & Assert
		Assert.That(_fileReader.LoadTextDocument("NotExist.xml"), Is.Null);

	[Test]
	public void LoadXDocument_FileExist_Loaded() =>
		// Act & Assert
		Assert.That(_fileReader.LoadXDocument("Foo.xml")!.Root!.OuterXml(),
			Is.EqualTo(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>").Root!.OuterXml()));

	[Test]
	public void LoadXDocument_FileNameWithoutExtensionFileExist_Loaded() =>
		// Act & Assert
		Assert.That(_fileReader.LoadXDocument("Foo")!.Root!.OuterXml(),
			Is.EqualTo(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>").Root!.OuterXml()));

	[Test]
	public void LoadXDocument_FileNotExistButDefaultFileExist_DefaultFile() =>
		// Act & Assert
		Assert.That(_fileReader.LoadXDocument("Bar.xml")!.Root!.OuterXml(),
			Is.EqualTo(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>en bar data</data>").Root!.OuterXml()));

	[Test]
	public void LoadXDocument_CacheEnabled_SecondTimeFromCache()
	{
		// Arrange
		FileReader.ClearCache();

		// Act

		_fileReader.LoadXDocument("Foo.xml", true);

		_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
		_fileReader.Setup();

		var result = _fileReader.LoadXDocument("Foo.xml", true)!;

		// Assert

		Assert.That(result.Root!.OuterXml(),
			Is.EqualTo(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>").Root!.OuterXml()));
	}

	[Test]
	public void LoadXDocument_CacheEnabledDefaultFile_DefaultFileFromCache()
	{
		// Arrange
		FileReader.ClearCache();

		// Act

		_fileReader.LoadXDocument("Bar.xml", true);

		_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
		_fileReader.Setup();

		var result = _fileReader.LoadXDocument("Bar.xml", true)!;

		// Assert

		Assert.That(result.Root!.OuterXml(),
			Is.EqualTo(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>en bar data</data>").Root!.OuterXml()));
	}

	[Test]
	public void LoadXDocument_DefaultThenNonDefault_NonDefaultLoaded()
	{
		// Act & Assert

		Assert.That(_fileReader.LoadTextDocument("Foo.xml", "en", true), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><data>en data</data>"));
		Assert.That(_fileReader.LoadTextDocument("Foo.xml", "ru", true), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><data>ru data</data>"));
	}
}