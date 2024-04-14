using System;
using Simplify.Web.Old.Model.Binding.Attributes;

namespace Simplify.Web.Tests.Old.Model.Binding.Parsers.ListToModelParserTestTypes;

public class TestModelDateTime
{
	[Format("dd--yyyy--MM")]
	public DateTime? Prop1 { get; set; }

	public DateTime Prop2 { get; set; }
}