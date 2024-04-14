using System.Collections.Generic;

namespace Simplify.Web.Modules.Data;

/// <summary>
/// Represents a string table.
/// </summary>
public interface IStringTable
{
	/// <summary>
	/// Gets StringTable items.
	/// </summary>
	IDictionary<string, object?> Items { get; }

	/// <summary>
	/// Setups this string table.
	/// </summary>
	void Setup();

	/// <summary>
	/// Gets an enum associated value from string table by enum type + enum element name.
	/// </summary>
	/// <typeparam name="T">Enum.</typeparam>
	/// <param name="enumValue">Enum value.</param>
	/// <returns>Associated value.</returns>
	string? GetAssociatedValue<T>(T enumValue) where T : struct;

	/// <summary>
	/// Gets an item from string table.
	/// </summary>
	/// <param name="itemName">Name of the item.</param>
	string? GetItem(string itemName);
}