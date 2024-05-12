using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Tests.Modules.Data.TestTypes;

namespace Simplify.Web.Tests.Modules.Data.Html;

public class ListsGeneratorTests
{
	[Test]
	public void ListsGenerator_NormalData_ListsGeneratedCorrectly()
	{
		Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
		Thread.CurrentThread.CurrentCulture = new CultureInfo("en");

		var st = new Mock<IStringTable>();

		st.Setup(x => x.GetItem(It.Is<string>(c => c == "HtmlListDefaultItemLabel"))).Returns("Default label");
		st.Setup(x => x.GetAssociatedValue(It.Is<FooEnum>(c => c == FooEnum.FooItem1))).Returns("Foo item text");

		var lg = new ListsGenerator(st.Object);

		Assert.That(lg.GenerateDefaultListItem(false), Is.EqualTo("<option value=''>Default label</option>"));
		Assert.That(lg.GenerateDefaultListItem(), Is.EqualTo("<option value='' selected='selected'>Default label</option>"));
		Assert.That(lg.GenerateNumbersList(2), Is.EqualTo("<option value='0' selected='selected'>0</option><option value='1'>1</option>"));

		Assert.That(lg.GenerateHoursList(), Is.EqualTo("<option value='0'>00</option><option value='1'>01</option><option value='2'>02</option><option value='3'>03</option><option value='4'>04</option><option value='5'>05</option><option value='6'>06</option><option value='7'>07</option><option value='8'>08</option><option value='9'>09</option><option value='10'>10</option><option value='11'>11</option><option value='12'>12</option><option value='13'>13</option><option value='14'>14</option><option value='15'>15</option><option value='16'>16</option><option value='17'>17</option><option value='18'>18</option><option value='19'>19</option><option value='20'>20</option><option value='21'>21</option><option value='22'>22</option><option value='23'>23</option>"));
		Assert.That(lg.GenerateMinutesList(), Is.EqualTo("<option value='0'>00</option><option value='1'>01</option><option value='2'>02</option><option value='3'>03</option><option value='4'>04</option><option value='5'>05</option><option value='6'>06</option><option value='7'>07</option><option value='8'>08</option><option value='9'>09</option><option value='10'>10</option><option value='11'>11</option><option value='12'>12</option><option value='13'>13</option><option value='14'>14</option><option value='15'>15</option><option value='16'>16</option><option value='17'>17</option><option value='18'>18</option><option value='19'>19</option><option value='20'>20</option><option value='21'>21</option><option value='22'>22</option><option value='23'>23</option><option value='24'>24</option><option value='25'>25</option><option value='26'>26</option><option value='27'>27</option><option value='28'>28</option><option value='29'>29</option><option value='30'>30</option><option value='31'>31</option><option value='32'>32</option><option value='33'>33</option><option value='34'>34</option><option value='35'>35</option><option value='36'>36</option><option value='37'>37</option><option value='38'>38</option><option value='39'>39</option><option value='40'>40</option><option value='41'>41</option><option value='42'>42</option><option value='43'>43</option><option value='44'>44</option><option value='45'>45</option><option value='46'>46</option><option value='47'>47</option><option value='48'>48</option><option value='49'>49</option><option value='50'>50</option><option value='51'>51</option><option value='52'>52</option><option value='53'>53</option><option value='54'>54</option><option value='55'>55</option><option value='56'>56</option><option value='57'>57</option><option value='58'>58</option><option value='59'>59</option>"));
		Assert.That(lg.GenerateDaysList(), Is.EqualTo("<option value='' selected='selected'>Default label</option><option value='1' >01</option><option value='2' >02</option><option value='3' >03</option><option value='4' >04</option><option value='5' >05</option><option value='6' >06</option><option value='7' >07</option><option value='8' >08</option><option value='9' >09</option><option value='10' >10</option><option value='11' >11</option><option value='12' >12</option><option value='13' >13</option><option value='14' >14</option><option value='15' >15</option><option value='16' >16</option><option value='17' >17</option><option value='18' >18</option><option value='19' >19</option><option value='20' >20</option><option value='21' >21</option><option value='22' >22</option><option value='23' >23</option><option value='24' >24</option><option value='25' >25</option><option value='26' >26</option><option value='27' >27</option><option value='28' >28</option><option value='29' >29</option><option value='30' >30</option><option value='31' >31</option>"));
		Assert.That(lg.GenerateMonthsList(), Is.EqualTo("<option value='' selected='selected'>Default label</option><option value='0' >January</option><option value='1' >February</option><option value='2' >March</option><option value='3' >April</option><option value='4' >May</option><option value='5' >June</option><option value='6' >July</option><option value='7' >August</option><option value='8' >September</option><option value='9' >October</option><option value='10' >November</option><option value='11' >December</option>"));
		Assert.That(lg.GenerateMonthsListFrom1(), Is.EqualTo("<option value='' selected='selected'>Default label</option><option value='1' >January</option><option value='2' >February</option><option value='3' >March</option><option value='4' >April</option><option value='5' >May</option><option value='6' >June</option><option value='7' >July</option><option value='8' >August</option><option value='9' >September</option><option value='10' >October</option><option value='11' >November</option><option value='12' >December</option>"));
		Assert.That(lg.GenerateYearsListToPast(2, -1, true, 2013), Is.EqualTo("<option value='' selected='selected'>Default label</option><option value='2013' >2013</option><option value='2012' >2012</option><option value='2011' >2011</option>"));
		Assert.That(lg.GenerateYearsListToFuture(2, 2013), Is.EqualTo("<option value='2013'>2013</option><option value='2014'>2014</option><option value='2015'>2015</option>"));

		Assert.That(lg.GenerateListFromEnum(FooEnum.FooItem1), Is.EqualTo("<option value=''>Default label</option><option value='0' selected='selected'>Foo item text</option><option value='1'></option>"));
		Assert.That(lg.GenerateListFromEnum(FooEnum.FooItem1, false), Is.EqualTo("<option value='0' selected='selected'>Foo item text</option><option value='1'></option>"));
		Assert.That(lg.GenerateListFromEnum<FooEnum>(true), Is.EqualTo("<option value='' selected='selected'>Default label</option><option value='0'>Foo item text</option><option value='1'></option>"));
		Assert.That(lg.GenerateListFromEnum<FooEnum>(true, false), Is.EqualTo("<option value=''>Default label</option><option value='0'>Foo item text</option><option value='1'></option>"));
		Assert.That(lg.GenerateListFromEnum<FooEnum>(false), Is.EqualTo("<option value='0'>Foo item text</option><option value='1'></option>"));

		Assert.That(lg.GenerateEmptyListItem(), Is.EqualTo("<option value=''>&nbsp;</option>"));

		var items = new List<FooTestList?>
		{
			new(1, "Item 1"),
			new(2, "Item 2")
		};

		Assert.That(lg.GenerateList(items, x => x?.Id.ToString(), x => x?.Value), Is.EqualTo("<option value='1' >Item 1</option><option value='2' >Item 2</option>"));
	}

	public class FooTestList(int id, string value)
	{
		public readonly int Id = id;
		public readonly string Value = value;
	}
}