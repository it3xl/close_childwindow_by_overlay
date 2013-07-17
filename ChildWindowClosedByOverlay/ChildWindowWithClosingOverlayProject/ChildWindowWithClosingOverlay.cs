using System;
using System.Windows.Controls;
using ClosingOverlayBaseProject;

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
			_closingOverlayController = new ClosingOverlayController(this, Close);

			Loaded += _closingOverlayController.ChildWindowLoaded;
			Unloaded += _closingOverlayController.ChildWindowUnloaded;
		}

		/// <summary>
		/// <see cref="ClosingOverlayController"/>
		/// </summary>
		private readonly ClosingOverlayController _closingOverlayController;

		/// <summary>
		/// Disable the closing by the overlay from XAML or before the Load event.
		/// </summary>
		public Boolean DisabledClosingOverlay
		{
			get
			{
				return _closingOverlayController.DisabledClosingOverlay;
			}
			set
			{
				_closingOverlayController.DisabledClosingOverlay = value;
			}
		}

		/// <summary>
		/// The sign that the right mouse button is closing too, by clicking on the overlay.
		/// </summary>
		public Boolean RightMouseButtonClosingTo
		{
			get
			{
				return _closingOverlayController.RightMouseButtonClosingTo;
			}
			set
			{
				_closingOverlayController.RightMouseButtonClosingTo = value;
			}
		}




	}
}
