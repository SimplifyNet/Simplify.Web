using System;

using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes.MaxAttributeTests;

public class LongMaxAttributeTests : AttributesTestBase
{
	public const long MaxValue = 12;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MaxAttribute(MaxValue);

	[Test]
	public void Validate_BelowMaxValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue((long)10);
	}

	[Test]
	public void Validate_MaxValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue((long)12);
	}

	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Assign

		var value = (long)15;
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
}