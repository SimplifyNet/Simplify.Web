#nullable disable

using System.Collections.Generic;
using System.Xml.Linq;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data
{
	public enum FooEnum
	{
		FooItem1,
		FooItem2
	}

	[TestFixture]
	public class StringTableTests
	{
		private const string DefaultLanguage = "en";

		private readonly IList<string> _stringTableFiles = new[] { "StringTable.xml" };
		private StringTable _stringTable;
		private Mock<IFileReader> _fileReader;

		private Mock<ILanguageManagerProvider> _languageManagerProvider;
		private Mock<ILanguageManager> _languageManager;

		[SetUp]
		public void Initialize()
		{
			_fileReader = new Mock<IFileReader>();
			_languageManagerProvider = new Mock<ILanguageManagerProvider>();
			_languageManager = new Mock<ILanguageManager>();

			_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);
			_languageManager.SetupGet(x => x.Language).Returns("ru");
		}

		[Test]
		public void Constructor_NoStringTable_NoItemsLoaded()
		{
			// Act

			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
			_stringTable.Setup();

			// Assert
			Assert.AreEqual(0, ((IDictionary<string, object>)_stringTable.Items).Count);
		}

		[Test]
		public void Constructor_StringTableFound_ItemsLoadedCorrectly()
		{
			// Assign
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

			// Act

			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
			_stringTable.Setup();

			// Assert
			Assert.AreEqual("Your site title!", _stringTable.Items.SiteTitle);
		}

		[Test]
		public void Constructor_CurrentLanguageEqualToDefaultLanguage_DefaultItemsNotLoaded()
		{
			// Assign

			_languageManager.SetupGet(x => x.Language).Returns("en");
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

			// Act

			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
			_stringTable.Setup();

			// Assert
			_fileReader.Verify(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
		}

		[Test]
		public void Constructor_StringTableNotFound_DefaultLoaded()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns((XDocument)null);
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.Is<string>(d => d == DefaultLanguage), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

			// Act

			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
			_stringTable.Setup();

			// Assert
			Assert.AreEqual("Your site title!", _stringTable.Items.SiteTitle);
		}

		[Test]
		public void Constructor_StringTableWithMissingItems_MissingItemsLoadedFromDefaultStringTable()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"Item1\" value=\"Foo\" /></items>"));
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.Is<string>(d => d == DefaultLanguage), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"Item1\" value=\"FooDef\" /><item name=\"Item2\" value=\"BarDef\" /></items>"));

			// Act

			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
			_stringTable.Setup();

			// Assert

			Assert.AreEqual("Foo", _stringTable.Items.Item1);
			Assert.AreEqual("BarDef", _stringTable.Items.Item2);
		}

		[Test]
		public void GetAssociatedValue_EnumItems_GetCorrectly()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);

			// Act
			_stringTable.Setup();

			// Act & Assert
			Assert.AreEqual("Foo", _stringTable.GetAssociatedValue(FooEnum.FooItem1));
			Assert.IsNull(_stringTable.GetAssociatedValue(FooEnum.FooItem2));
		}

		[Test]
		public void GetItem_ItemFound_Returned()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);

			// Act
			_stringTable.Setup();

			// Act & Assert
			Assert.AreEqual("Foo", _stringTable.GetItem("FooEnum.FooItem1"));
		}

		[Test]
		public void GetItem_ItemNotFound_Null()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);

			// Act
			_stringTable.Setup();

			// Act & Assert
			Assert.IsNull(_stringTable.GetItem("Foo"));
		}

		[Test]
		public void Constructor_CacheEnabled_LoadedFromCacheSecondTime()
		{
			// Assign
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

			// Act

			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object, true);
			_stringTable.Setup();

			_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object, true);
			_stringTable.Setup();

			// Assert

			_fileReader.Verify(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
			Assert.AreEqual("Your site title!", _stringTable.Items.SiteTitle);
		}
	}
}