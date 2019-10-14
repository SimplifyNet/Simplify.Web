using System;
using System.Reflection;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Model.Validation;
using Simplify.Web.Model.Validation.Attributes;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Model.Validation.Attributes
{
	[TestFixture]
	public class RequiredAttributeTests
	{
		private PropertyInfo _propertyInfo;

		[OneTimeSetUp]
		public void Initialize()
		{
			_propertyInfo = typeof(TestEntityWithProperty).GetProperty(nameof(TestEntityWithProperty.Prop1));
		}

		[Test]
		public void Validate_NotNullReference_NoExceptions()
		{
			// Assign
			var attr = new RequiredAttribute();

			// Act
			attr.Validate(new object(), _propertyInfo, null);
		}

		[Test]
		public void Validate_DefaultInt_NoExceptions()
		{
			// Assign
			var attr = new RequiredAttribute();

			// Act
			attr.Validate(default(int), _propertyInfo, null);
		}

		[Test]
		public void Validate_NullReference_DefaultModelValidationException()
		{
			// Assign
			var attr = new RequiredAttribute();

			// Act
			var ex = Assert.Throws<ModelValidationException>(() => attr.Validate(null, _propertyInfo, null));

			// Assert
			Assert.That(ex.Message,
				Does.StartWith($"Required property '{nameof(TestEntityWithProperty.Prop1)}' is null or empty"));
		}

		[Test]
		public void Validate_DefaultDateTime_DefaultModelValidationException()
		{
			// Assign
			var attr = new RequiredAttribute();

			// Act
			var ex = Assert.Throws<ModelValidationException>(() => attr.Validate(default(DateTime), _propertyInfo, null));

			// Assert
			Assert.AreEqual($"Required property '{nameof(TestEntityWithProperty.Prop1)}' is null or empty", ex.Message);
		}

		[Test]
		public void Validate_NullReferenceWithCustomError_ModelValidationExceptionWithCustomError()
		{
			// Assign
			var attr = new RequiredAttribute("Hello world!", false);

			// Act
			var ex = Assert.Throws<ModelValidationException>(() => attr.Validate(null, _propertyInfo, null));

			// Assert
			Assert.AreEqual("Hello world!", ex.Message);
		}

		[Test]
		public void Validate_NullReferenceWithMessageFromStringTable_ModelValidationExceptionWithMessageFromStringTable()
		{
			// Assign

			var attr = new RequiredAttribute("MyKey");
			var st = Mock.Of<IStringTable>(x => x.GetItem(It.Is<string>(s => s == "MyKey")) == "Hello world!");
			var resolver = Mock.Of<IDIResolver>(x => x.Resolve(It.Is<Type>(t => t == typeof(IStringTable))) == st);

			// Act
			var ex = Assert.Throws<ModelValidationException>(() => attr.Validate(null, _propertyInfo, resolver));

			// Assert
			Assert.AreEqual("Hello world!", ex.Message);
		}
	}
}