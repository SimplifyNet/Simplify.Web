using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;
using System;

namespace Simplify.Web.Tests.Model.Validation.Attributes.MaxAttributeTests;

public class TypeMaxAttributeTests : AttributesTestBase
{
	public const string MaxValue = "12.5";

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MaxAttribute(typeof(decimal), MaxValue);

	[Test]
	public void Validate_BelowMaxValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue((decimal)10);
	}

	[Test]
	public void Validate_MaxValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue((decimal)12.5);
	}

	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Assign

		var value = (decimal)15;
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' required maximum value is {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_NullValue_NoExceptions()
	{
		// Act & Assert
		TestAttributeForValidValue(null);
	}

	[Test]
	public void Validate_DifferentTypes_ExceptionThrown()
	{
		// Act & Assert
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue(15.2d));
	}

	[Test]
	public void Validate_ObjectValueIsNotIComparable_ExceptionThrown()
	{
		// Act & Assert
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue(new object()));
	}
}