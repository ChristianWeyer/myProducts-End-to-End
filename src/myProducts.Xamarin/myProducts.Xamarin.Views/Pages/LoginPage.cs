using myProducts.Xamarin.Contracts.i18n;
using myProducts.Xamarin.Contracts.ViewModels;
using myProducts.Xamarin.Views.Components;
using myProducts.Xamarin.Views.Contracts;
using myProducts.Xamarin.Views.Extensions;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Pages
{
	public class LoginPage : BasePage
	{
		private readonly ILoginPageViewModel _viewModel;
		private readonly ITranslation _translation;

		public LoginPage(ILoginPageViewModel viewModel, ITranslation translation, IViewLocator viewLocator)
		{
			_viewModel = viewModel;
			_viewModel.NavigateToMainPageCommand = new Command(async () => await Navigation.PushAsync(viewLocator.MainPage));
			BindingContext = _viewModel;
			
			_translation = translation;
			CreateUI();
		}

// ReSharper disable InconsistentNaming
		private void CreateUI()
// ReSharper restore InconsistentNaming
		{
			this.SetDefaultPadding();

			var stackLayout = new StackLayout()
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};

			var userLoginLabel = CreateUserLoginLabel();
			var userNameEntry = CreateUserNameEntry();
			var passwordEntry = CreatePasswordEntry();
			var loginButton = CreateLogInButton();
			var errorLabel = CreateErrorLabel();

			this.SetBinding<ILoginPageViewModel>(IsBusyProperty, m => m.IsBusy);

			stackLayout.Children.AddRange(userLoginLabel, errorLabel, userNameEntry,
				passwordEntry, loginButton);

			SetScrollViewContent(stackLayout);
		}

		private Label CreateErrorLabel()
		{
			var label = new Label()
			{
				Text = _translation.LogInNotPossible,
				IsVisible = false,
				TextColor = Color.Red,
			};
			label.SetBinding<ILoginPageViewModel>(Label.IsVisibleProperty, m => m.ErrorOccured);

			return label;
		}

		private Button CreateLogInButton()
		{
			var button = new Button()
			{
				Text = _translation.LogIn,
			};
			button.SetBinding<ILoginPageViewModel>(Button.CommandProperty, m => m.LogInCommand);

			return button;
		}

		private Entry CreatePasswordEntry()
		{
			var entry = new Entry()
			{
				Placeholder = _translation.Password,
				IsPassword = true,
			};
			entry.SetBinding<ILoginPageViewModel>(Entry.TextProperty, m => m.Password, BindingMode.TwoWay);

			return entry;
		}

		private Entry CreateUserNameEntry()
		{
			var entry = new Entry()
			{
				Placeholder = _translation.UserName,
			};
			entry.SetBinding<ILoginPageViewModel>(Entry.TextProperty, m => m.UserName, BindingMode.TwoWay);

			return entry;
		}

		private Label CreateUserLoginLabel()
		{
			return new Label()
			{
				Text = _translation.UserLogin,
				Font = Font.SystemFontOfSize(NamedSize.Large),
			};
		}
	}
}