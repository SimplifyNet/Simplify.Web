using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Binding.Parsers;
using Simplify.Web.Tests.Model.Binding.Parsers.ListToModelParserTestTypes;

namespace Simplify.Web.Tests.Model.Binding.Parsers;

[TestFixture]
public class ListToModelParserTests
{
	[Test]
	public void Parse_DefaultKeyValuePair_Null()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			default
		};

		// Act
		var model = ListToModelParser.Parse<TestModelUndefinedType>(coll);

		// Assert
		Assert.That(model.Prop1, Is.Null);
	}

	[Test]
	public void Parse_EmptyArray_Null()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("Prop1", [])
		};

		// Act
		var model = ListToModelParser.Parse<TestModelUndefinedType>(coll);

		// Assert
		Assert.That(model.Prop1, Is.Null);
	}

	[Test]
	public void Parse_DataTypeMismatch_ModelNotSupportedExceptionThrown()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("Prop1", ["test"])
		};

		// Act & Assert
		Assert.Throws<ModelNotSupportedException>(() => ListToModelParser.Parse<TestModelUndefinedType>(coll));
	}

	[Test]
	public void Parse_DateTimeNormal_Bind()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("Prop1", ["15--2014--03"]),
			new("Prop2", ["2014-03-16T00:00:00.0000000"])
		};

		// Act
		var obj = ListToModelParser.Parse<TestModelDateTime>(coll);

		// Assert

		Assert.That(obj.Prop1, Is.EqualTo(new DateTime(2014, 03, 15, 0, 0, 0, DateTimeKind.Utc)));
		Assert.That(obj.Prop2, Is.EqualTo(new DateTime(2014, 03, 16, 0, 0, 0, DateTimeKind.Utc)));
	}

	[Test]
	public void Parse_StringArray_Parsed()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("Prop1", ["asd", "qwe"])
		};

		// Act
		var obj = ListToModelParser.Parse<TestModelStringsList>(coll)!;

		// Assert

		Assert.That(obj.Prop1![0], Is.EqualTo("asd"));
		Assert.That(obj.Prop1[1], Is.EqualTo("qwe"));
	}

	[Test]
	public void Parse_WithBindProperty_Parsed()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("Prop1", ["test1"]),
			new("Prop2", ["test2"])
		};

		// Act
		var obj = ListToModelParser.Parse<TestModelWithBindProperty>(coll);

		// Assert
		Assert.That(obj.Prop1, Is.EqualTo("test2"));
	}

	[Test]
	public void Parse_WithExcludedProperty_Ignored()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("Prop1", ["test"])
		};

		// Act
		var obj = ListToModelParser.Parse<TestModelWithExcludedProperty>(coll);

		// Assert
		Assert.That(obj.Prop1, Is.Null);
	}

	[Test]
	public void Parse_StringsArray_ModelNotSupportedExceptionThrown()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("Prop1", ["val1", "val2"])
		};

		// Act & Assert
		Assert.Throws<ModelNotSupportedException>(() => ListToModelParser.Parse<TestModelStringsArray>(coll));
	}

	[Test]
	public void Parse_DifferentFieldCase_Parsed()
	{
		// Arrange
		var coll = new List<KeyValuePair<string, string[]>>
		{
			new("prop1", ["test"])
		};

		// Act
		var model = ListToModelParser.Parse<TestModel>(coll);

		// Assert
		Assert.That(model.Prop1, Is.EqualTo("test"));
	}
}