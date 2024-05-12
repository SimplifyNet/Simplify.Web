using NUnit.Framework;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public partial class FileReaderTests
{
	[Test]
	public void LoadTextDocument_FileNotExist_Null() =>
		// Act & Assert
		Assert.That(_fileReader.LoadTextDocument("NotExist.txt"), Is.Null);

	[Test]
	public void LoadTextDocument_FileExist_Loaded() =>
		// Act & Assert
		Assert.That(_fileReader.LoadTextDocument("Foo.txt"), Is.EqualTo("ru data"));

	[Test]
	public void LoadTextDocument_FileNotExistButDefaultFileExist_DefaultFile() =>
		// Act & Assert
		Assert.That(_fileReader.LoadTextDocument("Bar.txt"), Is.EqualTo("en bar data"));

	[Test]
	public void LoadTextDocument_CacheEnabled_SecondTimeFromCache()
	{
		// Arrange
		FileReader.ClearCache();

		// Act

		_fileReader.LoadTextDocument("Foo.txt", true);

		_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
		_fileReader.Setup();

		var result = _fileReader.LoadTextDocument("Foo.txt", true);

		// Assert
		Assert.That(result, Is.EqualTo("ru data"));
	}

	[Test]
	public void LoadTextDocument_CacheEnabledDefaultFile_DefaultFileFromCache()
	{
		// Arrange
		FileReader.ClearCache();
		// Act

		_fileReader.LoadTextDocument("Bar.txt", true);

		_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
		_fileReader.Setup();

		var result = _fileReader.LoadTextDocument("Bar.txt", true);

		// Assert

		Assert.That(result, Is.EqualTo("en bar data"));
	}

	[Test]
	public void LoadTextDocument_DefaultThenNonDefault_NonDefaultLoaded()
	{
		// Act & Assert

		Assert.That(_fileReader.LoadTextDocument("Foo.txt", "en", true), Is.EqualTo("en data"));
		Assert.That(_fileReader.LoadTextDocument("Foo.txt", "ru", true), Is.EqualTo("ru data"));
	}
}