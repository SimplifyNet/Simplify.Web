using System;
using NUnit.Framework;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Binding.Parsers;
using Simplify.Web.Tests.Model.Binding.Parsers.TestTypes;

namespace Simplify.Web.Tests.Model.Binding.Parsers;

[TestFixture]
public class StringToSpecifiedObjectParserTests
{
	#region Validation

	[Test]
	public void IsTypeValidForParsing_String_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(string)));
	}

	[Test]
	public void IsTypeValidForParsing_Int_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(int)));
	}

	[Test]
	public void IsTypeValidForParsing_NullableInt_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(int?)));
	}

	[Test]
	public void IsTypeValidForParsing_Bool_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(bool)));
	}

	[Test]
	public void IsTypeValidForParsing_NullableBool_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(bool?)));
	}

	[Test]
	public void IsTypeValidForParsing_Decimal_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(decimal)));
	}

	[Test]
	public void IsTypeValidForParsing_NullableDecimal_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(decimal?)));
	}

	[Test]
	public void IsTypeValidForParsing_Long_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(long)));
	}

	[Test]
	public void IsTypeValidForParsing_NullableLong_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(long?)));
	}

	[Test]
	public void IsTypeValidForParsing_DateTime_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(DateTime)));
	}

	[Test]
	public void IsTypeValidForParsing_NullableDateTime_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(DateTime?)));
	}

	[Test]
	public void IsTypeValidForParsing_Enum_True()
	{
		Assert.IsTrue(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(TestEnum)));
	}

	[Test]
	public void IsTypeValidForParsing_Array_False()
	{
		Assert.IsFalse(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(Array)));
	}

	#endregion Validation

	#region Parsing

	[Test]
	public void ParseUndefined_String_Parsed()
	{
		Assert.AreEqual("test", StringToSpecifiedObjectParser.ParseUndefined("test", typeof(string)));
	}

	[Test]
	public void ParseUndefined_Bool_Parsed()
	{
		Assert.AreEqual(true, StringToSpecifiedObjectParser.ParseUndefined("true", typeof(bool)));
	}

	[Test]
	public void ParseUndefined_BoolOnValue_Parsed()
	{
		Assert.AreEqual(true, StringToSpecifiedObjectParser.ParseUndefined("on", typeof(bool)));
	}

	[Test]
	public void ParseUndefined_BoolNull_DefaultBool()
	{
		Assert.AreEqual(default(bool), StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(bool)));
	}

	[Test]
	public void ParseUndefined_BoolBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(bool)));
	}

	[Test]
	public void ParseUndefined_NullableBool_Parsed()
	{
		Assert.AreEqual((bool?)true, StringToSpecifiedObjectParser.ParseUndefined("true", typeof(bool?)));
	}

	[Test]
	public void ParseUndefined_NullableBoolOnValue_Parsed()
	{
		Assert.AreEqual((bool?)true, StringToSpecifiedObjectParser.ParseUndefined("on", typeof(bool?)));
	}

	[Test]
	public void ParseUndefined_NullableBoolNull_Null()
	{
		Assert.IsNull(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(bool?)));
	}

	[Test]
	public void ParseUndefined_NullableBoolBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(bool?)));
	}

	[Test]
	public void ParseUndefined_Int_Parsed()
	{
		Assert.AreEqual(15, StringToSpecifiedObjectParser.ParseUndefined("15", typeof(int)));
	}

	[Test]
	public void ParseUndefined_IntNull_DefaultInt()
	{
		Assert.AreEqual(default(int), StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(int)));
	}

	[Test]
	public void ParseUndefined_IntBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(int)));
	}

	[Test]
	public void ParseUndefined_NullableInt_Parsed()
	{
		Assert.AreEqual((int?)15, StringToSpecifiedObjectParser.ParseUndefined("15", typeof(int?)));
	}

	[Test]
	public void ParseUndefined_NullableIntNull_Null()
	{
		Assert.IsNull(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(int?)));
	}

	[Test]
	public void ParseUndefined_NullableIntBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(int?)));
	}

	[Test]
	public void ParseUndefined_Decimal_Parsed()
	{
		Assert.AreEqual((decimal)15, StringToSpecifiedObjectParser.ParseUndefined("15", typeof(decimal)));
	}

	[Test]
	public void ParseUndefined_DecimalNull_DefaultDecimal()
	{
		Assert.AreEqual(default(decimal), StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(decimal)));
	}

	[Test]
	public void ParseUndefined_DecimalBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(decimal)));
	}

	[Test]
	public void ParseUndefined_NullableDecimal_Parsed()
	{
		Assert.AreEqual((decimal?)15, StringToSpecifiedObjectParser.ParseUndefined("15", typeof(decimal?)));
	}

	[Test]
	public void ParseUndefined_NullableDecimalNull_Null()
	{
		Assert.IsNull(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(decimal?)));
	}

	[Test]
	public void ParseUndefined_NullableDecimalBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(decimal?)));
	}

	[Test]
	public void ParseUndefined_Long_Parsed()
	{
		Assert.AreEqual((long)15, StringToSpecifiedObjectParser.ParseUndefined("15", typeof(long)));
	}

	[Test]
	public void ParseUndefined_LongNull_DefaultLong()
	{
		Assert.AreEqual(default(long), StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(long)));
	}

	[Test]
	public void ParseUndefined_LongBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(long)));
	}

	[Test]
	public void ParseUndefined_NullableLong_Parsed()
	{
		Assert.AreEqual((long?)15, StringToSpecifiedObjectParser.ParseUndefined("15", typeof(long?)));
	}

	[Test]
	public void ParseUndefined_NullableLongNull_Null()
	{
		Assert.IsNull(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(long?)));
	}

	[Test]
	public void ParseUndefined_NullableLongBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(long?)));
	}

	[Test]
	public void ParseUndefined_DateTime_Parsed()
	{
		Assert.AreEqual(new DateTime(2014, 03, 15),
			StringToSpecifiedObjectParser.ParseUndefined("2014-03-15T00:00:00.0000000", typeof(DateTime)));
	}

	[Test]
	public void ParseUndefined_DateTimeWithFormat_Parsed()
	{
		Assert.AreEqual(new DateTime(2014, 03, 15),
			StringToSpecifiedObjectParser.ParseUndefined("15.03.2014", typeof(DateTime), "dd.MM.yyyy"));
	}

	[Test]
	public void ParseUndefined_DateTimeNull_DefaultDateTime()
	{
		Assert.AreEqual(default(DateTime), StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime)));
	}

	[Test]
	public void ParseUndefined_DateTimeWithFormatNull_DefaultDateTime()
	{
		Assert.AreEqual(default(DateTime), StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime), "dd.MM.yyyy"));
	}

	[Test]
	public void ParseUndefined_DateTimeBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime)));
	}

	[Test]
	public void ParseUndefined_DateTimeWithFormatBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime), "dd.MM.yyyy"));
	}

	[Test]
	public void ParseUndefined_NullableDateTime_Parsed()
	{
		Assert.AreEqual((DateTime?)new DateTime(2014, 03, 15),
			StringToSpecifiedObjectParser.ParseUndefined("2014-03-15T00:00:00.0000000", typeof(DateTime?)));
	}

	[Test]
	public void ParseUndefined_NullableDateTimeWithFormat_Parsed()
	{
		Assert.AreEqual((DateTime?)new DateTime(2014, 03, 15),
			StringToSpecifiedObjectParser.ParseUndefined("15.03.2014", typeof(DateTime?), "dd.MM.yyyy"));
	}

	[Test]
	public void ParseUndefined_NullableDateTimeNull_DefaultDateTime()
	{
		Assert.IsNull(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime?)));
	}

	[Test]
	public void ParseUndefined_NullableDateTimeWithFormatNull_DefaultDateTime()
	{
		Assert.IsNull(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime?), "dd.MM.yyyy"));
	}

	[Test]
	public void ParseUndefined_NullableDateTimeBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime?)));
	}

	[Test]
	public void ParseUndefined_NullableDateTimeWithFormatBadValue_ExceptionThrown()
	{
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime?), "dd.MM.yyyy"));
	}

	[Test]
	public void ParseUndefined_Enum_Parsed()
	{
		Assert.AreEqual(TestEnum.Value1, StringToSpecifiedObjectParser.ParseUndefined("1", typeof(TestEnum)));
	}

	[Test]
	public void ParseUndefined_UndefinedType_ExceptionThrown()
	{
		Assert.Throws<ModelNotSupportedException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(Array)));
	}

	#endregion Parsing
}