using System;
using System.Threading.Tasks;

namespace Simplify.Web.Tests.Old.Core.Controllers.TestTypes;

public class TestController5 : Web.Old.AsyncController<TestModel>
{
	public override Task<Web.Old.ControllerResponse?> Invoke() => throw new NotImplementedException();
}