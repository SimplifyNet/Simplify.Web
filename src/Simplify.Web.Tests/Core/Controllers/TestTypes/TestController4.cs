﻿using System;

namespace Simplify.Web.Tests.Core.Controllers.TestTypes;

public class TestController4 : Controller<TestModel>
{
	public override ControllerResponse Invoke() => throw new NotImplementedException();
}