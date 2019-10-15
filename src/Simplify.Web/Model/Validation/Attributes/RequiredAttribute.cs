﻿using System;
using System.Reflection;
using Simplify.DI;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Indicates what this property should be not null or empty
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute : ValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredAttribute" /> class.
		/// </summary>
		/// <param name="errorMessage">The custom error message, should contain string table item key if messageFromStringTable is true.</param>
		/// <param name="isMessageFromStringTable">if set to <c>true</c> then indicates that errorMessage is containing string table item key instead of string error message.</param>
		public RequiredAttribute(string errorMessage = null, bool isMessageFromStringTable = true) : base(errorMessage, isMessageFromStringTable)
		{
		}

		/// <summary>
		/// Validates the specified property value.
		/// </summary>
		/// <param name="value">The object value.</param>
		/// <param name="propertyInfo">Information about the property containing this attribute.</param>
		/// <param name="resolver">The objects resolver, useful if you need to retrieve some dependencies to perform validation.</param>
		/// <exception cref="ModelValidationException">
		/// Required property '{propertyInfo.Name}' is null or empty
		/// or
		/// or
		/// </exception>
		public override void Validate(object value, PropertyInfo propertyInfo, IDIResolver resolver)
		{
			var objectIsValid = false;

			if (value != null)
			{
				var objectType = value.GetType();

				if (objectType == typeof(DateTime))
					objectIsValid = !value.Equals(default(DateTime));
				else
					objectIsValid = true;
			}

			if (objectIsValid)
				return;

			TryThrowCustomOrStringTableException(resolver);

			throw new ModelValidationException($"Required property '{propertyInfo.Name}' is null or empty");
		}
	}
}