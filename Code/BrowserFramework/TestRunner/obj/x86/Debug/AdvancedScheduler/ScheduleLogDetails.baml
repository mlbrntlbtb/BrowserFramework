   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TestRunner.AdvancedSchedulerTestRunner.AdvancedScheduler  5   	   5    /TestRunner.AdvancedScheduler.ScheduleLogDetails   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:TestRunner.AdvancedScheduler     5   	   -   5          ? Title$  View Status Logs??5   	   $	??520??6"   $
??1280??6/    ? WindowStartupLocation$ CenterScreen=?6<    ? 
ResizeMode$ NoResize=?5	   	    ? ShowInTaskbar .  ? 	Resources 5
          BooleanToVisibilityConverter??%   &	        ?? 5   
   6'   5      .??5      ? -   5         ?Name$ gridMain??5      $??Stretch=?6   ??5   
   ?? 5       ?? ImageSource$< 6pack://siteoforigin:,,,/Resources/DeltekBackground.png??6    j? Stretch$ UniformToFill=?6^    ? RowDefinitions 5   
   ?? 5      $??50 ?6   ?? 5      .+?5   
   ?? $??0??6   $??10,10,1047.333,0q?6"   $??2??6<   $
??Auto??6M   $	??Top=?6[   
?????? .@?5      ?? $y?Hidden=?6   .??5      ]? $
??Auto??6   
?????? .+?5      ?? -   5        	 ??Name$	 	lblFilter??5      $??Display logs:??6/   $??0q?6G   $
??Left=?6[   $
??Left=?6v   ?? 5      -   5        
 ??Name$
 	cmbFilter??5      $	??120??62   $	??Top=?6>   $	??0,3q?6V   $
??Left=?6c   $
??Left=?6~   5       5      5      5      ]? 5   
   -   5         ]?Name$ CenterStack??5      $??
10,5,10,10q?6)   $??2??6=   $??1??6M   $??Stretch=?6Z   .+?5      ]? $??Stretch=?6   $
??Left=?64   $	??Top=?6O   .+?5       ?? -   5        $??tabLogs??5       $??0,0,0.333,5q?6,   $
??Auto??6A   $
??Auto??6N   .@?5!      ?? $
??Logs??6   $y?	Collapsed=?6,   .??5"      ? $??Stretch=?6    5#      ?? 5$   "   $??* ?60   ?? 5%   "   $??23 ?60   .+?5'      &   System.Windows.Controls.DataGrid  -   5          Name$ 	LogsQueue??5'   O   $
??Left=?6'   $??0??6B      RowHeaderWidth$ 0Q?6?     DataGridStyle1#??[ 5(      $   HorizontalScrollBarVisibility$
 Auto=?6F   "   VerticalScrollBarVisibility$
 Auto=?6k      SelectionMode$ Single=?5)         
IsReadOnly .    SelectionUnit$ FullRow=?6G      CanUserAddRows .     CanUserDeleteRows .    DataGridRowStyle0   RowStyle# [ 5*   O   $	??Top=?5+   !    %? IsVirtualizing . $??Stretch=?6c      CanUserResizeColumns . ??5'   b   ?? *ScheduleLogsRecord+   	CellStyle 5,   "   ?? 5-   &    ?? 
TargetType*  $System.Windows.Controls.DataGridCell"  6,    ?? Triggers 5.   *   ?? 5/   .    ?? Binding 6:   ?? *mTestStatus+ ?? Value
 Passed6Z   .a?50   2   ??  ?? Property ? ?? ?? Value
 ?????6O   ?? 51   2    ? ??
 ??  ?6O   52   /   ?? 53   .    6:   ?? *mTestStatus+
 Failed6Z   .a?54   2   ??  ? ??
 ?r???6O   ?? 55   2    ? ??
 ?   ?6O   56   /   ?? 57   .    6:   ?? *mTestStatus+	 Error6Z   .a?58   2   ??  ? ??
 ?r???6O   ?? 59   2    ? ??
 ?   ?6O   5:   /   ?? 5;   .    6:   ?? *mTestStatus+ Warning6Z   .a?5<   2   ??  ? ??
 ?r???6O   ?? 5=   2    ? ??
 ?   ?6O   5>   /   ?? 5?   .    6:   ?? *mTestStatus+ Pending6Z   .a?5@   2   ??  ? ??
 ?????6O   ?? 5A   2    ? ??
 ??  ?6O   5B   /   ?? 5C   .    6:   ?? *mTestStatus+ 	Cancelled6Z   .a?5D   2   ??  ? ??
 ?????6O   ?? 5E   2    ? ??
 ??  ?6O   5F   /   ?? 5G   .    6:   ?? *mTestStatus+ Disconnected6Z   .a?5H   2   ??  ? ??
 ?????6O   ?? 5I   2    ? ??
 ??  ?6O   5J   /   5L   '      Columns 5N   "   0  *System.Windows.Controls.DataGridTextColumn  5O   &   ,  &System.Windows.Controls.DataGridColumn    CanUserSort  .  !   CanUserReorder! .  "   CanUserResize" . #   Header$# 	Timestamp??6?     DataGridColumnHeaderStyle7$   HeaderStyle#$ [ 6?   %   
