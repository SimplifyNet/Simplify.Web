﻿using System;

using NUnit.Framework;
using RangeAttribute = Simplify.Web.Model.Validation.Attributes.RangeAttribute;

namespace Simplify.Web.Tests.Model.Validation.Attributes.RangeAttributeTests;

public class LongRangeAttributeTests : AttributesTestBase
{
	public const long MinValue = 2;
	public const long MaxValue = 12;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new RangeAttribute(MinValue, MaxValue);

	[Test]
	public void Validate_ValueInRange_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue((long)10);
	}

	[Test]
	public void Validate_BelowMinValue_ExceptionThrown()
	{
		// Assign

		var value = (long)1;
		var defaultMessage = $"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_AboveMaxValue_ExceptionThrown()
	{
		// Assign

		var value = (long)13;
		var defaultMessage = $"The value is out of range. The range constraint - {MinValue} - {MaxValue}, actual value: {value}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}

	[Test]
	public void Validate_MaxValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue((long)12);
	}

	[Test]
	public void Validate_MinValueEqualsValue_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue((long)2);
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
		Assert.Throws<ArgumentException>(() => TestAttributeForValidValue(7d));
	}
}