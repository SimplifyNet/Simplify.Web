using System;
using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public class DataCollectorTests
{
	private DataCollector _dataCollector = null!;

	private Mock<IStringTable> _stringTable = null!;

	[SetUp]
	public void Initialize()
	{
		_stringTable = new Mock<IStringTable>();

		_dataCollector = new DataCollector("MainContent", "Title", _stringTable.Object);
	}

	[Test]
	public void AddVariableWithTest_Nulls_ArgumentException() =>
		// Act & Assert
		Assert.Throws<ArgumentException>(() => _dataCollector.Add(null, (string?)null));

	[Test]
	public void AddVariableWithTest_NotExist_Created()
	{
		// Act
		_dataCollector.Add("Foo", "Bar");

		// Assert

		Assert.That(_dataCollector.Items.Count, Is.EqualTo(1));
		Assert.That(_dataCollector["Foo"], Is.EqualTo("Bar"));
		Assert.That(_dataCollector.IsDataExist("Foo"), Is.True);
	}

	[Test]
	public void AddVariableWithTest_Exist_AddedToExisting()
	{
		// Act

		_dataCollector.Add("Foo", "Bar");
		_dataCollector.Add("Foo", "Test");

		// Assert

		Assert.That(_dataCollector.Items.Count, Is.EqualTo(1));
		Assert.That(_dataCollector["Foo"], Is.EqualTo("BarTest"));
	}

	[Test]
	public void AddVariableWithTest_AnotherVariable_2Variables()
	{
		// Act

		_dataCollector.Add("Foo", "Bar");
		_dataCollector.Add("Foo2", "Test");

		// Assert

		Assert.That(_dataCollector.Items.Count, Is.EqualTo(2));
		Assert.That(_dataCollector["Foo"], Is.EqualTo("Bar"));
		Assert.That(_dataCollector["Foo2"], Is.EqualTo("Test"));
	}

	[Test]
	public void AddVariableWithTemplate_Null_NotAdded()
	{
		// Act
		_dataCollector.Add("Foo", (ITemplate?)null);

		// Assert
		Assert.That(_dataCollector.Items.Count, Is.EqualTo(0));
	}

	[Test]
	public void AddVariableWithTemplate_NormalData_Added()
	{
		// Act
		_dataCollector.Add("Foo", TemplateBuilder.FromString("Bar").Build());

		// Assert
		Assert.That(_dataCollector["Foo"], Is.EqualTo("Bar"));
	}

	[Test]
	public void AddMainContentVariableWithText_NormalData_AddedToMainContentVariable()
	{
		// Act
		_dataCollector.Add("Foo");

		// Assert
		Assert.That(_dataCollector["MainContent"], Is.EqualTo("Foo"));
	}

	[Test]
	public void AddMainContentVariableWithTemplate_NormalData_Added()
	{
		// Act
		_dataCollector.Add("Foo", TemplateBuilder.FromString("Bar").Build());

		// Assert
		Assert.That(_dataCollector["Foo"], Is.EqualTo("Bar"));
	}

	[Test]
	public void AddTitleWithText_NormalData_Added()
	{
		// Act
		_dataCollector.AddTitle("Foo");

		// Assert
		Assert.That(_dataCollector["Title"], Is.EqualTo("Foo"));
	}

	[Test]
	public void AddStWithVariableName_NormalData_Added()
	{
		// Arrange
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == "Bar"))).Returns("Test");

		// Act
		_dataCollector.AddSt("Foo", "Bar");

		// Assert
		Assert.That(_dataCollector["Foo"], Is.EqualTo("Test"));
	}

	[Test]
	public void AddStMainContentVariable_NormalData_Added()
	{
		// Arrange
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == "Bar"))).Returns("Test");

		// Act
		_dataCollector.AddSt("Bar");

		// Assert
		Assert.That(_dataCollector["MainContent"], Is.EqualTo("Test"));
	}

	[Test]
	public void AddStTitleVariable_NormalData_Added()
	{
		// Arrange
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == "Bar"))).Returns("Test");

		// Act
		_dataCollector.AddTitleSt("Bar");

		// Assert
		Assert.That(_dataCollector["Title"], Is.EqualTo("Test"));
	}
}