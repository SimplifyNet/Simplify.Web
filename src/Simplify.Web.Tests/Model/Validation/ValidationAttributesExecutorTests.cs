using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.Model.Validation;
using Simplify.Web.Tests.Model.Validation.TestTypes;
using Simplify.Web.Tests.Model.Validation.TestTypes.Inheritance;
using Simplify.Web.Tests.Model.Validation.TestTypes.Nesting;

namespace Simplify.Web.Tests.Model.Validation;

[TestFixture]
public class ValidationAttributesExecutorTests
{
	private ValidationAttributesExecutor _validator = null!;

	[SetUp]
	public void Initialize() => _validator = new ValidationAttributesExecutor();

	[Test]
	public void Validate_ModelWithOnePropertyAndOneValidateAttribute_AttributeValidationCalled()
	{
		// Assign
		var model = new TestModel();

		// Act

		var ex = Assert.Throws<ModelValidationException>(() => _validator.Validate(model, null!));

		// Assert
		Assert.That(ex!.Message,
			Does.StartWith($"Required property '{nameof(TestEntityWithProperty.Prop1)}' is null or empty"));
	}

	[Test]
	public void Validate_NestedProperties_NestedNullAttributeException()
	{
		// Arrange
		var model = new NestingRootModel
		{
			NestedProperty = new NestedModel
			{
				NestedProperty = new SubNestedModel()
			},
			TestInt = 1
		};

		// Act

		var ex = Assert.Throws<ModelValidationException>(() => _validator.Validate(model, null!));

		// Assert
		Assert.That(ex!.Message,
			Does.StartWith($"Required property '{nameof(SubNestedModel.BuiltInType)}' is null or empty"));
	}

	[Test]
	public void Validate_InheritedProperty_NestedNullAttributeException()
	{
		// Arrange
		var model = new InheritanceRootModel
		{
			NestedProperty = new BaseNestedModel()
		};

		// Act
		var ex = Assert.Throws<ModelValidationException>(() => _validator.Validate(model, null!));

		// Assert
		Assert.That(ex!.Message,
			Does.StartWith($"Required property '{nameof(BaseNestedModel.BuiltInType)}' is null or empty"));
	}

	[Test]
	public void Validate_NotNullSystemTypes_NoExceptions()
	{
		// Arrange
		var model = new SystemTypesModel
		{
			StringProperty = "test",
			IntProperty = 23
		};

		// Act
		_validator.Validate(model, null!);
	}

	[Test]
	public void Validate_IList_NoExceptions()
	{
		// Arrange
		IList<TestModel> model = new List<TestModel>{
		{
			new() {Prop1 = "test"}
		}};

		// Act
		_validator.Validate(model, null!);
	}

	[Test]
	public void Validate_Array_NoExceptions()
	{
		// Arrange
		var model = new TestModel[]
		{
			new() { Prop1 = "test" }
		};

		// Act
		_validator.Validate(model, null!);
	}
}