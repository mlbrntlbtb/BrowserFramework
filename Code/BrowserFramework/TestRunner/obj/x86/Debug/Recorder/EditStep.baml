   M S B A M L   `   `   `  ????  
TestRunner"    TestRunner.Recorder.EditStep   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $	??320??5   	   $	??490??6     ? WindowStyle$  
ToolWindow=?6;   .??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/splash.png??6    j? Stretch$ UniformToFill=?6T    ? ColumnDefinitions 5	   
   ?? 5
      $??10 ?6   ?? 5      $	??180 ?6   ?? 5      $??10 ?6   ?? 5      ?? 5      $??10 ?6    ? RowDefinitions 5   
   ?? 5      $??10 ?6   ?? 5      ?? 5      $??10 ?6   ?? 5      $??30 ?6   ?? 5      $??10 ?6   .+?5   
   ?? $??1??6   $??1??6%   ???	#FF90D2FF.@?5      ?? $y?Hidden=?6   .??5      ?  5      ?? 5      $??22 ?6(   ?? 5      ?? 5      $??22 ?6(   ?? 5      ?? 5      $??22 ?6(   ?? 5       ?? 5!      $??15 ?6(   .+?5#      ?? $??0??6   $??11(?6)   .??67   Screen6?   ?? 5$      -   5         ??Name$ 	txtScreen??5$      $??1??61   $??Stretch=?6W   $??Stretch=?6s    ?? TextWrapping$
 Wrap=?6?   {?.  ??6>   ?? *	mScreen+6?   ?? 5%      $??2??6   $??11(?6)   .??67   	Control6@   ?? 5&      -   5        $ 
txtControl??5&      $??3??62   $??Stretch=?6Y   $??Stretch=?6u   $
 Wrap=?6?   {?.  ??6?   ?? *
mControl+6?   ?? 5'      $??4??6   $??11(?6)   .??67   	Keyword6@   ?? 5(      -   5        $ 
txtKeyword??5(      $??5??62   $??Stretch=?6Y   $??Stretch=?6u   $
 Wrap=?6?   {?.  ??6?   ?? *
mKeyword+6?   5)      5*      5+      ?? 5-   
   $??3??6   $??1??6#   $??
Parameters??60   .??5.      ? .+?5/      &   System.Windows.Controls.DataGrid  -   5          Name$ dgParameters??5/         DataGridStyle3#??[  6I      SelectionUnit$
 Cell=?6q   	   SelectionMode$	 Single=?6?   $??25??50      $	??125??6.   $
??Auto??6>   $??1q?6L   $??Center=?6`   "
   VerticalScrollBarVisibility$

 Auto=?51      $   HorizontalScrollBarVisibility$ Hidden=?6B   ??5/   1   ??    Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    CanUserSort .     CanUserReorder .     CanUserResize .     Header$ 
Parameters??54   -     DataGridColumnHeaderStyle0   HeaderStyle# [ 6A      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6|   1  +System.Windows.Controls.DataGridBoundColumn   Binding 53   ?   ?? *mParameterName+   	CellStyle 55      ?? 56   "    ?? 
TargetType*  $System.Windows.Controls.DataGridCell"  6(   .?57   &   ??  ?? Property ? ?? ?? Value$ 25??6B   ?? 58   &    ? ??$ 11(?6A   ?? 59   &    ? ??$
 0.25q?6H   ?? 5:   &    ? ??$
 Auto??6>   ?? 5;   &    ? ??# Z?6C   ?? 5<   &    ? ??
 ???? 6D   ?? 5=   &    ? ??$ Center=?6Q   ?? 5>   &    ? ?? .  6B    ?? Triggers 5?   &   P? 5@   *    P? Property ?@ 
IsSelected P? Value . . ?5A   .   ??  ? ??
 ?   ?6K   5B   +   5D   #      ElementStyle 5F      ?? 5G   "   " ??6(   .?5H   &   ??  ? ??$ Center=?6J   ?? 5I   &    ? ??$ 5,0,0,0q?6?   5J   #   5L        5M       .   .   .  $ Values??6?     DataGridColumnHeaderStyle1# [ 5N   -   $	 150 6g    5M   A   ?? *mValue+ 5O      ?? 5P   "   "  6(   .?5Q   &   -   5         5R   &   P? 5S   *    ?@ 
IsSelected . . ?5T   .   ??  ? ??
 ?????6K   ?? 5U   .    ? ??
 ???? 6L   5V   +   5X   #    5Z      ?? 5[   "   " ??6(   .?5\   &   ??  ? ??$
 Wrap=?6E   ?? 5]   &    ? ??$ Center=?6J   ?? 5^   &    ? ??$	 125??6B   ?? 5_   &    ? ??$ 5,0,0,0q?6?   5`   #      EditingElementStyle 5b      ?? 5c   "   " ??6(   .?5d   &   ??  ?@??TextWrapping$
 Wrap=?6E   ?? 5e   &    ?@??AcceptsReturn . 6F   ?? 5f   &    ? ??$	 125??6B   ?? 5g   &   # ?@??VerticalScrollBarVisibility$
 Auto=?6T   5h   #   5j      5l      5m      5n      ? 5o   
   $??1??6   $??3??6   $??3??6,    5p      ?? 5q      ?? 5r      .+?5t      ?? -   5         ??Name$ btnOK??5t      $??0??6$     ButtonStyle3#??[ 64   $??OK??6Z   $??Right=?6g   6?   ?? 5u      -   5        $ 	btnCancel??5u      $??1??6(   #??[ 68   $??Cancel??6^   $
??Left=?6o   6?   5v      5w      5x      