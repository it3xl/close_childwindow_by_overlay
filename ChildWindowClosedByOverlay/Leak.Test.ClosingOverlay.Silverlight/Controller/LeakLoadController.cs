using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ChildWindowWithClosingOverlayProject;
using Leak.Test.ClosingOverlay.Silverlight.View;

namespace Leak.Test.ClosingOverlay.Silverlight.Controller
{
	public class LeakLoadController
	{
		private readonly DispatcherTimer _loadTimer = new DispatcherTimer
		{
			Interval = TimeSpan.FromMilliseconds(100),
		};

		private Grid _injectionElement;


		private ChildWindow _withOverlay;
		private ChildWindow _withBehaviour;

		internal void StartLoad(Grid injectionPlace)
		{
			_injectionElement = injectionPlace;

			_loadTimer.Tick += _loadTimer_Tick;
			_loadTimer.Start();
		}

		void _loadTimer_Tick(object sender, EventArgs e)
		{
			SynchronizationContext.Current.Post(
				_ => SetLoad(_injectionElement),
				null
			);
		}

		private void SetLoad(Grid injectionElement)
		{
			LoadWithBehaviour();
			LoadWithOverlay();
		}


		private void LoadWithBehaviour()
		{
			CloseAll(_withBehaviour);
			SetWithBehaviour();
		}

		private void SetWithBehaviour()
		{
			var content = new TextBlock
				{
					Margin = new Thickness(30, 30, 30, 30),
					Text = "Touch my Overlay to close me.\n\nI have the OverlayClosingBehavior.",
					FontWeight = FontWeights.Bold,
				};

			_withBehaviour = new TestChildWindowWithBehaviour
				{
					Title = "ChildWindow closed by Overlay",
					Content = content,
					OverlayBrush = new SolidColorBrush(Colors.Transparent),
					OverlayOpacity = 0.4,
					HasCloseButton = false,
				};
			_withBehaviour.Show();
		}


		private void LoadWithOverlay()
		{
			CloseAll(_withOverlay);
			SetWithOverlay();
		}

		private void SetWithOverlay()
		{
			var content = new TextBlock
				{
					Margin = new Thickness(30, 30, 30, 30),
					Text = "Touch my Overlay to close me.\n\nI have no Silverlight's behaviors.",
					FontWeight = FontWeights.Bold,
				};

			_withOverlay = new ChildWindowWithClosingOverlay
				{
					// !!! Custom settings.
					DisabledClosingOverlay = false,
					RightMouseButtonClosingTo = false,


					Title = "ChildWindow closed by Overlay",
					Content = content,

					OverlayBrush = new SolidColorBrush(Colors.Transparent),
					OverlayOpacity = 0.4,
					HasCloseButton = false,
				};
			_withOverlay.Show();
		}


		private static void CloseAll(ChildWindow childWindows)
		{
			if (childWindows == null)
			{
				return;
			}
			
			childWindows.Close();
		}

	}
}
