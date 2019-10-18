#nullable disable

using System;
using System.IO.Abstractions;
using System.Xml.Linq;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Xml;

namespace Simplify.Web.Tests.Modules.Data
{
	[TestFixture]
	public class FileReaderTests
	{
		private const string DataPath = "C:/WebSites/FooSite/App_Data/";

		private Mock<ILanguageManagerProvider> _languageManagerProvider;
		private Mock<ILanguageManager> _languageManager;

		private Mock<IFileSystem> _fs;

		private FileReader _fileReader;

		[SetUp]
		public void Initialize()
		{
			_languageManagerProvider = new Mock<ILanguageManagerProvider>();
			_languageManager = new Mock<ILanguageManager>();

			_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);
			_languageManager.SetupGet(x => x.Language).Returns("ru");

			_fs = new Mock<IFileSystem>();

			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.xml")))
				.Returns("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Title\" /></items>");

			// ReSharper disable once StringLiteralTypo
			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.xml")))
				.Returns("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок\" /></items>");

			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns("Dummy");

			// ReSharper disable once StringLiteralTypo
			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt")))
				.Returns("Тест");

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.xml")))
				.Returns(true);

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.xml")))
				.Returns(true);

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns(true);

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt")))
				.Returns(true);

			FileReader.FileSystem = _fs.Object;

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();
		}

		#region General

		[Test]
		public void FileSystem_NullsPassed_ArgumentNullExceptionThrown()
		{
			// Assert
			Assert.Throws<ArgumentNullException>(() => FileReader.FileSystem = null);
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

		#endregion General

		#region GetFilePath

		[Test]
		public void GetFilePath_CommonFile_PathIsCorrect()
		{
			// Assign
			_languageManager.SetupGet(x => x.Language).Returns("en");

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/My.Project/Foo.en.xml", _fileReader.GetFilePath("My.Project/Foo.xml"));
		}

		[Test]
		public void GetFilePath_FileWithoutExtension_PathIsCorrect()
		{
			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/MyProject/Foo.en", _fileReader.GetFilePath("MyProject/Foo", "en"));
		}

		#endregion GetFilePath

		#region LoadXDocument

		[Test]
		public void LoadXDocument_FileNotExist_Null()
		{
			// Assign
			_fs.Setup(x => x.File.Exists(It.IsAny<string>())).Returns(false);

			// Act & Assert
			Assert.IsNull(_fileReader.LoadTextDocument("Foo.xml"));
		}

		[Test]
		public void LoadXDocument_FileExist_Loaded()
		{
			// Act & Assert
			Assert.AreEqual(
				XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок\" /></items>")
					.Root.OuterXml(), _fileReader.LoadXDocument("Foo.xml").Root.OuterXml());
		}

		[Test]
		public void LoadXDocument_FileNameWithoutExtensionFileExist_Loaded()
		{
			// Act & Assert
			Assert.AreEqual(
				XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок\" /></items>")
					.Root.OuterXml(), _fileReader.LoadXDocument("Foo").Root.OuterXml());
		}

		[Test]
		public void LoadXDocument_FileNotExistButDefaultFileExist_DefaultFile()
		{
			// Assign
			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.xml"))).Returns(false);

			// Act & Assert
			Assert.AreEqual(XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Title\" /></items>")
					.Root.OuterXml(), _fileReader.LoadXDocument("Foo.xml").Root.OuterXml());
		}

		[Test]
		public void LoadXDocument_CacheEnabled_SecondTimeFromCache()
		{
			// Assign
			FileReader.ClearCache();

			// Act

			_fileReader.LoadXDocument("Foo.xml", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadXDocument("Foo.xml", true);

			// Assert

			_fs.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
			Assert.AreEqual(XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок\" /></items>")
					.Root.OuterXml(), result.Root.OuterXml());
		}

		[Test]
		public void LoadXDocument_CacheEnabledDefaultFile_DefaultFileFromCache()
		{
			// Assign
			FileReader.ClearCache();
			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.xml"))).Returns(false);

			// Act

			_fileReader.LoadXDocument("Foo.xml", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadXDocument("Foo.xml", true);

			// Assert

			_fs.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
			Assert.AreEqual(XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Title\" /></items>")
					.Root.OuterXml(), result.Root.OuterXml());
		}

		#endregion LoadXDocument

		#region LoadTextDocument

		[Test]
		public void LoadTextDocument_FileNotExist_Null()
		{
			// Assign
			_fs.Setup(x => x.File.Exists(It.IsAny<string>())).Returns(false);

			// Act & Assert
			Assert.IsNull(_fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_FileExist_Loaded()
		{
			// ReSharper disable once StringLiteralTypo

			// Act & Assert
			Assert.AreEqual("Тест", _fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_FileNotExistButDefaultFileExist_DefaultFile()
		{
			// Assign
			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt"))).Returns(false);

			// Act & Assert
			Assert.AreEqual("Dummy", _fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_CacheEnabled_SecondTimeFromCache()
		{
			// Assign
			FileReader.ClearCache();

			// Act

			_fileReader.LoadTextDocument("Foo.txt", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadTextDocument("Foo.txt", true);

			// Assert

			_fs.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);

			// ReSharper disable once StringLiteralTypo
			Assert.AreEqual("Тест", result);
		}

		[Test]
		public void LoadTextDocument_CacheEnabledDefaultFile_DefaultFileFromCache()
		{
			// Assign
			FileReader.ClearCache();
			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt"))).Returns(false);

			// Act

			_fileReader.LoadTextDocument("Foo.txt", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadTextDocument("Foo.txt", true);

			// Assert

			_fs.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
			Assert.AreEqual("Dummy", result);
		}

		#endregion LoadTextDocument
	}
}