#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Modules.Data.Html;

public sealed class ListsGenerator(IStringTable stringTable) : IListsGenerator
{
	public string GenerateNumbersList(int length, int? selectedNumber = 0, int startNumber = 0, bool displayNotSelectedMessage = false)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedNumber == null) : "";

		for (var i = startNumber; i < startNumber + length; i++)
			data += string.Format("<option value='{0}'{1}>{0}</option>", i, i == selectedNumber ? " selected='selected'" : "");

		return data;
	}

	public string GenerateHoursList(int selectedHour = -1, bool displayNotSelectedMessage = false)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedHour == -1) : "";

		for (var i = 0; i < 24; i++)
			data += string.Format("<option value='{0}'{2}>{1:00}</option>", i, i, i == selectedHour ? " selected='selected'" : "");

		return data;
	}

	public string GenerateMinutesList(int selectedMinute = -1, bool displayNotSelectedMessage = false)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMinute == -1) : "";

		for (var i = 0; i < 60; i++)
			data += string.Format("<option value='{0}'{2}>{1:00}</option>", i, i, i == selectedMinute ? " selected='selected'" : "");

		return data;
	}

	public string GenerateDaysList(int selectedDay = -1, bool displayNotSelectedMessage = true)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedDay == -1) : "";

		for (var i = 1; i < 32; i++)
			data += string.Format("<option value='{0}' {2}>{1:00}</option>", i, i, i == selectedDay ? "selected='selected'" : "");

		return data;
	}

	public string GenerateMonthsList(int selectedMonth = -1, bool displayNotSelectedMessage = true)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMonth == -1) : "";

		var month = Convert.ToDateTime("1/1/2010");

		for (var i = 0; i < 12; i++)
			data += string.Format("<option value='{0}' {2}>{1:MMMM}</option>", i, month.AddMonths(i), i == selectedMonth ? "selected='selected'" : "");

		return data;
	}

	public string GenerateMonthsListFrom1(int selectedMonth = -1, bool displayNotSelectedMessage = true)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMonth == -1) : "";

		var month = Convert.ToDateTime("1/1/2010");

		for (var i = 0; i < 12; i++)
			data += $"<option value='{i + 1}' {(i + 1 == selectedMonth ? "selected='selected'" : "")}>{month.AddMonths(i):MMMM}</option>";

		return data;
	}

	public string GenerateYearsListToPast(int numberOfYears, int selectedYear = -1, bool displayNotSelectedMessage = true, int? currentYear = null)
	{
		var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedYear == -1) : "";
		var year = currentYear ?? DateTime.Now.Year;

		for (var i = year; i >= year - numberOfYears; i--)
			data += string.Format("<option value='{0}' {2}>{1}</option>", i, i, i == selectedYear ? "selected='selected'" : "");

		return data;
	}

	public string GenerateYearsListToFuture(int numberOfYears, int? currentYear = null)
	{
		var data = "";
		var year = currentYear ?? DateTime.Now.Year;

		for (var i = year; i <= year + numberOfYears; i++)
			data += $"<option value='{i}'>{i}</option>";

		return data;
	}

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

	public string GenerateEmptyListItem() => "<option value=''>&nbsp;</option>";

	public string GenerateDefaultListItem(bool isSelected = true) =>
		string.Format("<option value=''{1}>{0}</option>", stringTable.GetItem("HtmlListDefaultItemLabel"), isSelected ? " selected='selected'" : "");
}