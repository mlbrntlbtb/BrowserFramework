   M S B A M L   `   `   `  ????  
TestRunner$    TestRunner.ExportReportsDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          ? Title$  Export??5   	   $	??260??6   $	??280??6%    ? WindowStartupLocation$ CenterOwner=?6H    ? WindowStyle$ 
ToolWindow=?6l    ? 
ResizeMode$ NoResize=?6?   .??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T   .+?5	   
   ]? ??. .+?5
      ??  ?? Orientation$ 
Horizontal=?6   $??1??62   $
??Left=?6?   $??Bottom=?6[   $??40??6s   $??	150,0,0,0q?6   .+?5      ?? -   5         ??Name$ btnOk??5         ButtonStyle2#??[  6(   $??OK??6N   6[   ?? 5      -   5        $ 	btnCancel??5      #??[  6,   $??Cancel??6R   6c   5      ?? 5      -   5         ??Name$ tabSettings??5      $??5,5,5,0q?6.   ???	#FF90D2FF.@?5      ?? -   5        	 ??Name$	 tpgEmail??5      $?? ??6,   $y?	Collapsed=?66   .??5      ?? $??Stretch=?6   .+?5      ?? -   5        
 ??Name$
 	groupBox1??5   }   $??Export to Excel??6   $??Stretch=?68   $??Stretch=?6T   $??5q?6r   .??5      ? .+?5      ?? $ 
Horizontal=?6)   $??
10,10,10,0q?6B   $
??Left=?6V   $	??Top=?6q   .+?5   "   ?? -   5         ??Name$ chkSchedulesInTestLineup??5   +   $??30??6M    J? RenderTransformOrigin$ 	0.5,0.967&?6Y   .??5   &   ??  ?? TextWrapping$
 Wrap=?61   $??Test Schedules??6E   $??20??6[   5   #   5      ?? 5      $ 
Horizontal=?6)   $??10,40,10,70q?6B   $
??Left=?6W   .+?5   "   ?? -   5        $ chkResultsHistory??5   +   $??30??6?   .??5   &   ?? $
 Wrap=?61   $??Results History??6E   5   #   5      ?? 5      $ 
Horizontal=?6)   $??50,65,10,40q?6B   $
??Left=?6W   .+?5   &   	? -	   5         	?Name$ optAll??5   2   $??48??6C   $	??All??6g   	? 5   &   -
   5        $ optLatestOnly??5   2   $??Latest Only??6i   5       ?? 5!      $ 
Horizontal=?6)   $??10,100,10,10q?6B   $
??Left=?6X   .+?5"   &   ?? -   5        $ chkAgentsInformation??5"   /   $??30??6M   .??5#   &   ?? $
 Wrap=?61   $??Agents Information??6E   5$   #   5%      5&      5'      5(      5)      5*      5+      5,      5-      