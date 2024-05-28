using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;
using System;

namespace Simplify.Web.Tests.Model.Validation.Attributes.MaxAttributeTests;

public class IntegerMaxAttributeTests : AttributesTestBase
{
	public const int MaxValue = 12;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MaxAttribute(MaxValue);

	[Test]
	public void Validate_BelowMaxValue_Ok() =>
		// Act & Assert
		TestAttributeForValidValue(10);

	[Test]
	public void Validate_MaxValueEqualsValue_Ok() =>
		// Act & Assert
		TestAttributeForValidValue(12);

	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Arrange

		var value = 15;
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' required maximum value is {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_NullValue_NoExceptions() =>
		// Act & Assert
		TestAttributeForValidValue(null);

	[Test]
	public void Validate_DifferentTypes_ExceptionThrown() =>
		// Act & Assert
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue((long)15.2));
}