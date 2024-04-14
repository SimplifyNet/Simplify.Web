using System;
using Simplify.Web.Old;

namespace Simplify.Web.Tests.Old.Core.Controllers.TestTypes;

public class TestController4 : Controller<TestModel>
{
	public override Web.Old.ControllerResponse Invoke() => throw new NotImplementedException();
}