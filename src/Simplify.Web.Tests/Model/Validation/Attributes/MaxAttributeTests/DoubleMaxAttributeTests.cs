using System;

using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes.MaxAttributeTests;

[TestFixture]
public class DoubleMaxAttributeTests : AttributesTestBase
{
	public const double MaxValue = 12d;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MaxAttribute(MaxValue);

	[Test]
	public void Validate_BelowMaxValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(10d);
	}

	[Test]
	public void Validate_MaxValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(12d);
	}

	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Assign

		var value = 15d;
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
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue((long)15.2));
	}
}