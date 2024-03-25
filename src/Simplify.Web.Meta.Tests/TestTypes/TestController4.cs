using System;

namespace Simplify.Web.Meta.Tests.TestTypes;

public class TestController4 : Controller<TestModel>
{
	public override ControllerResponse Invoke() => throw new NotImplementedException();
}