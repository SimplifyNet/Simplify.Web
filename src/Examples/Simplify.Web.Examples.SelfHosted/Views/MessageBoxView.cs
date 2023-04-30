namespace Simplify.Web.Examples.SelfHosted.Views;

public class MessageBoxView : View
{
	public string Get(string message)
	{
		return TemplateFactory.Load("MessageBox").Set("Message", message).Get();
	}
}