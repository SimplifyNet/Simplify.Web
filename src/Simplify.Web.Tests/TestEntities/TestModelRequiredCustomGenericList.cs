using System.Collections.Generic;
using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.Tests.TestEntities;

public class TestModelRequiredCustomGenericList
{
	[Required]
	public IList<TestModelEMail> Prop1 { get; set; }
}