using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Extensions;
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

			this.SetDefaultPadding();
		}

		protected virtual void SetScrollViewContent(View content)
		{
			ScrollView.Content = content;
		}
	}
}