using myProducts.Xamarin.Views.Components;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public abstract class BasePage : ContentPage
	{
		protected ScrollView ScrollView;

		public BasePage()
		{
			CreateUI();
		}

		private void CreateUI()
		{
			ScrollView = new ScrollView()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			Content = new StackLayout()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children =
				{
					ScrollView,
					new Footer()
				},
			};
		}

		protected virtual void SetScrollViewContent(View content)
		{
			ScrollView.Content = content;
		}
	}
}