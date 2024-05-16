using System.Collections.Generic;
using System.Xml.Linq;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Tests.Modules.Data.TestTypes;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public class StringTableTests
{
	private const string DefaultLanguage = "en";

	private readonly IList<string> _stringTableFiles = new[] { "StringTable.xml" };

	private StringTable? _stringTable;

	private Mock<IFileReader> _fileReader = null!;

	private Mock<ILanguageManagerProvider> _languageManagerProvider = null!;
	private Mock<ILanguageManager> _languageManager = null!;

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
		Assert.That(_stringTable.Items.Count, Is.EqualTo(0));
	}

	[Test]
	public void Constructor_StringTableFound_ItemsLoadedCorrectly()
	{
		// Arrange
		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

		// Act

		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
		_stringTable.Setup();

		// Assert
		Assert.That(_stringTable.Items["SiteTitle"], Is.EqualTo("Your site title!"));
	}

	[Test]
	public void Constructor_CurrentLanguageEqualToDefaultLanguage_DefaultItemsNotLoaded()
	{
		// Arrange

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
		// Arrange

		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns((XDocument?)null);
		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.Is<string>(d => d == DefaultLanguage), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

		// Act

		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
		_stringTable.Setup();

		// Assert
		Assert.That(_stringTable.Items["SiteTitle"], Is.EqualTo("Your site title!"));
	}

	[Test]
	public void Constructor_StringTableWithMissingItems_MissingItemsLoadedFromDefaultStringTable()
	{
		// Arrange

		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"Item1\" value=\"Foo\" /></items>"));
		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.Is<string>(d => d == DefaultLanguage), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"Item1\" value=\"FooDef\" /><item name=\"Item2\" value=\"BarDef\" /></items>"));

		// Act

		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);
		_stringTable.Setup();

		// Assert

		Assert.That(_stringTable.Items["Item1"], Is.EqualTo("Foo"));
		Assert.That(_stringTable.Items["Item2"], Is.EqualTo("BarDef"));
	}

	[Test]
	public void GetAssociatedValue_EnumItems_GetCorrectly()
	{
		// Arrange

		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);

		// Act
		_stringTable.Setup();

		// Act & Assert

		Assert.That(_stringTable.GetAssociatedValue(FooEnum.FooItem1), Is.EqualTo("Foo"));
		Assert.That(_stringTable.GetAssociatedValue(FooEnum.FooItem2), Is.Null);
	}

	[Test]
	public void GetItem_ItemFound_Returned()
	{
		// Arrange

		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);

		// Act
		_stringTable.Setup();

		// Act & Assert
		Assert.That(_stringTable.GetItem("FooEnum.FooItem1"), Is.EqualTo("Foo"));
	}

	[Test]
	public void GetItem_ItemNotFound_Null()
	{
		// Arrange

		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object);

		// Act
		_stringTable.Setup();

		// Act & Assert
		Assert.That(_stringTable.GetItem("Foo"), Is.Null);
	}

	[Test]
	public void Constructor_CacheEnabled_LoadedFromCacheSecondTime()
	{
		// Arrange
		_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

		// Act

		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object, true);
		_stringTable.Setup();

		_stringTable = new StringTable(_stringTableFiles, DefaultLanguage, _languageManagerProvider.Object, _fileReader.Object, true);
		_stringTable.Setup();

		// Assert

		_fileReader.Verify(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
		Assert.That(_stringTable.Items["SiteTitle"], Is.EqualTo("Your site title!"));
	}
}