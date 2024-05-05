using Simplify.Web.Controllers.V1.Matcher;

namespace Simplify.Web.Controllers.V1.Matcher;

/// <summary>
/// Represent a controller v1 path parser.
/// </summary>
public interface IController1PathParser
{
	/// <summary>
	/// Parses the specified controller path.
	/// </summary>
	/// <param name="controllerPath">The controller path.</param>
	IControllerPath Parse(string controllerPath);
}