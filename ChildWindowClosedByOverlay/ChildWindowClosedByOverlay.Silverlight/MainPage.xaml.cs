﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ChildWindowClosedByOverlay.Silverlight.View;
using ChildWindowWithClosingOverlayProject;

namespace ChildWindowClosedByOverlay.Silverlight
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();

			Loaded += MainPage_Loaded;
		}

		void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			ShowTestChildWindowWithBehaviour();
		}

		private void ShowChildWindowWithBehaviourButton_Click(object sender, RoutedEventArgs e)
		{
			ShowTestChildWindowWithBehaviour();
		}

		private void ShowChildWindowButton_Click(object sender, RoutedEventArgs e)
		{
			ShowTestChildWindow();
		}

		private static void ShowTestChildWindowWithBehaviour()
		{
			var content = new TextBlock
			{
				Margin = new Thickness(30, 30, 30, 30),
				Text = "Touch my Overlay to close me.\n\nI have the OverlayClosingBehavior.",
				FontWeight = FontWeights.Bold,
			};

			new TestChildWindowWithBehaviour
			{
				Title = "ChildWindow closed by Overlay",
				Content = content,
				OverlayBrush = new SolidColorBrush(Colors.Gray),
				OverlayOpacity = 0.4,
				HasCloseButton = false,
			}
			.Show();
		}


		private static void ShowTestChildWindow()
		{
			var content = new TextBlock
			{
				Margin = new Thickness(30, 30, 30, 30),
				Text = "Touch my Overlay to close me.\n\nI have no Silverlight's behaviors.",
				FontWeight = FontWeights.Bold,
			};

			new ChildWindowWithClosingOverlay
			{

				// !!! Custom settings.
				DisabledClosingOverlay = false,
				RightMouseButtonClosingTo = false,


				Title = "ChildWindow closed by Overlay",
				Content = content,

				OverlayBrush = new SolidColorBrush(Colors.Gray),
				OverlayOpacity = 0.4,
				HasCloseButton = false,
			}
			.Show();
		}


	}
}
