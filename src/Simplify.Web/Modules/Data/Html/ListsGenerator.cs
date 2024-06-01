#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Modules.Data.Html;

/// <summary>
/// Provides the lists generator
/// </summary>
/// <seealso cref="IListsGenerator" />
public sealed class ListsGenerator(IStringTable stringTable) : IListsGenerator
{
	/// <summary>
	/// Generates a number selected HTML list.
	/// </summary>
	/// <param name="length">Length of a list.</param>
	/// <param name="selectedNumber">Selected list number.</param>
	/// <param name="startNumber">Start number of a list.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	public string GenerateNumbersList(int length, int? selectedNumber = 0, int startNumber = 0, bool displayNotSelectedMessage = false)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedNumber == null) : "";

		for (var i = startNumber; i < startNumber + length; i++)
			data += string.Format("<option value='{0}'{1}>{0}</option>", i, i == selectedNumber ? " selected='selected'" : "");

		return data;
	}

	/// <summary>
	/// Generates an hour selector HTML list in 24 hours format (from 0 to 23).
	/// </summary>
	/// <param name="selectedHour">Selected hour.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	public string GenerateHoursList(int selectedHour = -1, bool displayNotSelectedMessage = false)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedHour == -1) : "";

		for (var i = 0; i < 24; i++)
			data += string.Format("<option value='{0}'{2}>{1:00}</option>", i, i, i == selectedHour ? " selected='selected'" : "");

		return data;
	}

	/// <summary>
	/// Generates a minute selector HTML list (from 0 to 59).
	/// </summary>
	/// <param name="selectedMinute">Selected minute.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	public string GenerateMinutesList(int selectedMinute = -1, bool displayNotSelectedMessage = false)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMinute == -1) : "";

		for (var i = 0; i < 60; i++)
			data += string.Format("<option value='{0}'{2}>{1:00}</option>", i, i, i == selectedMinute ? " selected='selected'" : "");

		return data;
	}

	/// <summary>
	/// Generates a day selector HTML list (from 1 to 31).
	/// </summary>
	/// <param name="selectedDay">Selected day.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	public string GenerateDaysList(int selectedDay = -1, bool displayNotSelectedMessage = true)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedDay == -1) : "";

		for (var i = 1; i < 32; i++)
			data += string.Format("<option value='{0}' {2}>{1:00}</option>", i, i, i == selectedDay ? "selected='selected'" : "");

		return data;
	}

	/// <summary>
	/// Generates a month selector HTML list (from 0 to 11).
	/// </summary>
	/// <param name="selectedMonth">Selected month.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	public string GenerateMonthsList(int selectedMonth = -1, bool displayNotSelectedMessage = true)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMonth == -1) : "";

		var month = Convert.ToDateTime("1/1/2010");

		for (var i = 0; i < 12; i++)
			data += string.Format("<option value='{0}' {2}>{1:MMMM}</option>", i, month.AddMonths(i), i == selectedMonth ? "selected='selected'" : "");

		return data;
	}

	/// <summary>
	/// Generates a month selector HTML list (from 1 to 12).
	/// </summary>
	/// <param name="selectedMonth">Selected month.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	public string GenerateMonthsListFrom1(int selectedMonth = -1, bool displayNotSelectedMessage = true)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMonth == -1) : "";

		var month = Convert.ToDateTime("1/1/2010");

		for (var i = 0; i < 12; i++)
			data += $"<option value='{i + 1}' {(i + 1 == selectedMonth ? "selected='selected'" : "")}>{month.AddMonths(i):MMMM}</option>";

		return data;
	}

	/// <summary>
	/// Generates a year selector HTML list (from current year to -<paramref name="numberOfYears" />).
	/// </summary>
	/// <param name="numberOfYears">Number of years in list.</param>
	/// <param name="selectedYear">Selected year.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <param name="currentYear">The current year.</param>
	public string GenerateYearsListToPast(int numberOfYears, int selectedYear = -1, bool displayNotSelectedMessage = true, int? currentYear = null)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedYear == -1) : "";
		var year = currentYear ?? DateTime.Now.Year;

		for (var i = year; i >= year - numberOfYears; i--)
			data += string.Format("<option value='{0}' {2}>{1}</option>", i, i, i == selectedYear ? "selected='selected'" : "");

		return data;
	}

	/// <summary>
	/// Generates a year selector HTML list (from current year to +<paramref name="numberOfYears" />).
	/// </summary>
	/// <param name="numberOfYears">Number of years in list.</param>
	/// <param name="currentYear">The current year.</param>
	public string GenerateYearsListToFuture(int numberOfYears, int? currentYear = null)
	{
		var data = "";
		var year = currentYear ?? DateTime.Now.Year;

		for (var i = year; i <= year + numberOfYears; i++)
			data += $"<option value='{i}'>{i}</option>";

		return data;
	}

	/// <summary>
	/// A generic list generator.
	/// </summary>
	/// <typeparam name="T">Item type to generate list from.</typeparam>
	/// <param name="items">List of items.</param>
	/// <param name="id">Item ID field.</param>
	/// <param name="name">Item name field.</param>
	/// <param name="selectedItem">Selected item.</param>
	/// <param name="generateEmptyListItem">Generate empty list item.</param>
	public string GenerateList<T>(IList<T> items, Func<T, string> id, Func<T, string> name, T selectedItem = null, bool generateEmptyListItem = false)
		where T : class
	{
		var data = generateEmptyListItem ? GenerateEmptyListItem() : "";

		return items.Aggregate(data,
			(current, item) =>
				current +
				string.Format("<option value='{0}' {2}>{1}</option>", id(item), name(item),
					(selectedItem == item ? "selected='selected'" : "")));
	}

	/// <summary>
	/// Generates an HTML list from enum items.
	/// </summary>
	/// <typeparam name="T">Enum type.</typeparam>
	/// <param name="selectedItem">Selected enum item.</param>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	public string GenerateListFromEnum<T>(T selectedItem = default(T), bool displayNotSelectedMessage = true)
		where T : struct
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(false) : "";

		return Enum.GetValues(typeof(T))
			.Cast<T>()
			.Aggregate(data,
				(current, item) =>
					current +
					string.Format("<option value='{0}'{2}>{1}</option>", Convert.ToInt32(item), stringTable.GetAssociatedValue(item),
						selectedItem.ToString() == item.ToString() ? " selected='selected'" : ""));
	}

	/// <summary>
	/// Generates an HTML list from enum items.
	/// </summary>
	/// <typeparam name="T">Enum typ.e</typeparam>
	/// <param name="displayNotSelectedMessage">Display not selected message in list or not.</param>
	/// <param name="selectNotSelectedMessage">Is not selected message should be selected.</param>
	public string GenerateListFromEnum<T>(bool displayNotSelectedMessage = true, bool selectNotSelectedMessage = true)
		where T : struct
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectNotSelectedMessage) : "";

		return Enum.GetValues(typeof(T))
			.Cast<T>()
			.Aggregate(data,
				(current, item) =>
					current +
					$"<option value='{Convert.ToInt32(item)}'>{stringTable.GetAssociatedValue(item)}</option>");
	}

	/// <summary>
	/// Generates an empty HTML list item.
	/// </summary>
	public string GenerateEmptyListItem() => "<option value=''>&nbsp;</option>";

	/// <summary>
	/// Generates an HTML list default item.
	/// </summary>
	/// <param name="isSelected">if set to <c>true</c> then the item will be generated as selected.</param>
	public string GenerateDefaultListItem(bool isSelected = true) =>
		string.Format("<option value=''{1}>{0}</option>", stringTable.GetItem("HtmlListDefaultItemLabel"), isSelected ? " selected='selected'" : "");
}