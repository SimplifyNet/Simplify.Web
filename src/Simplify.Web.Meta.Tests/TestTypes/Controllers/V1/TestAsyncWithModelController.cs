using System;
using System.Threading.Tasks;
using Simplify.Web.Meta.Tests.TestTypes.Models;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;

public class TestAsyncWithModelController : AsyncController<TestModel>
{
	public override Task<ControllerResponse?> Invoke() => throw new NotImplementedException();
}