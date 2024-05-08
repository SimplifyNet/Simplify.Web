using System.Data;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.PageComposition;

/// <summary>
/// Represent web-page generator.
/// </summary>
public interface IPageGenerator
{
	/// <summary>
	/// Generates the web page HTML code.
	/// </summary>
	string Generate(IDataCollector dataCollector);
}