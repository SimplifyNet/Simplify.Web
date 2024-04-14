using System;
using System.Threading.Tasks;
using Simplify.Web.Old;

namespace Simplify.Web.Tests.Old.Core.Controllers.TestTypes;

public class TestController2 : AsyncController
{
	public override Task<Web.Old.ControllerResponse?> Invoke()
	{
		throw new NotImplementedException();
	}
}