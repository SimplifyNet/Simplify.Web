using System;
using NUnit.Framework;
using RangeAttribute = Simplify.Web.Old.Model.Validation.Attributes.RangeAttribute;

namespace Simplify.Web.Tests.Old.Model.Validation.Attributes;

[TestFixture]
public class RangeAttributeTests : AttributesTestBase
{
	public const int MinValue = 2;
	public const int MaxValue = 12;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new RangeAttribute(MinValue, MaxValue);

	[Test]
	public void Validate_ValueInRange_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(10);
	}

	[Test]
	public void Validate_BelowMinValue_ExceptionThrown()
	{
		// Assign

		var value = 1;
		var defaultMessage = $"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Assign

		var value = 13;
		var defaultMessage = $"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_MaxValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(12);
	}

	[Test]
	public void Validate_MinValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(2);
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
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue(15.2));

	}

	[Test]
	public void Validate_ValueTypeNotInheritIComparable_ExceptionThrown()
	{
		// Act & Assert
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue(new object()));
	}
}