using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.ProcessingContext;

namespace Simplify.Web.Core2.Controllers;

public interface IControllersRequestHandler
{
	Task HandleAsync(IControllersProcessingContext context);
}