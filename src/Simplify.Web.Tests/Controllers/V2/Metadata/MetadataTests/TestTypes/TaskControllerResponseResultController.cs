using System.Threading.Tasks;

namespace Simplify.Web.Tests.Controllers.V2.Metadata.MetadataTests.TestTypes;

public class TaskControllerResponseResultController : Controller2
{
	public Task<ControllerResponse> Invoke() => null!;
}