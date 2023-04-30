using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Modules.Data;

[TestFixture]
public class DataCollectorTests
{
	private Mock<IStringTable> _stringTable = null!;
	private DataCollector _dataCollector = null!;

	[SetUp]
	public void Initialize()
	{
		_stringTable = new Mock<IStringTable>();
		_dataCollector = new DataCollector("MainContent", "Title", _stringTable.Object);
	}

	[Test]
	public void AddVariableWithTest_NotExist_Created()
	{
		// Act
		_dataCollector.Add("Foo", "Bar");

		// Assert
		Assert.AreEqual(1, _dataCollector.Items.Count);
		Assert.AreEqual("Bar", _dataCollector["Foo"]);
		Assert.IsTrue(_dataCollector.IsDataExist("Foo"));
	}

	[Test]
	public void AddVariableWithTest_Exist_AddedToExisting()
	{
		// Act
		_dataCollector.Add("Foo", "Bar");
		_dataCollector.Add("Foo", "Test");

		// Assert
		Assert.AreEqual(1, _dataCollector.Items.Count);
		Assert.AreEqual("BarTest", _dataCollector["Foo"]);
	}

	[Test]
	public void AddVariableWithTest_AnotherVariable_2Variables()
	{
		// Act
		_dataCollector.Add("Foo", "Bar");
		_dataCollector.Add("Foo2", "Test");

		// Assert
		Assert.AreEqual(2, _dataCollector.Items.Count);
		Assert.AreEqual("Bar", _dataCollector["Foo"]);
		Assert.AreEqual("Test", _dataCollector["Foo2"]);
	}

	[Test]
	public void AddVariableWithTemplate_Null_NotAdded()
	{
		// Act
		_dataCollector.Add("Foo", (ITemplate)null);

		// Assert
		Assert.AreEqual(0, _dataCollector.Items.Count);
	}

	[Test]
	public void AddVariableWithTemplate_NormalData_Added()
	{
		// Act
		_dataCollector.Add("Foo", TemplateBuilder.FromString("Bar").Build());

		// Assert
		Assert.AreEqual("Bar", _dataCollector["Foo"]);
	}

	[Test]
	public void AddMainContentVariableWithText_NormalData_AddedToMainContentVariable()
	{
		// Act
		_dataCollector.Add("Foo");

		// Assert
		Assert.AreEqual("Foo", _dataCollector["MainContent"]);
	}

	[Test]
	public void AddMainContentVariableWithTemplate_NormalData_Added()
	{
		// Act
		_dataCollector.Add("Foo", TemplateBuilder.FromString("Bar").Build());

		// Assert
		Assert.AreEqual("Bar", _dataCollector["Foo"]);
	}

	[Test]
	public void AddTitleWithText_NormalData_Added()
	{
		// Act
		_dataCollector.AddTitle("Foo");

		// Assert
		Assert.AreEqual("Foo", _dataCollector["Title"]);
	}

	[Test]
	public void AddStWithVariableName_NormalData_Added()
	{
		// Assign
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == "Bar"))).Returns("Test");

		// Act
		_dataCollector.AddSt("Foo", "Bar");

		// Assert
		Assert.AreEqual("Test", _dataCollector["Foo"]);
	}

	[Test]
	public void AddStMainContentVariable_NormalData_Added()
	{
		// Assign
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == "Bar"))).Returns("Test");

		// Act
		_dataCollector.AddSt("Bar");

		// Assert
		Assert.AreEqual("Test", _dataCollector["MainContent"]);
	}

	[Test]
	public void AddStTitleVariable_NormalData_Added()
	{
		// Assign
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == "Bar"))).Returns("Test");

		// Act
		_dataCollector.AddTitleSt("Bar");

		// Assert
		Assert.AreEqual("Test", _dataCollector["Title"]);
	}
}