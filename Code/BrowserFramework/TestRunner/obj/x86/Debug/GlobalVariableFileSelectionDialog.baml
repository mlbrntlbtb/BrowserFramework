   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	   2    ,TestRunner.GlobalVariableFileSelectionDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestRunner     5   	     ? Title$$  Global Variable File Selection??5   	   $	??294??60   $	??332??6=    ? WindowStartupLocation$ CenterOwner=?6I    ? WindowStyle$ 
ToolWindow=?6m    ? 
ResizeMode$ NoResize=?6?   .??5	      ? ??5
   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/splash.png??6    ?? Opacity$
 0.25Q?6T    ? RowDefinitions 5   
   ?? 5      ?? 5      $??49 ?6   .+?5   
   ? $??1??6   $??0,0,0,0q?6    ? ColumnDefinitions 5      ?? 5      $
??221* ?6#   ?? 5      $
??178* ?6#   ?? 5      $	??43* ?6#   .+?5      ?? -   5         ??Name$ btnOK??5      $??0??6$      ButtonStyle4#??[  64   $??Right=?6Z   $??25??6v   $??OK??6?   $??	0,10,2,12q?6?   6?   ?? 5      -   5        $ 	btnCancel??5      $??1??6(   #??[  68   $
??Left=?6^   $??25??6z   $??Cancel??6?   $??	2,10,0,12q?6?   6?   5      ]? 5   
   $??0??6   .+?5      ?? $	??Top=?6   $??
20,15,20,5q?60   $??14(?6D   
?????F?$	??Top=?6i   $
??Wrap=?6~   $2??,Select the global variable file to reference??6?   ?? 5      	 ?? Orientation$	 Vertical=?6   $??	20,0,20,5q?60   $??26??6C   $	??Top=?6O   .+?5      ?? -   5        
 ??Name$
 cboGlobalVarFile??5      ??65   ?? 6M   5      ?? 5      $
??20,0q?6   $	??140??6'   $??Bottom=?64   .+?5       &   System.Windows.Controls.DataGrid  -   5          Name$ dgGlobalVariables??5          RowHeaderWidth$ 0Q?6N   $??Stretch=?6a     DataGridStyle1#??[ 5!   #     DataGridCellStyle7   	CellStyle# [ 6K      SelectionMode$ Single=?5"   #   $   HorizontalScrollBarVisibility$
 Auto=?6:   "   VerticalScrollBarVisibility$ Visible=?6_      RowDetailsVisibilityMode$ 	Collapsed=?5#   $     DataGridRowStyle0   RowStyle# [ 6I      CanUserAddRows .  $??0,0,0.333,0q?5$   #      CanUserResizeColumns . $??Stretch=?6T   ??.  $	??139??6?   ??5    6   ??    Columns 5%      4  .System.Windows.Controls.DataGridTemplateColumn  5&      ,  &System.Windows.Controls.DataGridColumn   CanUserSort .     CanUserReorder .     CanUserResize .    Header$ VarName??6r     DataGridColumnHeaderStyle0   HeaderStyle# [ 6?      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$	 130 6?      CellTemplate 5'      ?? 5(   "   .R?5)   &   ?? $	 
Horizontal=?61   .+?5*   *   ?? $??30??6L   $??Center=?6X   $
??Left=?6z   
????  ???60   ?? *	VarName+5+   +   5,   '   5-   #   5/        50       .   .   . $ Value??6r     DataGridColumnHeaderStyle1# [ 6?    . $ * 6?    51      ?? 52   "   .R?53   &   ?? $	 
Horizontal=?61   .+?54   *   ?? $??30??6J   $??Center=?6V   $
??Left=?6x   
????  ???60   ?? *Value+55   +   56   '   57   #   59      5;      5<      5=      5>      5?      