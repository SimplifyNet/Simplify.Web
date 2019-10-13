using Simplify.DI;

namespace Simplify.Web.Model
{
	/// <summary>
	/// Represent model handler
	/// </summary>
	public interface IModelHandler
	{
		/// <summary>
		/// Parses model and validates it
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="resolver">The resolver.</param>
		/// <returns></returns>
		T Process<T>(IDIResolver resolver);
	}
}