using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Binding.Parsers;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Model.Binding.Parsers
{
	[TestFixture]
	public class ListToModelParserTests
	{
		[Test]
		public void Parse_DefaultKeyValuePair_Null()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				default(KeyValuePair<string, string[]>)
			};

			// Act
			var model = ListToModelParser.Parse<TestModelUndefinedType>(coll);

			// Assert
			Assert.IsNull(model.Prop1);
		}

		[Test]
		public void Parse_EmptyArray_Null()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new string[0])
			};

			// Act
			var model = ListToModelParser.Parse<TestModelUndefinedType>(coll);

			// Assert
			Assert.IsNull(model.Prop1);
		}

		[Test]
		public void Parse_DataTypeMismatch_ModelNotSupportedExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"test"})
			};

			// Act & Assert
			Assert.Throws<ModelNotSupportedException>(() => ListToModelParser.Parse<TestModelUndefinedType>(coll));
		}

		[Test]
		public void Parse_DateTimeNormal_Binded()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"15--2014--03"}),
				new KeyValuePair<string, string[]>("Prop2", new []{"2014-03-16T00:00:00.0000000"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelDateTime>(coll);

			// Assert

			Assert.AreEqual(new DateTime(2014, 03, 15), obj.Prop1);
			Assert.AreEqual(new DateTime(2014, 03, 16), obj.Prop2);
		}

		[Test]
		public void Parse_StringArray_Parsed()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"asd", "qwe"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelStringsList>(coll);

			// Assert

			Assert.AreEqual("asd", obj.Prop1[0]);
			Assert.AreEqual("qwe", obj.Prop1[1]);
		}

		[Test]
		public void Parse_WithBindProperty_Parsed()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"test1"}),
				new KeyValuePair<string, string[]>("Prop2", new []{"test2"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelWithBindProperty>(coll);

			// Assert
			Assert.AreEqual("test2", obj.Prop1);
		}

		[Test]
		public void Parse_WithExcludedProperty_Ignored()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"test"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelWithExcludedProperty>(coll);

			// Assert
			Assert.IsNull(obj.Prop1);
		}

		[Test]
		public void Parse_StringsArray_ModelNotSupportedExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"val1", "val2"})
			};

			// Act & Assert
			Assert.Throws<ModelNotSupportedException>(() => ListToModelParser.Parse<TestModelStringsArray>(coll));
		}
	}
}