   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:TestRunner.DesignerTestRunner.Designer  5   	   %    TestRunner.Designer.TestDetails   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:TestRunner.Designer     5   	     ? Title$  Test Details??5   	   $	??300??6   $	??620??6.   $	??600??6=   $	??620??6J    ? 
ResizeMode$ CanResizeWithGrip=?6V    ? WindowStartupLocation$ CenterOwner=?6v   .??5	      ? ??5
   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T   .+?5   
   ? $??20,15,20,20q?6   $??2??6$    ? RowDefinitions 5      ?? 5      $
??100* ?6    ?? 5      $
??420* ?6    .+?5      ?? $??0??6   $??0,0,0,0q?6'   .@?5      ?? 
???   ?$y?	Collapsed=?6-   .??5      ?  5      ?? 5      ?? 5      $??2* ?6,   .+?5      ? $??0,0,-0.333,0q?6   .+?5      ?? $??0??6)    ?? Orientation$ 
Horizontal=?69   .+?5   "   ?? -   5         ??Name$ lblTest??5   (      LabelStyle1#??[  69   $??
Test Name:??6^   $
??Left=?6s   $??Center=?6?   $??25??6?   $??70??6?   ?? 5   "   -   5        $ lblTestName??5   (   #??[  6>   $?? ??6c   $
??Left=?6n   $??Center=?6?   $??25??6?   $	??372??6?   5       5!      ? 5#      $??1??6   $??0,0,-0.333,0q?6,    ? ColumnDefinitions 5$      ?? 5%   "   $??50 ?63   ?? 5&   "   $??* ?63   ?? 5'   "   $??5 ?63   .+?5)      ?? -   5        $ 
lblPathTxt??5)   4   $??0??6$   $??Path:??6H   $??3??6X   ?? 5*      -   5        	 ??Name$	 tbxFolderPath??5*   6   $??1??6&   
 ?? 
IsReadOnly
 . $ ?? HorizontalScrollBarVisibility$ Disabled=?6_   " ?? VerticalScrollBarVisibility$
 Auto=?6?   ??.  ?? TextWrapping$
 Wrap=?6?   $??4.4,4,0.6,4q?6?   .??6?   ??5+   "   ?? 5,   &    ?? EndPoint$
 0,20&?6:    ? MappingMode$ Absolute=?6J    ?? 
StartPoint$	 0,0&?6a   .??5-   *   ?  ? Color$ 	#FFABADB3??67    ? Offset$
 0.05Q?6I   ? 5.   *   $ 	#FFE2E3EA??67   $
 0.07Q?6I   ? 5/   *   $ 	#FF828790??67   $ 1Q?6I   50   '   .??51   7   52      53      54      55      56      ?? 58      $??1??6   $??0,3,0,0q?6'   .@?59      ?? $y?	Collapsed=?6   .??5:      ?  5;      ?? 5<      $	??40* ?6,   ?? 5=      $
??469* ?6,   .+?5?      ? $??0??6   $??0,0,0,0q?6,    5@      ?? 5A   "   $??90 ?63   ?? 5B   "   $??* ?63   ?? 5C   "   $	??110 ?63   .+?5E      ?? -   5        $ 
lblMatches??5E   $   $??0??68   #??[  6H   $??Steps Matched:??6m   $
??Left=?6?   $??25??6?   $??95??6?   $??0,5,0,0q?6?   ?? 5F      -   5        $ lblMatchCount??5F   $   $??1??6;   #??[  6K   $?? ??6p   $??25??6{   $	??105??6?   $
??Left=?6?   $??0,5,0,0q?6?   ?? 5H      $??1??6)   $ 
Horizontal=?69   $??Right=?6R   $??2??6o   $??0,5,10,-0.4q?6?   .+?5I   "   ?? -   5        $ lblMatch??5I   (   #??[  6;   $??Match??6`   $
??Left=?6p   $	??Top=?6?   $??25??6?   $??44??6?   ?? 5J   "   $
??Left=?6,   $??12??6G   $??0,7,0,0q?6S   
???   ?$	??Top=?6s   $??12??6?   ???	#FF5BE2756?   5K      5L      &   System.Windows.Controls.DataGrid  5N      -   5          Name$ dgStepResults??5N   #   $??1??6:     DataGridStyle1#??[ 6G      RowHeaderWidth$ 0Q?6?   $   HorizontalScrollBarVisibility$
 Auto=?5O   "   "   VerticalScrollBarVisibility$ Visible=?6G      SelectionMode$ Single=?5P   "      
IsReadOnly .    CanUserDeleteRows .     CanUserAddRows .    DataGridRowStyle1   RowStyle# [ 5R   "     DataGridCellStyle5   	CellStyle# [ 6P    %? IsVirtualizing .     CanUserResizeColumns .  J? RenderTransformOrigin$ 0.5,0.5&?6?   $??0,0,-0.333,-0.333q?6?   $??2??6   ??5N   o   ?? z?5S      V? 5T   "   .~?5U   &   ?? ?? 5V   &     ?? AngleY$  -0.114Q?64   ?? 5W   &   T? 5X   &   ! T? Y$! 0.531Q?69   5Y   #   "   Columns" 5[      0  *System.Windows.Controls.DataGridTextColumn  5\   "   ,  &System.Windows.Controls.DataGridColumn#   CanUserSort# .  $   CanUserReorder$ .  %   CanUserResize% . &   Header$
& Step??6?     DataGridColumnHeaderStyle0'   HeaderStyle#' [ 6?   (   Width5  /System.Windows.Controls.DataGridLengthConverter$( 40 6?   1  +System.Windows.Controls.DataGridBoundColumn)   Binding) 65   ?? *mStepNumber+6?     5]   "   # .  $ .  % . $& Screen??6?   #' [ 6?   $	( 120 6?   ) 65   ?? *	mScreen+6?     5^   "   # .  $ .  % . $& Control??6?   #' [ 6?   $	( 120 6?   ) 65   ?? *
mControl+6?     5_   "   # .  $ .  % . $& Keyword??6?   #' [ 6?   $	( 120 6?   ) 65   ?? *
mKeyword+6?     5`   "   # .  $ .  % . $& 
Parameters??6?   #' [ 6?   $	( 160 6?   ) 65   ?? *mParameterString+6?   5b      5c      5d      5e      5f      5g      5h      