IsReadOnly% . &   Width5  /System.Windows.Controls.DataGridLengthConverter$	& 180 6?   '   MinWidth$	' 160Q?6  1  +System.Windows.Controls.DataGridBoundColumn(   Binding( 69   ?? *	mDateTime+)   ElementStyle) 5P   *   ?? 5Q   .   .?5R   2   ??  ?@^?ToolTip 6[   ?? *	mDateTime+?? 5S   2    ? ??$
 Wrap=?6[   5T   /   5V   '     5W   &     .  ! .  " . $# Product??6?   #$ [ 6?   % . $	& 150 6?   $	' 130Q?6?   ( 69   ?? *
mProduct+) 5X   *   ?? 5Y   .   .?5Z   2   ??  ?@^?ToolTip 6[   ?? *
mProduct+?? 5[   2    ? ??$
 Wrap=?6[   5\   /   5^   '     5_   &     .  ! .  " . $# Suite??6?   #$ [ 6?   % . $	& 245 6?   $	' 200Q?6?   ( 69   ?? *	mTestName+) 5`   *   ?? 5a   .   .?5b   2   ??  ?@^?ToolTip 6[   ?? *	mTestName+?? 5c   2    ? ??$
 Wrap=?6[   5d   /   5f   '     5g   &     .  ! .  " . $# Agent??6?   #$ [ 6?   % . $	& 120 6?   $' 70Q?6?   ( 69   ?? *
mAgentName+) 5h   *   ?? 5i   .   .?5j   2   ??  ?@^?ToolTip 6[   ?? *
mAgentName+?? 5k   2    ? ??$
 Wrap=?6[   5l   /   5n   '     5o   &     .  ! .  " . $# Environment??6?   #$ [ 6?   % . $	& 170 6?   $' 85Q?6  ( 69   ?? *mTestEnvironment+) 5p   *   ?? 5q   .   .?5r   2   ??  ?@^?ToolTip 6[   ?? *mTestEnvironment+?? 5s   2    ? ??$
 Wrap=?6[   5t   /   5v   '     5w   &     .  ! .  " . $# Status??6?   #$ [ 6?   % . $	& 120 6?   $' 80Q?6  ( 69   ?? *mTestStatus+) 5x   *   ?? 5y   .   .?5z   2   ??  ?@^?ToolTip 6[   ?? *mTestStatus+?? 5{   2    ? ??$
 Wrap=?6[   5|   /   5~   '     5   &     .  ! .  " . $# Details??6?   #$ [ 6?   % . $& * 6?   $	' 120Q?6?   ( 69   ?? *
mMessage+) 5?   *   ?? 5?   .   .?5?   2   ??  ?@^?ToolTip 6[   ?? *
mMessage+?? 5?   2    ? ??$
 Wrap=?6[   5?   /   5?   '   5?      ? 5?      $??0,0,1,0q?6#   * J? RenderTransformOrigin$* 0.585,0.208&?64   $??1??6X   $??Stretch=?6e   .+?5?   "   ?? + ?? Orientation$+ 
Horizontal=?6-   $
??Left=?6F   $??5,0,0,0q?6a   .+?5?   &   ?? 
???????
???????$??12??6R   $??12??6^   $	??0.5??6i   6?   ?? 5?   &   $??3??60   ?? 5?   &   $??Center=?60   $??10(?6K   Passed6a   ?? 5?   &   $??2??60   ?? 5?   &   $??Center=?60   $??10(?6K   $??Italic$?6Y   ( ?? 5?   .   ??68   ?? , ?? Path$, Passed?- ?? UpdateSourceTrigger$- PropertyChanged=?. ?? NotifyOnSourceUpdated. . / ?? RelativeSource/ ?? *FindAncestor+0 ?? AncestorType"0 ? )5?   +   ?? 5?   &   $??10??60   ?? 5?   &   
???r???
???????$??12??6N   $??12??6Z   $	??0.5??6e   6}   ?? 5?   &   $??3??60   ?? 5?   &   $??Center=?60   $??10(?6K   Failed6a   ?? 5?   &   $??2??60   ?? 5?   &   $??Center=?60   $??10(?6K   $??Italic$?6Y   ( ?? 5?   .   ??68   ?? $, Failed?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ? )5?   +   ?? 5?   &   $??10??60   ?? 5?   &   
???r???
???????$??12??6N   $??12??6Z   $	??0.5??6e   6}   ?? 5?   &   $??3??60   ?? 5?   &   $??Center=?60   $??10(?6K   Error6`   ?? 5?   &   $??2??60   ?? 5?   &   $??Center=?60   $??10(?6K   $??Italic$?6Y   ( ?? 5?   .   ??68   ?? $, Error?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ? )5?   +   ?? 5?   &   $??10??60   ?? 5?   &   
???r???
???????$??12??6N   $??12??6Z   $	??0.5??6e   6}   ?? 5?   &   $??3??60   ?? 5?   &   $??Center=?60   $??10(?6K   	Warning6b   ?? 5?   &   $??2??60   ?? 5?   &   $??Center=?60   $??10(?6K   $??Italic$?6Y   ( ?? 5?   .   ??68   ?? $, Warning?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ? )5?   +   ?? 5?   &   $??10??60   ?? 5?   &   
???????
???????$??12??6T   $??12??6`   $	??0.5??6k   6?   ?? 5?   &   $??3??60   ?? 5?   &   $??Center=?60   $??10(?6K   	Pending6b   ?? 5?   &   $??2??60   ?? 5?   &   $??Center=?60   $??10(?6K   $??Italic$?6Y   ( ?? 5?   .   ??68   ?? $, Pending?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ? )5?   +   ?? 5?   &   $??10??60   ?? 5?   &   
???????
???????$??12??6Q   $??12??6]   $	??0.5??6h   6?   ?? 5?   &   $??3??60   ?? 5?   &   $??Center=?60   $??10(?6K   	Cancelled6d   ?? 5?   &   $??2??60   ?? 5?   &   $??Center=?60   $??10(?6K   $??Italic$?6Y   ( ?? 5?   .   ??68   ?? $, 	Cancelled?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ? )5?   +   ?? 5?   &   $??10??60   ?? 5?   &   
???????
???????$??12??6Q   $??12??6]   $	??0.5??6h   6?   ?? 5?   &   $??3??60   ?? 5?   &   $??Center=?60   $??10(?6K   Disconnected6g   ?? 5?   &   $??2??60   ?? 5?   &   $??Center=?60   $??10(?6K   $??Italic$?6Y   ( ?? 5?   .   ??68   ?? $, Disconnected?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ? )5?   +   5?   #   ?? 5?   "   $??Right=?6,   $??Center=?6H   $??11(?6c   Log Count: ?? 5?   *   $
??Bold"?6?   
????  ?$??Center=?6  ??64   ?? $, Total?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ?6   ?? 5?   *   $??15??64   5?   '   5?      ?? 5?      ??5?   "   ?? 5?   &   " ??6,   .?5?   *   ??  ? ??$ No logs to display??6A   ?? 5?   *    ? y?$ Visible=?6G   ?? 5?   *    ? ??$ Center=?6P   ?? 5?   *    ? ??$	 Top=?6N   ?? 5?   *    ? ??$ 0,17,0,0q?6C   ?? 5?   *    ? ??$ Italic$?6F   ?? 5?   *    ? ??$ 10(?6E   ?? 5?   *    ? ??
 ??  ?6G    5?   *   ?? 5?   .    6:   ?? $, NoLogsFound?$- PropertyChanged=?. . / ?? *FindAncestor+"0 ?	 False6?   .a?5?   2   ??  ? y?$ Hidden=?6O   5?   /   5?   '   5?      5?      5?      5?      5?      5?      5?      5?      