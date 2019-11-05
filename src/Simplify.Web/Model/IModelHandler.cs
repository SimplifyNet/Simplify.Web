using System.Threading.Tasks;
using Simplify.DI;

namespace Simplify.Web.Model
{
	/// <summary>
	/// Represent model handler
	/// </summary>
	public interface IModelHandler
	{
		/// <summary>
		/// Parses model and validates it asynchronously
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="resolver">The resolver.</param>
		/// <returns></returns>
		Task<T> ProcessAsync<T>(IDIResolver resolver);
	}
}