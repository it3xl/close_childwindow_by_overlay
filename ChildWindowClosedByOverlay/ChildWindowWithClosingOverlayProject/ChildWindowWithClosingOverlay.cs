using System;
using System.Windows;
using System.Windows.Controls;
using TreeTraversingProject;

namespace ChildWindowWithClosingOverlayProject
{
	/// <summary>
	/// It's the <see cref="ChildWindow"/> that may be closed by the click on its overlay.<para/>
	/// Inherits your <see cref="ChildWindow"/>s from this class.<para/>
	/// <para/>
	/// if you prefer to use the Silverlight behaviours then you may use the OverlayClosingChildWindowBehavior from 
	/// the OverlayClosingBehaviorProject at the https://overlayclosingchildwindowbehavior.codeplex.com/.
	/// </summary>
	/// <example>
	/// For examples of use see at the project's host at the https://overlayclosingchildwindowbehavior.codeplex.com/
	/// </example>
	public class ChildWindowWithClosingOverlay : ChildWindow
	{
		public ChildWindowWithClosingOverlay()
		{
			ChildWindow.Loaded += ChildWindowLoaded;
			ChildWindow.Unloaded += ChildWindowUnloaded;
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
		/// The associated <see cref="ChildWindow"/>.
		/// </summary>
		private ChildWindow ChildWindow
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// The default overlay of the <see cref="ChildWindow"/>.
		/// </summary>
		private FrameworkElement OverlayFromTemplate
		{
			get
			{
				var childWindow = ChildWindow;
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
		/// The <see cref="ChildWindow"/>.<see cref="FrameworkElement.Loaded"/> event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="routedEventArgs"></param>
		private void ChildWindowLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			// The first Loaded event fired before elements form templates created.
			// We must use the delayed invoking.
			ProcessAfterTemplatedChildrenCreated();
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
		/// Checks the Overlay of the <see cref="ChildWindow"/> exist.<para/>
		/// Will proceed the execution if it exist.
		/// </summary>
		private void CheckerWithRecursionOrProceedExecution()
		{
			if (ChildWindow.HasChildren() == false)
			{
				// The real case when children elements from the ChildWindow template very delayed with creation.

				ProcessAfterTemplatedChildrenCreated();

				return;
			}

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

		private void ChildWindowUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			UnsetOverlayCloseEvents();
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
			ChildWindow.Close();

			// It's a workaround for the abnormal controls blocking.
			// It will happen when we click faster on the overlay before a ChildWindow was opened.
			// http://stackoverflow.com/questions/6456952/silverlight-modal-childwindow-keeps-parent-grayed-after-closing
			// http://social.msdn.microsoft.com/Forums/silverlight/en-US/89d38ae0-5d55-4b6e-9981-894321082a07/childwindow-blocks-the-main-window-perminently
			Application.Current.RootVisual.SetValue(IsEnabledProperty, true);
		}
	}
}
