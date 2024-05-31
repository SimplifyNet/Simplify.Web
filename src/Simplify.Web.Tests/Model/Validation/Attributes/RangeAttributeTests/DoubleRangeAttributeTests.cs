using System;

using NUnit.Framework;
using RangeAttribute = Simplify.Web.Model.Validation.Attributes.RangeAttribute;

namespace Simplify.Web.Tests.Model.Validation.Attributes.RangeAttributeTests;

[TestFixture]
public class DoubleRangeAttributeTests : AttributesTestBase
{
	public const double MinValue = 2d;
	public const double MaxValue = 12d;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new RangeAttribute(MinValue, MaxValue);

	[Test]
	public void Validate_ValueInRange_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(10d);
	}

	[Test]
	public void Validate_BelowMinValue_ExceptionThrown()
	{
		// Assign

		var value = 1d;
		var defaultMessage = $"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Assign

		var value = 13d;
		var defaultMessage = $"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_MaxValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(12d);
	}

	[Test]
	public void Validate_MinValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(2d);
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
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue((long)7));
	}
}