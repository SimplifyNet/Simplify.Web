using System.Threading.Tasks;

namespace Simplify.Web.Core2.Controllers;

public interface IControllersRequestHandler
{
	Task HandleAsync(IControllersProcessingContext context);
}