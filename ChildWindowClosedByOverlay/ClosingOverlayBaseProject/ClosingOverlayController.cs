using System;
using System.Windows;
using System.Windows.Controls;

namespace ClosingOverlayBaseProject
{
	public class ClosingOverlayController
	{
		public ClosingOverlayController(FrameworkElement childWindow, Action closeAction)
		{
			_childWindow = childWindow;
			_close = closeAction;
		}

		/// <summary>
		/// Disable the closing by the overlay from XAML or before the Load event.
		/// </summary>
		public Boolean DisabledClosingOverlay { get; set; }

		/// <summary>
		/// The sign that the right mouse button is closing too, by clicking on the overlay.
		/// </summary>
		public Boolean RightMouseButtonClosingTo { get; set; }

		/// <summary>
		/// The associated ChildWindow.
		/// </summary>
		private FrameworkElement _childWindow;

		/// <summary>
		/// The associated ChildWindow's Close method.
		/// </summary>
		private Action _close;

		/// <summary>
		/// The default overlay of the ChildWindow.
		/// </summary>
		private FrameworkElement OverlayFromTemplate
		{
			get
			{
				var childWindow = _childWindow;
				if (childWindow == null)
				{
					return null;
				}

				var dependencyOverlay = childWindow.GetChildByName("Overlay");
				var elementOverlay = dependencyOverlay as FrameworkElement;

				return elementOverlay;
			}
		}

		/// <summary>
		/// The ChildWindow.<see cref="FrameworkElement.Loaded"/> event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="routedEventArgs"></param>
		public void ChildWindowLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			// The first Loaded event fired before elements form templates created.
			// We must use the delayed invoking.
			ProcessAfterTemplatedChildrenCreated();
		}

		/// <summary>
		/// The unloaded event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="routedEventArgs"></param>
		public void ChildWindowUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			UnsetOverlayCloseEvents();
		}

		/// <summary>
		/// Invokes a passed action after creating of children controls in templates.
		/// </summary>
		/// <remarks>
		/// The first Loaded event fired before elements form templates created.
		/// We must use the delayed invoking.
		/// </remarks>
		private void ProcessAfterTemplatedChildrenCreated()
		{
			Deployment.Current.Dispatcher.BeginInvoke(CheckerWithRecursionOrProceedExecution);
		}

		/// <summary>
		/// The recursion's max amount.
		/// </summary>
		private const Int32 FranticRecursionMaxAmount = 50;
		/// <summary>
		/// The recursion's counter.
		/// </summary>
		private Int32 _recursionCounter;

		/// <summary>
		/// Checks the existens of the Overlay of the target ChildWindow.<para/>
		/// Will proceed the execution if it exist.
		/// </summary>
		private void CheckerWithRecursionOrProceedExecution()
		{
			if (_recursionCounter == FranticRecursionMaxAmount)
			{
				// It's a sily case. I'd never seen this. But we should be aware about it.
				// Let's interrupt the behaviour, cause it something strange and need investigations.

				return;
			}

			if (_childWindow.HasChildren() == false)
			{
				// The real case when children elements from the ChildWindow template very delayed with creation.

				_recursionCounter++;
				ProcessAfterTemplatedChildrenCreated();

				return;
			}
			_recursionCounter = 0;

			var overlay = OverlayFromTemplate;
			if (overlay == null)
			{
				// The overley not exist.
				// Maybe you have changed a ChildWindow control template.

				// Let's interrupt the behaviour!
				return;
			}

			SetOverlayCloseEvents();
		}


		/// <summary>
		/// Turns on this behaviour.
		/// </summary>
		private void SetOverlayCloseEvents()
		{
			var overlay = OverlayFromTemplate;
			if (overlay == null)
			{
				return;
			}

			overlay.MouseLeftButtonUp += Overlay_MouseSomeButtonUp;

			if (RightMouseButtonClosingTo)
			{
				overlay.MouseRightButtonDown += Overlay_MouseSomeButtonUp;
			}
		}

		/// <summary>
		/// Workaround for memory leaks.
		/// </summary>
		private void UnsetOverlayCloseEvents()
		{
			var overlay = OverlayFromTemplate;
			if (overlay == null)
			{
				return;
			}

			overlay.MouseLeftButtonUp -= Overlay_MouseSomeButtonUp;
			overlay.MouseRightButtonDown -= Overlay_MouseSomeButtonUp;
		}

		/// <summary>
		/// The overley's click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Overlay_MouseSomeButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (_close == null)
			{
				return;
			}

			_close();

			// It's a workaround for the abnormal controls blocking.
			// It will happen when we click faster on the overlay before a ChildWindow was opened.
			// http://stackoverflow.com/questions/6456952/silverlight-modal-childwindow-keeps-parent-grayed-after-closing
			// http://social.msdn.microsoft.com/Forums/silverlight/en-US/89d38ae0-5d55-4b6e-9981-894321082a07/childwindow-blocks-the-main-window-perminently
			Application.Current.RootVisual.SetValue(Control.IsEnabledProperty, true);
		}

		/// <summary>
		/// This method intended to treat memory leaks of Silverlight behaviours.
		/// It's not tested for leaks.
		/// </summary>
		public void DestroyForSakeSilverlightsBehaviour()
		{
			_childWindow = null;
			_close = null;
		}
	}
}
