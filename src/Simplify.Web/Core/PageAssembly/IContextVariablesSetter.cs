using Simplify.DI;

namespace Simplify.Web.Core.PageAssembly;

/// <summary>
/// Represent context variables setter.
/// </summary>
public interface IContextVariablesSetter
{
	/// <summary>
	/// Sets the context variables to data collector.
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	void SetVariables(IDIResolver resolver);
}