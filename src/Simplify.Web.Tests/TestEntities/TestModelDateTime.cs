﻿using System;
using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.Tests.TestEntities;

public class TestModelDateTime
{
	[Format("dd--yyyy--MM")]
	public DateTime? Prop1 { get; set; }

	public DateTime Prop2 { get; set; }
}