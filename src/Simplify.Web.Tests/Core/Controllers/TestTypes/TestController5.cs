﻿using System;
using System.Threading.Tasks;

namespace Simplify.Web.Tests.Core.Controllers.TestTypes;

public class TestController5 : AsyncController<TestModel>
{
	public override Task<ControllerResponse?> Invoke() => throw new NotImplementedException();
}