#nullable disable

using System.Reflection;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Model.Validation;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes
{
	public class AttributesTestBase
	{
		protected PropertyInfo PropertyInfo;
		protected IDIResolver Resolver;

		protected ValidationAttribute Attr;

		[OneTimeSetUp]
		public void SetupPropertyInfo()
		{
			PropertyInfo = typeof(TestEntityWithProperty).GetProperty(nameof(TestEntityWithProperty.Prop1));
		}

		protected void TestAttributeForValidValue(object value)
		{
			// Act
			Attr.Validate(value, PropertyInfo, Resolver);
		}

		/// <summary>
		/// Perform validation attribute test
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="expectedExceptionMessage">The expected message.</param>
		/// <param name="customAttribute">The custom attribute.</param>
		protected void TestAttribute(object value, string expectedExceptionMessage, ValidationAttribute customAttribute = null)
		{
			// Act

			var ex = customAttribute != null
				? Assert.Throws<ModelValidationException>(() => customAttribute.Validate(value, PropertyInfo, Resolver))
				: Assert.Throws<ModelValidationException>(() => Attr.Validate(value, PropertyInfo, Resolver));

			// Assert
			Assert.AreEqual(expectedExceptionMessage, ex.Message);
		}
	}
}