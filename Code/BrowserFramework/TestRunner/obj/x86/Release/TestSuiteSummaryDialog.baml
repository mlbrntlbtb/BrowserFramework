   M S B A M L   `   `   `  ????   
TestRunner'    !TestRunner.TestSuiteSummaryDialog   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       ? Title$  Test Suite Summary??$	??500??$	??800?? ? 
ResizeMode$ NoResize=?.??? ????  ?? ImageSource$< 6pack://siteoforigin:,,,/Resources/DeltekBackground.png?? j? Stretch$ UniformToFill=? ? ColumnDefinitions ?? $??* ? ? RowDefinitions ?? $??* ?.+???  ?? Orientation$ Vertical=?$??5,5,5,5q?.+??? $
??Left=?$
??Left=?.??Test Suite Summary:&   System.Windows.Controls.DataGrid  -     Name$ dgTestSuiteSummary??$	??400??   RowHeaderWidth$ 0Q?   DataGridStyle1#??[  $	   HorizontalScrollBarVisibility$	 Hidden=?
   SelectionMode$
 Single=?   
IsReadOnly .   DataGridRowStyle1   RowStyle# [   DataGridCellStyle1   	CellStyle# [ ????    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   CanUserSort .     CanUserReorder .     CanUserResize .     Header$ Suite??  DataGridColumnHeaderStyle4   HeaderStyle# [ 1  +System.Windows.Controls.DataGridBoundColumn   Binding ?? *suite+   .   .   .  $ Last Run??  DataGridColumnHeaderStyle3# [  ?? *resultsdirectory+   .   .   .  $ Total??# [  ?? *
counttotal+   .   .   .  $ Passed??# [  ?? *	countpass+   .   .   .  $ Failed??# [  ?? *	countfail+   RowDetailsTemplate ?? .R??? $??3q? ?? Padding$ 3q?
?????F?$??3q? ?? CornerRadius$ 5??._?  $ 0Q?#??[  $
 Single=?$	??700??"   VerticalScrollBarVisibility$ Visible=?$	??200?????? *
testrecs+ ??  ?? 
TargetType*  $System.Windows.Controls.DataGridCell"   ?? Triggers ??  ?? Binding ?? *
teststatus+ ?? Value Not Run.a???  ?? Property ? ?? ?? Value
 ???????  ? ??
 ??  ???  ?? *
teststatus+
 Passed.a???  ? ??
 ???????  ? ??
 ??  ???  ?? *
teststatus+
 Failed.a???  ? ??
 ???????  ? ??
 ??  ?    .   .   .  $ #??  DataGridColumnHeaderStyle0# [  ?? *	testrow+   .   .   .  $ Test Status??  DataGridColumnHeaderStyle1# [  ?? *
teststatus+   .   .   .  $ 	Test Type??# [  ?? *
testtype+   .   .   .  $ Folder??  DataGridColumnHeaderStyle2# [  ?? *folder+   .   .   .  $ Spreadsheet??# [  ?? *spreadsheet+   .   .   .  $ 	Test Name??  DataGridColumnHeaderStyle5# [    MinWidth$	 255Q? ?? *
testname+    ElementStyle  ?? .???  ? ??$
 Wrap=??? -   ! ??Name$! btnCloseSummary?? 	 ButtonStyle2#??[	 $??Right=?$??Close??