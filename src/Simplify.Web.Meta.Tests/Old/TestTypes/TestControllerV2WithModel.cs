using System;
using Simplify.Web.Old;

namespace Simplify.Web.Meta.Tests.Old.TestTypes;

public class TestControllerV2WithModel : Controller2<TestModel>
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}