   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:TestRunner.CommonTestRunner.Common  5   	   (    "TestRunner.SetExternalScriptDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2commonclr-namespace:TestRunner.Common     5   	   -   5          ? Title$  Set External Script??5   	   $	??400??6%   $	??380??62    ? WindowStartupLocation$ CenterOwner=?5   	    ? WindowStyle$ 
ToolWindow=?6-    ? 
ResizeMode$ NoResize=?6F   .??5      ? ??5   
   ?? 5	       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T   .+?5      ]? ??. .+?5      ??  ?? Orientation$ 
Horizontal=?6!   $??Right=?6:   $??10q?6V   $??Bottom=?6b   .+?5      ?? -   5         ??Name$ btnOK??5   !      ButtonStyle2#??[  60   $??80??6V   $??Stretch=?6a   $??20??6   .??6?   OK6?   ?? 5      -   5        $ 	btnCancel??5   !   #??[  64   $??80??6Z   $??Stretch=?6e   $??5,0,0,0q?6?   $??20??6?   .??6?   Cancel6?   5      ?? 5      -   5         ??Name$ 	tbScripts??5   !   $??
10,10,10,0q?64   ???	#FF90D2FF.@?5      ?? -   5        	 ??Name$	 
tpgScripts??5   "   $??Scripts??66   $y?	Collapsed=?6G   .??5      ?? -   5        
 ??Name$
 gbScript??5   '   $??Pre Execution??69   $??5q?6P   .??5   "   ?? $??0??6-   $??10q?6=   .+?5   &   ?? $ 
Horizontal=?61   $??0,5,0,0q?6J   .+?5   *   ?? -   5         ??Name$ 
lbTypeDesc??5   0   5   '   ?? 5   &   $ Vertical=?61   $??0,5,0,0q?6H   .+?5   *   ?? $??5,0,0,0q?60   .??6A   Path6G   ]? 5   *   ??. .+?5   &   ?? -   5        $ 	btnBrowse??5   -   $	??...??6@   $??Right=?6N    ? ToolTip$ Browse??6j   $??Right=?6{   $	??5,0q?6?   $??22??6?   $??22??6?   6?   ?? 5   .   -	   5         ??Name$ txtPath??5   6    ?? AcceptsReturn .   TextBoxStyle1#??[ 6\   $
??Auto??6?   $??Stretch=?6?   $	??5,0q?6?    ?? 
IsReadOnly . 
??????? 6?   ??  ?? RelativeSource ?? *Self+ ?? Path$
 Text?6  5   +   5   '   ?? 5   &   $ Vertical=?61   $??0,5,0,0q?6H   .+?5   *   ?? $??5,0,0,0q?60   .??6A   Arguments (optional)6W   ?? 5    *   -
   5        $ txtArgs??5    2    . #??[ 6X   $
??Auto??6   $??Stretch=?6?   $	??5,0q?6?   5!   '   ?? 5"   &   $ Vertical=?61   $??0,5,0,0q?6H   .+?5#   *   ?? $??5,0,0,0q?60   .??6A   Start in (optional)6V   ?? 5$   *   -   5        $ 
txtStartIn??5$   2    . #??[ 6[   $
??Auto??6?   $??Stretch=?6?   $	??5,0q?6?   5%   '   ?? 5&   &   $ 
Horizontal=?61   $??0,5,0,0q?6J   .+?5'   *   ?? -   5         ??Name$ cbWait??5'   D   $??5,0,0,0q?63   $??Center=?6T   $??Wait to finish??6o   6?   5(   '   5)   #   5*      5+      5,      5-      5.      5/      