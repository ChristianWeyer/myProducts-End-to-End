namespace myProducts.Xamarin.Contracts.Text
{
	public interface IMailComposer
	{
		void Compose(string to, string subject, string body);
	}
}