using System;
using System.Threading.Tasks;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;

public class TestAsyncController : AsyncController
{
	public override Task<ControllerResponse?> Invoke()
	{
		throw new NotImplementedException();
	}
}