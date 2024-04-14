#nullable disable

using System;
using System.Collections.Generic;

namespace Simplify.Web.Old.Modules.Data.Html;

/// <summary>
/// HTML select control lists generator.
/// Usable <see cref="StringTable"/> items:
/// "HtmlListDefaultItemLabel"
/// </summary>
public interface IListsGenerator
{
	/// <summary>
	/// Generate number selected HTML list.
	/// </summary>
	/// <param name="length">Length of a list.</param>
	/// <param name="selectedNumber">Selected list number.</param>
	/// <param name="startNumber">Start number of a list.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <returns></returns>
	string GenerateNumbersList(int length, int? selectedNumber = 0, int startNumber = 0,
		bool displayNotSelectedMessage = false);

	/// <summary>
	/// Generate hours selector HTML list in 24 hour format (from 0 to 23).
	/// </summary>
	/// <param name="selectedHour">Selected hour.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <returns>HTML list.</returns>
	string GenerateHoursList(int selectedHour = -1, bool displayNotSelectedMessage = false);

	/// <summary>
	/// Generate minutes selector HTML list (from 0 to 59).
	/// </summary>
	/// <param name="selectedMinute">Selected minute.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <returns>HTML list.</returns>
	string GenerateMinutesList(int selectedMinute = -1, bool displayNotSelectedMessage = false);

	/// <summary>
	/// Generate days selector HTML list (from 1 to 31).
	/// </summary>
	/// <param name="selectedDay">Selected day.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <returns>HTML list.</returns>
	string GenerateDaysList(int selectedDay = -1, bool displayNotSelectedMessage = true);

	/// <summary>
	/// Generate months selector HTML list (from 0 to 11).
	/// </summary>
	/// <param name="selectedMonth">Selected month.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <returns>HTML list.</returns>
	string GenerateMonthsList(int selectedMonth = -1, bool displayNotSelectedMessage = true);

	/// <summary>
	/// Generate months selector HTML list (from 1 to 12).
	/// </summary>
	/// <param name="selectedMonth">Selected month.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <returns>HTML list</returns>
	string GenerateMonthsListFrom1(int selectedMonth = -1, bool displayNotSelectedMessage = true);

	/// <summary>
	/// Generate years selector HTML list (from current year to -<paramref name="numberOfYears" />).
	/// </summary>
	/// <param name="numberOfYears">Number of years in list.</param>
	/// <param name="selectedYear">Selected year.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <param name="currentYear">The current year.</param>
	/// <returns>
	/// HTML list.
	/// </returns>
	string GenerateYearsListToPast(int numberOfYears, int selectedYear = -1, bool displayNotSelectedMessage = true,
		int? currentYear = null);

	/// <summary>
	/// Generate years selector HTML list (from current year to +<paramref name="numberOfYears" />).
	/// </summary>
	/// <param name="numberOfYears">Number of years in list.</param>
	/// <param name="currentYear">The current year.</param>
	/// <returns>
	/// HTML list.
	/// </returns>
	string GenerateYearsListToFuture(int numberOfYears, int? currentYear = null);

	/// <summary>
	/// Generic list generator.
	/// </summary>
	/// <typeparam name="T">Item type to generate list from.</typeparam>
	/// <param name="items">List of items.</param>
	/// <param name="id">Item ID field.</param>
	/// <param name="name">Item name field.</param>
	/// <param name="selectedItem">Selected item.</param>
	/// <param name="generateEmptyListItem">Generate empty list item.</param>
	/// <returns>HTML list.</returns>
	string GenerateList<T>(IList<T> items, Func<T, string> id, Func<T, string> name, T selectedItem = null, bool generateEmptyListItem = false)
		where T : class;

	/// <summary>
	/// Generate HTML list from enum items.
	/// </summary>
	/// <typeparam name="T">Enum type.</typeparam>
	/// <param name="selectedItem">Selected enum item.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <returns>HTML list.</returns>
	string GenerateListFromEnum<T>(T selectedItem, bool displayNotSelectedMessage = true) where T : struct;

	/// <summary>
	/// Generate HTML list from enum items.
	/// </summary>
	/// <typeparam name="T">Enum typ.e</typeparam>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <param name="selectNotSelectedMessage">Is not selected message should be selected.</param>
	/// <returns>HTML list.</returns>
	string GenerateListFromEnum<T>(bool displayNotSelectedMessage = true, bool selectNotSelectedMessage = true)
		where T : struct;

	/// <summary>
	/// Generating empty HTML list item.
	/// </summary>
	string GenerateEmptyListItem();

	/// <summary>
	/// Generating HTML list default item.
	/// </summary>
	string GenerateDefaultListItem(bool isSelected = true);
}