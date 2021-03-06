﻿using System.Windows.Input;
using Xamarin.Forms;

namespace myProducts.Xamarin.Views.Components
{
	public class MainPageHubItem
	{
		private readonly string _text;
		private readonly Color _boxColor;
		private readonly ICommand _navigationCommand;
		private readonly Color _textColor;
		private BoxView _boxView;
		private ContentView _label;

		public ContentView Label { get { return _label; } }

		public BoxView BoxView { get { return _boxView; } }

		public MainPageHubItem(string text, Color boxColor, ICommand navigationCommand) 
			: this(text, boxColor, navigationCommand, Device.OnPlatform(Color.White, Color.White, Color.White)) {}

		public MainPageHubItem(string text, Color boxColor, ICommand navigationCommand, Color textColor)
		{
			_text = text;
			_boxColor = boxColor;
			_navigationCommand = navigationCommand;
			_textColor = textColor;
			CreateUI();
		}

		private void CreateUI()
		{
			_boxView = CreateBox();
			_label = CreateLabel();
		}

		private ContentView CreateLabel()
		{
			return new ContentView()
			{
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.Start,
				Padding = new Thickness(15, 0, 0, 15),
				Content = new Label()
				{
					Text = _text,
					TextColor = _textColor,
				}
			};
		}

		private BoxView CreateBox()
		{
			return new BoxView()
			{
				Color = _boxColor,
				BackgroundColor = _boxColor,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				GestureRecognizers =
				{
					new TapGestureRecognizer()
					{
						Command = _navigationCommand,
					}
				}
			};
		}
	}
}