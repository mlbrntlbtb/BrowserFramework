   M S B A M L   `   `   `  ????   
TestRunner     TestRunner.EnterProductKey   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     ? Title$  Deltek Test Runner??$	??200??$	??450?? ? 
ResizeMode$ NoResize=? ? WindowStyle$ 
ToolWindow=? ? WindowStartupLocation$ CenterScreen=? ? ShowInTaskbar .  ? Icon$ TestRunner.ico?? ? 	Resources    MyButtonStyle2 ?? 
TargetType ?? Property	 ?? Value
 ?? Triggers P? Property P? Value??%  &	        ?? " ??.???  ? ??
	 ???????  ? ??
	 ???F???  ? ??$	 10(???  ? ??$	 50????  ? ??$	 20????  ? ??$	 2,2,2,2q???  ? ??$	 Center=???  ? ??$	 Center=?
 P?  ? {? .  . ???  ?@J?Opacity$		 0.5Q?.??? ????  ?? ImageSource$< 6pack://siteoforigin:,,,/Resources/DeltekBackground.png?? j? Stretch$ UniformToFill=?.+???  ?? Orientation$ Vertical=?$??2,0,2,5q?$??Center=?$??Center=?$	??180??$	??400??.+??? -    ??Name$ PanelMainRightTop??$ Vertical=?$??0,5,0,0q?.+??? $ Vertical=?$??6,10,0,0q?$
??Left=?.+??? $ 
Horizontal=?$??0,0,0,0q?$
??Left=?.+??? -    ??Name$ 
LblWelcome??$??Welcome to ??$
??Left=?$??Center=?$??27??$
??Left=??? -   $ LblTestRunner??$??-7,0,0,0q?$??Deltek Test Runner??$
??Left=?$??Center=?$??27??$
??Left=?$
??Bold"?
?????F??? $ 
Horizontal=?$??0,10,0,0q?$
??Left=?.+??? -   $ 	LblSelect??$??Please enter Product Key:??$
??Left=?$??Center=?$	??300??$??28??$??10(?
????·??? -   $??TxtProductKey??$ 
Horizontal=?$??	10,-3,0,0q?$
??Left=?.+??? -   $??TxtProductKeyValue??$	??385??$??0q?$??25??$	??Top=?$
??Left=? ?? CharacterCasing$ Upper=? ?? 	MaxLength$ 19??$??	ExtraBold"?$??Center=?$??Center=?$??15(?.????O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ?? 6sys&clr-namespace:System;assembly=mscorlib     " ?? ?? 	Resources   CueBannerBrush j? 
AlignmentX j? 
AlignmentY !? Visual??%?   &	       !? $ Center=?$ Center=?$
 None=? ?? $(??"Dashes will be added automatically??
???????
???????$	??385??$??25??$??Center=?$??Center=?
 P?  ? ?? ?? Empty# Z . ???  ? ??#	 [ P?  ? ?? Z? . ???  ? ??
	 ?????P?  ?@J?IsKeyboardFocused . . ???  ? ??
	 ?????.???? $ 
Horizontal=?$??0,5,0,0q?$??Center=?.+??? -   $??ErrorMessage??$4??.The Product key has Expired, please renew now!??$y?Hidden=?$??Italic$?$??	ExtraBold"?$??10(?$??0q?$??23??$??Center=?$	??349??
???  ??$??Center=??? $ 
Horizontal=?$??Center=?$??0,8,0,10q?.+??? -	    ??Name$ BtnNext??$
??Next??$??80??{?.  #??[  ?? -
   $ BtnClose??$??Close??$??80??$??5,0,0,0q?#??[  