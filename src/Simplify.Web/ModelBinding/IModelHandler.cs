namespace Simplify.Web.ModelBinding;

/// <summary>
/// Represent model handler
/// </summary>
public interface IModelHandler
{
	/// <summary>
	/// Parses model and validates it
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	/// <returns></returns>
	T Process<T>();
}