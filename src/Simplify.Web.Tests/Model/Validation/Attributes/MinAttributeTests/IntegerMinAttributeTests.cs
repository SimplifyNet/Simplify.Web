using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;
using System;

namespace Simplify.Web.Tests.Model.Validation.Attributes.MinAttributeTests;

[TestFixture]
public class IntegerMinAttributeTests : AttributesTestBase
{
	public const int MinValue = 12;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MinAttribute(MinValue);

	[Test]
	public void Validate_AboveMinValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(15);
	}

	[Test]
	public void Validate_MinValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(12);
	}

	[Test]
	public void Validate_BelowMinValue_ExceptionThrown()
	{
		// Assign

		var value = 8;
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' required minimum value is {MinValue}, actual value: {value}";

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
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue(12.5));
	}
}