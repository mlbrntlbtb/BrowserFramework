   M S B A M L   `   `   `  ????  
TestRunner'    !TestRunner.TestSuiteSummaryDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     ? Title$  Test Suite Summary??5   	   $	??500??6$   $	??800??61    ? 
ResizeMode$ NoResize=?6=   .??5      ? ??5   
   ?? 5       ?? ImageSource$< 6pack://siteoforigin:,,,/Resources/DeltekBackground.png??6    j? Stretch$ UniformToFill=?6^    ? ColumnDefinitions 5	   
   ?? 5
      $??* ?6    ? RowDefinitions 5   
   ?? 5      $??* ?6   .+?5   
   ??  ?? Orientation$ Vertical=?6   $??5,5,5,5q?6,   .+?5      ?? $
??Left=?6   $
??Left=?6/   .??6Q   Test Suite Summary:6f   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgTestSuiteSummary??5   $   $	??400??6      RowHeaderWidth$ 0Q?6X      DataGridStyle1#??[  5      $	   HorizontalScrollBarVisibility$	 Hidden=?6?   
   SelectionMode$
 Single=?6f      
IsReadOnly .   DataGridRowStyle1   RowStyle# [ 6?     DataGridCellStyle1   	CellStyle# [ 6?   ??5   @   ??    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   CanUserSort .     CanUserReorder .     CanUserResize .     Header$ Suite??6?     DataGridColumnHeaderStyle4   HeaderStyle# [ 6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ?? *suite+6?     5       .   .   .  $ Last Run??6?     DataGridColumnHeaderStyle3# [ 6?    6)   ?? *resultsdirectory+6?     5       .   .   .  $ Total??6?   # [ 6?    6)   ?? *
counttotal+6?     5       .   .   .  $ Passed??6?   # [ 6?    6)   ?? *	countpass+6?     5       .   .   .  $ Failed??6?   # [ 6?    6)   ?? *	countfail+6?      RowDetailsTemplate 5      ?? 5      .R?5      ?? $??3q?6!    ?? Padding$ 3q?6,   
?????F?$??3q?6P    ?? CornerRadius$ 5??6d   ._?5        $ 0Q?5   3   #??[  6F   $
 Single=?5   3   $	??700??5    3   "   VerticalScrollBarVisibility$ Visible=?5!   3   $	??200??5"   3   ??5   '   ?? *
testrecs+ 5$   "   ?? 5%   &    ?? 
TargetType*  $System.Windows.Controls.DataGridCell"  6,    ?? Triggers 5&   *   ?? 5'   .    ?? Binding 6:   ?? *
teststatus+ ?? Value Not Run6Y   .a?5(   2   ??  ?? Property ? ?? ?? Value
 ?????6O   ?? 5)   2    ? ??
 ??  ?6O   5*   /   ?? 5+   .    6:   ?? *
teststatus+
 Passed6Y   .a?5,   2   ??  ? ??
 ?????6O   ?? 5-   2    ? ??
 ??  ?6O   5.   /   ?? 5/   .    6:   ?? *
teststatus+
 Failed6Y   .a?50   2   ??  ? ??
 ?????6O   ?? 51   2    ? ??
 ??  ?6O   52   /   54   '    56   "     57   &    .   .   .  $ #??6?     DataGridColumnHeaderStyle0# [ 6?    69   ?? *	testrow+6?     58   &    .   .   .  $ Test Status??6?     DataGridColumnHeaderStyle1# [ 6?    69   ?? *
teststatus+6?     59   &    .   .   .  $ 	Test Type??6?   # [ 6?    69   ?? *
testtype+6?     5:   &    .   .   .  $ Folder??6?     DataGridColumnHeaderStyle2# [ 6?    69   ?? *folder+6?     5;   &    .   .   .  $ Spreadsheet??6?   # [ 6?    69   ?? *spreadsheet+6?     5<   &    .   .   .  $ 	Test Name??6?     DataGridColumnHeaderStyle5# [ 6?      MinWidth$	 255Q?6?    69   ?? *
testname+    ElementStyle  5=   *   ?? 5>   .   .?5?   2   ??  ? ??$
 Wrap=?6[   5@   /   5B   '   5D      5E      5F      5H      ?? 5I      -   5        ! ??Name$! btnCloseSummary??5I       	 ButtonStyle2#??[	 6.   $??Right=?6T   $??Close??6p   6?   5J      5K      5L      