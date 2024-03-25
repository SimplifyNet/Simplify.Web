using System;
using System.Threading.Tasks;

namespace Simplify.Web.Meta.Tests.TestTypes;

public class TestController5 : AsyncController<TestModel>
{
	public override Task<ControllerResponse?> Invoke() => throw new NotImplementedException();
}