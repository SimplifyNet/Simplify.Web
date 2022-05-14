namespace Simplify.Web.Modules.Data;

/// <summary>
/// Represent string table
/// </summary>
public interface IStringTable
{
	/// <summary>
	/// String table items
	/// </summary>
	dynamic Items { get; }

	/// <summary>
	/// Setups this string table.
	/// </summary>
	void Setup();

	/// <summary>
	/// Get enum associated value from string table by enum type + enum element name
	/// </summary>
	/// <typeparam name="T">Enum</typeparam>
	/// <param name="enumValue">Enum value</param>
	/// <returns>associated value</returns>
	string? GetAssociatedValue<T>(T enumValue) where T : struct;

	/// <summary>
	/// Gets the item from string table.
	/// </summary>
	/// <param name="itemName">Name of the item.</param>
	/// <returns></returns>
	string? GetItem(string itemName);
}