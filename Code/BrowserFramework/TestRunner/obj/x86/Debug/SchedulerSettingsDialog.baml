   M S B A M L   `   `   `  ????  
TestRunner(    "TestRunner.SchedulerSettingsDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          ? Title$  Scheduler Settings??5   	   $	??400??6$   $	??380??61    ? WindowStartupLocation$ CenterOwner=?6T    ? WindowStyle$ 
ToolWindow=?6x    ? 
ResizeMode$ NoResize=?6?    ? DataContext 5   	   ??  ?? RelativeSource ?? *self+.??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T   .+?5
   
   ]? ??. .+?5      ??  ?? Orientation$ 
Horizontal=?6   $??1??62   $??Right=?6?   $??Bottom=?6[   $??45??6s   $??0,0,10,0q?6   .+?5      ?? -   5        	 ??Name$	 btnOk??5         ButtonStyle2#??[  6(   $??OK??6N   6[   ?? 5      -   5        $	 	btnCancel??5      #??[  6,   $??Cancel??6R   6c   5      ?? 5      -   5        
 ??Name$
 tabSettings??5      $??
10,10,10,0q?6.   ???	#FF90D2FF.@?5      ?? -   5         ??Name$ tpgEmail??5      $??Email Notification??6,   .??5      ?? $??Center=?6!   $??0q?6>   $	??330??6I   .+?5      ?? $??0,20,0,0q?6$   $??11(?67   $
??Wrap=?6E   =;Send email notification report to the following recipients:6?   ?? 5      -   5         ??Name$ txtMailAddresses??5   "   $??0,10,0,0q?6<    ?? TextWrapping$
 Wrap=?6N    ?? AcceptsReturn . " ?? VerticalScrollBarVisibility$
 Auto=?6w   $	??120??6?   6?   ?? 5      $??0,1,0,0q?6$   $??10(?65   $??Italic$?6C   ?=* Separate each email address with a semicolon (;) No spaces.6?   ?? 5      -   5        $"??chkSendEmailOnExecutionStart??5   #   $??10q?6G   $??45??6?   .??5      ??  ?? TextWrapping$
 Wrap=?6)    ?? FontSize$ 11(?6=   .??6K   KISend email notification upon start of the scheduled test suite execution.5      5      5      5      ?? 5      -   5        $ 
tpgScripts??5      $??External Scripts??6.   .??5      ]? ??. .+?5      ?? $??10q?6%   $	??Top=?61   $??Stretch=?6F   .+?5      ?? -	   5         ??Name$ cbScriptType??5   '   $??Stretch=?6?   ??6=   ?? 6?   ?? 5       -
   5         ??Name$ tbScriptDesc??5    G   $??0,1,0,0q?6(   $??10(?69   6G   5!      ?? 5"      .@?5#      ?? $??
ExtScripts??6&   $y?	Collapsed=?6:   .??5$   "   ?? -   5         ??Name$ buttonsPane??5$   -   $??Stretch=?6B   }?. $	??Top=?6t   
???????.+?5%   &   c? -   5        $
??Left=?5%   .    c? IsOverflowOpen .   `? IsLocked . $
??Left=?6?   
?????? $??
-5,0,-18,0q?6?   $??5,0,15,0q?6?    c? OverflowMode$ Never=?5&   [   .@?5'   *   ?? -   5        $	 btnNewScript??5'   g      TestEditorToolBarButtonStyle#??[ 61    ? ToolTip$	 New??6}   .??5(   .   ?? $ Vertical=?69   .+?5)   2   ?? $/??)pack://siteoforigin:,,,/Resources/add.png??68   $??12??6k   $??12??6w   ?? 5*   2   $??Center=?6<   $??9(?6Y   $
??Wrap=?6f   $??SemiBold"?6z   New6?   5+   /   5,   +   ?? 5-   *   -   5        $	 btnEditScript??5-   g   #??[ 61   $
 Edit??6~   .??5.   .   ?? $ Vertical=?69   .+?5/   2   ?? $0??*pack://siteoforigin:,,,/Resources/edit.png??68   $??12??6l   $??12??6x   ?? 50   2   $??Center=?6<   $??9(?6Y   $
??Wrap=?6f   $??SemiBold"?6z   Edit6?   51   /   52   +   ?? 53   *   -   5        $	 btnDeleteScript??53   g   #??[ 61   $ Delete??6?   {?.  .??54   .   ?? $ Vertical=?69   .+?55   2   ?? $9??3pack://siteoforigin:,,,/Resources/button_cancel.ico??68   $??12??6u   $??12??6?   ?? 56   2   $??Center=?6<   $??9(?6Y   $
??Wrap=?6f   $??SemiBold"?6z   Delete6?   57   /   58   +   ?? 59   *   -   5        $	 	btnMoveUp??59   g   #??[ 61   $ Up??6z   {?.  .??5:   .   ?? $ Vertical=?69   .+?5;   2   ?? $4??.pack://siteoforigin:,,,/Resources/arrow_up.png??68   $??8??6p   $??8??6{   $??0,1,0,2q?6?   ?? 5<   2   $??Center=?6<   $??9(?6Y   $
??Wrap=?6f   $??SemiBold"?6z   Up6?   5=   /   5>   +   ?? 5?   *   -   5        $	 btnMoveDown??5?   g   #??[ 61   $
 Down??6|   {?.  .??5@   .   ?? $ Vertical=?69   .+?5A   2   ?? $6??0pack://siteoforigin:,,,/Resources/arrow_down.png??68   $??0,1,0,2q?6r   $??8??6?   $??8??6?   ?? 5B   2   $??Center=?6<   $??9(?6Y   $
??Wrap=?6f   $??SemiBold"?6z   Down6?   5C   /   5D   +   5E   '   &   System.Windows.Controls.DataGrid  5F   &   -   5          Name$ dgExtScripts??5F   /   "   VerticalScrollBarVisibility$ Disabled=?6]     DataGridStyle1#??[ 5G   "     DataGridRowStyle1   RowStyle# [ 6J     DataGridCellStyle5   	CellStyle# [ 6x   $   HorizontalScrollBarVisibility$ Hidden=?5H   "      SelectionMode$ Extended=?6I   $??Stretch=?6c   $
??Auto??6?      RowHeaderWidth$ 0Q?6?       
IsReadOnly  . ???	#FF90D2FF! %? IsVirtualizing! .  ??5F   E   ?? "   Columns" 5J   *   0  *System.Windows.Controls.DataGridTextColumn  5K   .   ,  &System.Windows.Controls.DataGridColumn#   CanUserSort# .  $   CanUserReorder$ .  %   CanUserResize% .  &   Header$
& Name??6?     DataGridColumnHeaderStyle0'   HeaderStyle#' [ 6?   (   Width5  /System.Windows.Controls.DataGridLengthConverter$( * 6?   1  +System.Windows.Controls.DataGridBoundColumn)   Binding) 6A   ?? *Name+6?   5M   '   5N   #   5O      5P      5Q      5R      5S      5T      5U      5V      