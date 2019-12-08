#nullable disable

using System;
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
		public const string DataPath = "WebSites\\FooSite\\App_Data\\";

		private Mock<ILanguageManagerProvider> _languageManagerProvider;
		private Mock<ILanguageManager> _languageManager;

		private FileReader _fileReader;

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

		#region General

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
			Assert.AreEqual(DataPath + "My.Project\\Foo.en.xml", _fileReader.GetFilePath("My.Project\\Foo.xml"));
		}

		[Test]
		public void GetFilePath_FileWithoutExtension_PathIsCorrect()
		{
			// Act & Assert
			Assert.AreEqual(DataPath + "MyProject\\Foo.en", _fileReader.GetFilePath("MyProject\\Foo", "en"));
		}

		#endregion GetFilePath

		#region LoadXDocument

		[Test]
		public void LoadXDocument_FileNotExist_Null()
		{
			// Act & Assert
			Assert.IsNull(_fileReader.LoadTextDocument("NotExist.xml"));
		}

		[Test]
		public void LoadXDocument_FileExist_Loaded()
		{
			// Act & Assert
			Assert.AreEqual(
				XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>")
					.Root.OuterXml(), _fileReader.LoadXDocument("Foo.xml").Root.OuterXml());
		}

		[Test]
		public void LoadXDocument_FileNameWithoutExtensionFileExist_Loaded()
		{
			// Act & Assert
			Assert.AreEqual(
				XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>")
					.Root.OuterXml(), _fileReader.LoadXDocument("Foo").Root.OuterXml());
		}

		[Test]
		public void LoadXDocument_FileNotExistButDefaultFileExist_DefaultFile()
		{
			// Act & Assert
			Assert.AreEqual(XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>en bar data</data>")
					.Root.OuterXml(), _fileReader.LoadXDocument("Bar.xml").Root.OuterXml());
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

			Assert.AreEqual(XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>ru data</data>")
					.Root.OuterXml(), result.Root.OuterXml());
		}

		[Test]
		public void LoadXDocument_CacheEnabledDefaultFile_DefaultFileFromCache()
		{
			// Assign
			FileReader.ClearCache();

			// Act

			_fileReader.LoadXDocument("Bar.xml", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadXDocument("Bar.xml", true);

			// Assert

			Assert.AreEqual(XDocument.Parse(
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>en bar data</data>")
					.Root.OuterXml(), result.Root.OuterXml());
		}

		#endregion LoadXDocument

		#region LoadTextDocument

		[Test]
		public void LoadTextDocument_FileNotExist_Null()
		{
			// Act & Assert
			Assert.IsNull(_fileReader.LoadTextDocument("NotExist.txt"));
		}

		[Test]
		public void LoadTextDocument_FileExist_Loaded()
		{
			// Act & Assert
			Assert.AreEqual("ru data", _fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_FileNotExistButDefaultFileExist_DefaultFile()
		{
	// Act & Assert
			Assert.AreEqual("en bar data", _fileReader.LoadTextDocument("Bar.txt"));
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
			Assert.AreEqual("ru data", result);
		}

		[Test]
		public void LoadTextDocument_CacheEnabledDefaultFile_DefaultFileFromCache()
		{
			// Assign
			FileReader.ClearCache();
			// Act

			_fileReader.LoadTextDocument("Bar.txt", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadTextDocument("Bar.txt", true);

			// Assert

			Assert.AreEqual("en bar data", result);
		}

		#endregion LoadTextDocument
	}
}