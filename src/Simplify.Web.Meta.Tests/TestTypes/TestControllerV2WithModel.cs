using System;

namespace Simplify.Web.Meta.Tests.TestTypes;

public class TestControllerV2WithModel : Controller2<TestModel>
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}