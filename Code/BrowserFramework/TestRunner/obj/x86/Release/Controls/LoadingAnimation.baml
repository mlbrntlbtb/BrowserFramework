   M S B A M L   `   `   `  ????   
TestRunner*    $TestRunner.Controls.LoadingAnimation   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     >?Name$  MyLoadingAnimation?? ? 	Resources    loadingAnimation ?? Triggers P? Property P? Value N? EnterActions ??Name W? From	 W? To	 g? Duration
 g? RepeatBehavior ?? 
TargetName ?? TargetProperty N? ExitActions ?? BeginStoryboardName J? Opacity H? Drawing ? Brush ? Geometry ?? Rect I? 	Transform ?? Angle ?? CenterX ?? CenterY
 ? Pen >? 	Thickness >? Brush E? 
StartPoint ?? Point ?? RotationAngle ?? SweepDirection ?? 
IsLargeArc  ?? Size! ?? Point??%!  &	        ??  P?  ? y?$ Visible=? ?? $ 	animation??.???? .??W? $ 0X?$	 359X?$	 0:0:1D?$
 Forever??$ angle??$ Angle? ?? $ 	animation??.R??? $??content??$ 1Q???H?  I? .??? 
 ????  ?? $ 0,0,1,1?I?  ?? 	angle??$ 0Q?$	 0.5Q?$	 0.5Q?.???  >? $	 0.1Q?
 ??Zj? B? .??E? $ 0.9,0.5&?.???? $ 0.5,0.1&?$ 90Q?$ 	Clockwise=? . $  0.4,0.4??? 
 ??Zj? B? .??E? $ 0.5,0&?.???? $! 0.7,0.1&??? $! 0.5,0.2&?.???? -   $??loading??#??[  ???? " ?? Path$" Width?# ?? ElementName$# MyLoadingAnimation??y??? $" 
Visibility?$# MyLoadingAnimation??