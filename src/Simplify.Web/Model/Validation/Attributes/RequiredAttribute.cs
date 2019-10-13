using System;

namespace Simplify.Web.Model.Validation.Attributes
{
	/// <summary>
	/// Indicates what this property should be not null or empty
	/// </summary>
	//[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute : ValidationAttribute
	{
		private readonly string _stringTableKey;
		private readonly bool _messageFromStringTable;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredAttribute"/> class.
		/// </summary>
		/// <param name="stringTableKey">The string table key.</param>
		/// <param name="messageFromStringTable">if set to <c>true</c> [message from string table].</param>
		public RequiredAttribute(string stringTableKey = null, bool messageFromStringTable = true)
		{
			_stringTableKey = stringTableKey;
			_messageFromStringTable = messageFromStringTable;
		}

		/// <summary>
		/// Validates the specified property value.
		/// </summary>
		/// <param name="value">The value.</param>
		public override void Validate(object value)
		{
			throw new NotImplementedException();
		}
	}
}