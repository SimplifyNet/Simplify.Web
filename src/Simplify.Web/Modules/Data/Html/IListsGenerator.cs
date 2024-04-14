#nullable disable

using System;
using System.Collections.Generic;

namespace Simplify.Web.Modules.Data.Html;

/// <summary>
/// Represents the HTML select control lists generator.
/// </summary>
public interface IListsGenerator
{
	/// <summary>
	/// Generates a number selected HTML list.
	/// </summary>
	/// <param name="length">Length of a list.</param>
	/// <param name="selectedNumber">Selected list number.</param>
	/// <param name="startNumber">Start number of a list.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	string GenerateNumbersList(int length,
		int? selectedNumber = 0,
		int startNumber = 0,
		bool displayNotSelectedMessage = false);

	/// <summary>
	/// Generates an hour selector HTML list in 24 hours format (from 0 to 23).
	/// </summary>
	/// <param name="selectedHour">Selected hour.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	string GenerateHoursList(int selectedHour = -1, bool displayNotSelectedMessage = false);

	/// <summary>
	/// Generates a minute selector HTML list (from 0 to 59).
	/// </summary>
	/// <param name="selectedMinute">Selected minute.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	string GenerateMinutesList(int selectedMinute = -1, bool displayNotSelectedMessage = false);

	/// <summary>
	/// Generates a day selector HTML list (from 1 to 31).
	/// </summary>
	/// <param name="selectedDay">Selected day.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	string GenerateDaysList(int selectedDay = -1, bool displayNotSelectedMessage = true);

	/// <summary>
	/// Generates a month selector HTML list (from 0 to 11).
	/// </summary>
	/// <param name="selectedMonth">Selected month.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	string GenerateMonthsList(int selectedMonth = -1, bool displayNotSelectedMessage = true);

	/// <summary>
	/// Generates a month selector HTML list (from 1 to 12).
	/// </summary>
	/// <param name="selectedMonth">Selected month.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	string GenerateMonthsListFrom1(int selectedMonth = -1, bool displayNotSelectedMessage = true);

	/// <summary>
	/// Generates a year selector HTML list (from current year to -<paramref name="numberOfYears" />).
	/// </summary>
	/// <param name="numberOfYears">Number of years in list.</param>
	/// <param name="selectedYear">Selected year.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <param name="currentYear">The current year.</param>
	string GenerateYearsListToPast(int numberOfYears,
		int selectedYear = -1,
		bool displayNotSelectedMessage = true,
		int? currentYear = null);

	/// <summary>
	/// Generates a year selector HTML list (from current year to +<paramref name="numberOfYears" />).
	/// </summary>
	/// <param name="numberOfYears">Number of years in list.</param>
	/// <param name="currentYear">The current year.</param>
	string GenerateYearsListToFuture(int numberOfYears, int? currentYear = null);

	/// <summary>
	/// A generic list generator.
	/// </summary>
	/// <typeparam name="T">Item type to generate list from.</typeparam>
	/// <param name="items">List of items.</param>
	/// <param name="id">Item ID field.</param>
	/// <param name="name">Item name field.</param>
	/// <param name="selectedItem">Selected item.</param>
	/// <param name="generateEmptyListItem">Generate empty list item.</param>
	string GenerateList<T>(IList<T> items,
		Func<T, string> id, Func<T, string> name,
		T selectedItem = null,
		bool generateEmptyListItem = false)
		where T : class;

	/// <summary>
	/// Generates an HTML list from enum items.
	/// </summary>
	/// <typeparam name="T">Enum type.</typeparam>
	/// <param name="selectedItem">Selected enum item.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	string GenerateListFromEnum<T>(T selectedItem, bool displayNotSelectedMessage = true) where T : struct;

	/// <summary>
	/// Generates an HTML list from enum items.
	/// </summary>
	/// <typeparam name="T">Enum typ.e</typeparam>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <param name="selectNotSelectedMessage">Is not selected message should be selected.</param>
	string GenerateListFromEnum<T>(bool displayNotSelectedMessage = true, bool selectNotSelectedMessage = true)
		where T : struct;

	/// <summary>
	/// Generates an empty HTML list item.
	/// </summary>
	string GenerateEmptyListItem();

	/// <summary>
	/// Generates an HTML list default item.
	/// </summary>
	string GenerateDefaultListItem(bool isSelected = true);
}