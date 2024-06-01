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
	public void IsTypeValidForParsing_String_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(string)), Is.True);

	[Test]
	public void IsTypeValidForParsing_Int_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(int)), Is.True);

	[Test]
	public void IsTypeValidForParsing_NullableInt_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(int?)), Is.True);

	[Test]
	public void IsTypeValidForParsing_Bool_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(bool)), Is.True);

	[Test]
	public void IsTypeValidForParsing_NullableBool_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(bool?)), Is.True);

	[Test]
	public void IsTypeValidForParsing_Decimal_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(decimal)), Is.True);

	[Test]
	public void IsTypeValidForParsing_NullableDecimal_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(decimal?)), Is.True);

	[Test]
	public void IsTypeValidForParsing_Long_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(long)), Is.True);

	[Test]
	public void IsTypeValidForParsing_NullableLong_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(long?)), Is.True);

	[Test]
	public void IsTypeValidForParsing_DateTime_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(DateTime)), Is.True);

	[Test]
	public void IsTypeValidForParsing_NullableDateTime_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(DateTime?)), Is.True);

	[Test]
	public void IsTypeValidForParsing_Enum_True() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(Test)), Is.True);

	[Test]
	public void IsTypeValidForParsing_Array_False() =>
		Assert.That(StringToSpecifiedObjectParser.IsTypeValidForParsing(typeof(Array)), Is.False);

	#endregion Validation

	#region Parsing

	[Test]
	public void ParseUndefined_String_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("test", typeof(string)), Is.EqualTo("test"));

	[Test]
	public void ParseUndefined_Bool_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("true", typeof(bool)), Is.EqualTo(true));

	[Test]
	public void ParseUndefined_BoolOnValue_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("on", typeof(bool)), Is.EqualTo(true));

	[Test]
	public void ParseUndefined_BoolNull_DefaultBool() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(bool)), Is.EqualTo(default(bool)));

	[Test]
	public void ParseUndefined_BoolBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(bool)));

	[Test]
	public void ParseUndefined_NullableBool_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("true", typeof(bool?)), Is.EqualTo((bool?)true));

	[Test]
	public void ParseUndefined_NullableBoolOnValue_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("on", typeof(bool?)), Is.EqualTo((bool?)true));

	[Test]
	public void ParseUndefined_NullableBoolNull_Null() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(bool?)), Is.Null);

	[Test]
	public void ParseUndefined_NullableBoolBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(bool?)));

	[Test]
	public void ParseUndefined_Int_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15", typeof(int)), Is.EqualTo(15));

	[Test]
	public void ParseUndefined_IntNull_DefaultInt() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(int)), Is.EqualTo(default(int)));

	[Test]
	public void ParseUndefined_IntBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(int)));

	[Test]
	public void ParseUndefined_NullableInt_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15", typeof(int?)), Is.EqualTo((int?)15));

	[Test]
	public void ParseUndefined_NullableIntNull_Null() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(int?)), Is.Null);

	[Test]
	public void ParseUndefined_NullableIntBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(int?)));

	[Test]
	public void ParseUndefined_Decimal_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15", typeof(decimal)), Is.EqualTo((decimal)15));

	[Test]
	public void ParseUndefined_DecimalNull_DefaultDecimal() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(decimal)), Is.EqualTo(default(decimal)));

	[Test]
	public void ParseUndefined_DecimalBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(decimal)));

	[Test]
	public void ParseUndefined_NullableDecimal_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15", typeof(decimal?)), Is.EqualTo((decimal?)15));

	[Test]
	public void ParseUndefined_NullableDecimalNull_Null() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(decimal?)), Is.Null);

	[Test]
	public void ParseUndefined_NullableDecimalBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(decimal?)));

	[Test]
	public void ParseUndefined_Long_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15", typeof(long)), Is.EqualTo((long)15));

	[Test]
	public void ParseUndefined_LongNull_DefaultLong() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(long)), Is.EqualTo(default(long)));

	[Test]
	public void ParseUndefined_LongBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(long)));

	[Test]
	public void ParseUndefined_NullableLong_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15", typeof(long?)), Is.EqualTo((long?)15));

	[Test]
	public void ParseUndefined_NullableLongNull_Null() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(long?)), Is.Null);

	[Test]
	public void ParseUndefined_NullableLongBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(long?)));

	[Test]
	public void ParseUndefined_DateTime_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("2014-03-15T00:00:00.0000000", typeof(DateTime)), Is.EqualTo(new DateTime(2014, 03, 15, 0, 0, 0, DateTimeKind.Utc)));

	[Test]
	public void ParseUndefined_DateTimeWithFormat_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15.03.2014", typeof(DateTime), "dd.MM.yyyy"), Is.EqualTo(new DateTime(2014, 03, 15, 0, 0, 0, DateTimeKind.Utc)));

	[Test]
	public void ParseUndefined_DateTimeNull_DefaultDateTime() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime)), Is.EqualTo(default(DateTime)));

	[Test]
	public void ParseUndefined_DateTimeWithFormatNull_DefaultDateTime() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime), "dd.MM.yyyy"), Is.EqualTo(default(DateTime)));

	[Test]
	public void ParseUndefined_DateTimeBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime)));

	[Test]
	public void ParseUndefined_DateTimeWithFormatBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime), "dd.MM.yyyy"));

	[Test]
	public void ParseUndefined_NullableDateTime_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("2014-03-15T00:00:00.0000000", typeof(DateTime?)),
			Is.EqualTo((DateTime?)new DateTime(2014, 03, 15, 0, 0, 0, DateTimeKind.Utc)));

	[Test]
	public void ParseUndefined_NullableDateTimeWithFormat_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("15.03.2014", typeof(DateTime?), "dd.MM.yyyy"),
			Is.EqualTo((DateTime?)new DateTime(2014, 03, 15, 0, 0, 0, DateTimeKind.Utc)));

	[Test]
	public void ParseUndefined_NullableDateTimeNull_DefaultDateTime() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime?)), Is.Null);

	[Test]
	public void ParseUndefined_NullableDateTimeWithFormatNull_DefaultDateTime() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined(null!, typeof(DateTime?), "dd.MM.yyyy"), Is.Null);

	[Test]
	public void ParseUndefined_NullableDateTimeBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime?)));

	[Test]
	public void ParseUndefined_NullableDateTimeWithFormatBadValue_ExceptionThrown() =>
		Assert.Throws<ModelBindingException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(DateTime?), "dd.MM.yyyy"));

	[Test]
	public void ParseUndefined_Enum_Parsed() =>
		Assert.That(StringToSpecifiedObjectParser.ParseUndefined("1", typeof(Test)), Is.EqualTo(Test.Value1));

	[Test]
	public void ParseUndefined_UndefinedType_ExceptionThrown() =>
		Assert.Throws<ModelNotSupportedException>(() => StringToSpecifiedObjectParser.ParseUndefined("test", typeof(Array)));

	#endregion Parsing
}