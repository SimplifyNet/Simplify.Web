using System;

namespace Simplify.Web.Tests.TestEntities;

public class TestControllerV2WithModel : Controller2<TestModel>
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}