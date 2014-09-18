using System;
using System.Net.Http.Headers;
using myProducts.Xamarin.Contracts.i18n;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class InfoPage : BasePage
	{
		private readonly ITranslation _translation;

		public InfoPage(ITranslation translation)
		{
			_translation = translation;
			CreateUI();
		}

		private void CreateUI()
		{
			var stackLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children =
				{
					new Label()
					{
						Text = _translation.InfoHeadline,
						Font = Font.SystemFontOfSize(NamedSize.Large)
					},
					new Label()
					{
						Text = String.Format("{0} {1}", _translation.CreatedBy, "christian.weyer@thinktecture.com"),
					},
					new Label()
					{
						Text = _translation.InfoText
					}
				}
			};

			SetScrollViewContent(stackLayout);
		}
	}
}