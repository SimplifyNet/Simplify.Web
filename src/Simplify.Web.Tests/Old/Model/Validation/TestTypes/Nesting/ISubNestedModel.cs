﻿using Simplify.Web.Old.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Validation.TestTypes.Nesting;

public interface ISubNestedModel
{
	[Required]
	string? BuiltInType { get; set; }
}