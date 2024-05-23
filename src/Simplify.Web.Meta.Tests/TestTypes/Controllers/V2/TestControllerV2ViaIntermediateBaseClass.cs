using System;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V2;

public class TestControllerV2ViaIntermediateBaseClass : IntermediateControllerV2Base
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}