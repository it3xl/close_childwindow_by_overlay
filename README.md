# close_childwindow_by_overlay

**Project Description**
The fastest and proven solution to make you ChildWindows closed by its overlay.

It's the ChildWindow Closed By Overlay solution.

You may use:
# Silverlight Behaviour,
# simple inheritance from the tweaked CildWindow,
# instantiating of the  tweaked CildWindow class,
# or both.

The main idea of this project is to deliver the following things.
:1. Guaranteed closing of the ChildWindow the first time.
:2. Resolve the locking problem of RootVisual's controls after a ChildWindow closing.
:3. Get a complete and proven solution.
:4. Save the own time.

# and !

:5. **I humbly hope that this common in the world of WEB 2.0 approach will be included in the ChildWindow.**


**How to use**
Add .dll files to your project from the **DOWNLOAD** menu link above.
Use the next in code:

:1. By the behavior 
{code:xml}
<controls:ChildWindow
    x:Class="ChildWindowClosedByOverlay.Silverlight.TestChildWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" 
    xmlns:controls="clr-namespace:System.Windows.Controls;assembly=System.Windows.Controls"
    xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
    xmlns:Behaviors="clr-namespace:OverlayClosingBehaviorProject;assembly=OverlayClosingBehaviorProject"
    >
    
    <i:Interaction.Behaviors>
        <Behaviors:OverlayClosingChildWindowBehavior
            DisabledClosingOverlay="False"
            RightMouseButtonClosingTo="False"
        />
    </i:Interaction.Behaviors>
<!-- ... -->
{code:xml}

:2. By the inheritance or instantiating
{code:c#}
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

{code:c#}
----
**Live example:**
[ChildWindow Closed By Overlay solution](http://it3xl.ru/Resources_For_External/codeplex/CloseChildWindowByOverlay/ChildWindowClosedByOverlay.Silverlight.Web/)

----
**The example's screenshort:**
![](Home_http://it3xl.ru/Resources_For_External/codeplex/CloseChildWindowByOverlay/how_it_looks.jpg)

it3xl.com
