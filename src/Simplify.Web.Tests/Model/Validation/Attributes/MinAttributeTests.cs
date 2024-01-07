using System;
using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes;

[TestFixture]
public class MinAttributeTests : AttributesTestBase
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
		// Assign

		var value = 12.5;
		var defaultMessage = "Type mismatch. The minimum value and property value should be of the same type.";

		// Act & Assert
		TestAttribute(value, defaultMessage); 
	}
	
	[Test]
	public void Validate_ValueTypeNotInheritIComparable_ExceptionThrown()
	{
		// Assign

		var value = new object();
		var defaultMessage = $"The type of specified property value must be inherited from {typeof(IComparable)}";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
}