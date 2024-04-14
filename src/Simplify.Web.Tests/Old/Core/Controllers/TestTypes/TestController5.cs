using System;
using System.Threading.Tasks;
using Simplify.Web.Old;

namespace Simplify.Web.Tests.Old.Core.Controllers.TestTypes;

public class TestController5 : AsyncController<TestModel>
{
	public override Task<ControllerResponse?> Invoke() => throw new NotImplementedException();
}