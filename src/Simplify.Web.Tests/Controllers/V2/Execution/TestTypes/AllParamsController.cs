namespace Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

public class AllParamsController : Controller2
{
	public string StringParam { get; private set; } = null!;
	public int IntParam { get; private set; }
	public decimal DecimalParam { get; private set; }
	public bool BoolParam { get; private set; }
	public string[] StringArrayParam { get; private set; } = null!;
	public int[] IntArrayParam { get; private set; } = null!;
	public decimal[] DecimalArrayParam { get; private set; } = null!;
	public bool[] BoolArrayParam { get; private set; } = null!;

	public virtual void Invoke(
		string stringParam,
		int intParam,
		decimal decimalParam,
		bool boolParam,
		string[] stringArrayParam,
		int[] intArrayParam,
		decimal[] decimalArrayParam,
		bool[] boolArrayParam)
	{
		StringParam = stringParam;
		IntParam = intParam;
		DecimalParam = decimalParam;
		BoolParam = boolParam;
		StringArrayParam = stringArrayParam;
		IntArrayParam = intArrayParam;
		DecimalArrayParam = decimalArrayParam;
		BoolArrayParam = boolArrayParam;
	}
}