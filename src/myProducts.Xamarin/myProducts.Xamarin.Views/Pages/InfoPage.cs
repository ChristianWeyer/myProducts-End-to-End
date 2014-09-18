using System;
using System.Net.Http.Headers;
using myProducts.Xamarin.Contracts.i18n;
using myProducts.Xamarin.Contracts.ViewModels;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class InfoPage : BasePage
	{
		private const string MailWeyer = "christian.weyer@thinktecture.com";
		private const string MailRauber = "manuel.rauber@thinktecture.com";
		private readonly ITranslation _translation;
		private readonly IInfoPageViewModel _viewModel;

		public InfoPage(ITranslation translation, IInfoPageViewModel viewModel)
		{
			_translation = translation;
			_viewModel = viewModel;
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
						Text = String.Format("{0} {1}", _translation.CreatedBy, MailWeyer),
						GestureRecognizers =
						{
							new TapGestureRecognizer()
							{
								Command = new Command(() => _viewModel.SendInfoMail(MailWeyer))
							}
						}
					},
					new Label()
					{
						Text = String.Format("Xamarin-App {0} {1}", _translation.CreatedBy.ToLower(), MailRauber),
						GestureRecognizers =
						{
							new TapGestureRecognizer()
							{
								Command = new Command(() => _viewModel.SendInfoMail(MailRauber))
							}
						}
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