﻿using System;
using Simplify.Web.Attributes;

namespace Simplify.Web.Tests.Controllers.V2.Metadata.MetadataFactoryTests.TestTypes;

[Get("/{Stringparam}/{intParam}/{BOOLPARAM}/{StringArrayParam}/{intArrayParam}/{decimalArrayParam}/{boolArrayParam}/{decimalParam}")]
public class AllParamsControllerV2 : Controller2
{
	public ControllerResponse Invoke(string stringParam,
		int intParam,
		decimal decimalParam,
		bool boolParam,
		string[] stringArrayParam,
		int[] intArrayParam,
		decimal[] decimalArrayParam,
		bool[] boolArrayParam) => throw new NotImplementedException();
}