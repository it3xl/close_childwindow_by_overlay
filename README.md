# Close the ChildWindow by its Overlay

### The Silverlight's fastest and proven solution to make you ChildWindows control to be closed by its overlay.

[Created Dec 24, 2013; Migrated from CodePlex](https://closechildwindowbyoverlay.codeplex.com/)

You may use:
1. Silverlight Behaviour,
1. simple inheritance from the tweaked CildWindow,
1. instantiating of the tweaked CildWindow class,
1. or both.

The main idea of this project is to deliver the following things:
1. Guarantee closing of your ChildWindow by a first tap on its overlay.
1. Resolve the locking problem of RootVisual's controls after a ChildWindow will be closed.
1. Get a complete and proven solution at once00.
1. Save the own time.
1. and I humbly hope that this common in the world of WEB 2.0 approach will be included to the Silverlight ChildWindow control.


## How to use

Add .dll files of this project to your project.<br/>
Use one of next code approaches:

### By the behavior 
```xml
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
```

### By the inheritance or instantiating

```cs
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
```

----

## Silverlight's live example
[ChildWindow Closed By Overlay solution](http://it3xl.ru/Resources_For_External/codeplex/CloseChildWindowByOverlay/ChildWindowClosedByOverlay.Silverlight.Web/)

**The example's screenshort:**

![Close the ChildWindow by its Overlay screenshort](http://it3xl.ru/Resources_For_External/codeplex/CloseChildWindowByOverlay/how_it_looks.jpg)

it3xl.com
