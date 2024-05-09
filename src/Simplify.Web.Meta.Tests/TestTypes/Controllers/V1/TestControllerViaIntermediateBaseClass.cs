using System;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;

public class TestControllerViaIntermediateBaseClass : IntermediateControllerBase
{
	public override ControllerResponse Invoke() => throw new NotImplementedException();
}