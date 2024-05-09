using System;
using Simplify.Web.Meta.Tests.TestTypes.Models;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;

public class TestControllerWithModel : Controller<TestModel>
{
	public override ControllerResponse Invoke() => throw new NotImplementedException();
}