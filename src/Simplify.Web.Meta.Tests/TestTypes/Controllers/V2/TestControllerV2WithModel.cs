using System;
using Simplify.Web.Meta.Tests.TestTypes.Models;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V2;

public class TestControllerV2WithModel : Controller2<TestModel>
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}