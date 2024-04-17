using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.Execution;

public interface IControllerExecutorResolver
{
	IControllerExecutor Resolve(IControllerMetadata controller);
}