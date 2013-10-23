using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using ClosingOverlayBaseProject;

namespace OverlayClosingBehaviorProject
{
	/// <summary>
	/// It's Silverlight behaviour that set the closing action on the <see cref="ChildWindow"/>'s Overlay.<para/>
	/// <para/>
	/// if you don't want to use the Silverlight behaviours then you may use the ChildWindowWithClosingOverlay from 
	/// the ChildWindowWithClosingOverlayProject at the https://overlayclosingchildwindowbehavior.codeplex.com/.
	/// </summary>
	/// <example>
	/// For examples of use see at the project's host at the https://overlayclosingchildwindowbehavior.codeplex.com/
	/// </example>
	public class OverlayClosingChildWindowBehavior : Behavior<ChildWindow>
	{
		/// <summary>
		/// <see cref="ClosingOverlayController"/>
		/// </summary>
		private ClosingOverlayController _closingOverlayController;

		public Boolean _disabledClosingOverlay;
		/// <summary>
		/// Disable the closing by the overlay from XAML or before the Load event.
		/// </summary>
		public Boolean DisabledClosingOverlay
		{
			get
			{
				if (_closingOverlayController == null)
				{
					return _disabledClosingOverlay;
				}

				return _closingOverlayController.DisabledClosingOverlay;
			}
			set
			{
				if (_closingOverlayController == null)
				{
					_disabledClosingOverlay = value;

					return;
				}

				_closingOverlayController.DisabledClosingOverlay = value;
			}
		}

		public Boolean _rightMouseButtonClosingTo;
		/// <summary>
		/// The sign that the right mouse button is closing too, by clicking on the overlay.
		/// </summary>
		public Boolean RightMouseButtonClosingTo
		{
			get
			{
				if (_closingOverlayController == null)
				{
					return _rightMouseButtonClosingTo;
				}

				return _closingOverlayController.RightMouseButtonClosingTo;
			}
			set
			{
				if (_closingOverlayController == null)
				{
					_rightMouseButtonClosingTo = value;

					return;
				}

				_closingOverlayController.RightMouseButtonClosingTo = value;
			}
		}

		/// <summary>
		/// The associated <see cref="ChildWindow"/>.
		/// </summary>
		private ChildWindow ChildWindow
		{
			get
			{
				return AssociatedObject;
			}
		}


		/// <summary>
		/// <see cref="Behavior.OnAttached"/>
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();

			if (DisabledClosingOverlay)
			{
				return;
			}

			_closingOverlayController = new ClosingOverlayController(ChildWindow, ChildWindow.Close);

			// Workaround for the late initializing of the _closingOverlayController;
			_closingOverlayController.DisabledClosingOverlay = _disabledClosingOverlay;
			_closingOverlayController.RightMouseButtonClosingTo = _rightMouseButtonClosingTo;

			ChildWindow.Loaded += _closingOverlayController.ChildWindowLoaded;
			ChildWindow.Unloaded += _closingOverlayController.ChildWindowUnloaded;
		}

		/// <summary>
		/// <see cref="Behavior.OnDetaching"/>
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();

			ChildWindow.Loaded -= _closingOverlayController.ChildWindowLoaded;
			ChildWindow.Unloaded -= _closingOverlayController.ChildWindowUnloaded;

			_closingOverlayController.DestroyForSakeSilverlightsBehaviour();
		}

	}
}
