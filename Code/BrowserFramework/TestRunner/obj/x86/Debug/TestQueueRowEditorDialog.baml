   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	   )    #TestRunner.TestQueueRowEditorDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestRunner     5   	   -   5          ? Title$  
Row Editor??5   	   $??77??6   $	??380??6(   $??77??64   $??77??6C   $	??970??6R   $	??380??6a    ? WindowStartupLocation$ CenterOwner=?6p    ? WindowStyle$ 
ToolWindow=?6?    ? 	Resources 5	         ColHeaderStyle ?? 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader ?? Property ?? Value  ColHeaderStyleCentered  RowStyle)  #System.Windows.Controls.DataGridRow ?? Triggers P? Property	 P? Value  	CellStyle*  $System.Windows.Controls.DataGridCell  CellStyleResults??%  &	        &	 ?     &	 ~    &	 R    &	 ?    ?? 5
   
   ColHeaderStyle??"  6?   .?5      ??  ? ??
 ??  ?6+   ?? 5       ? ??
 ?????6+   ?? 5       ? ??$ 12(?6)   5      ?? 5   
   ColHeaderStyleCentered??"  6O   .?5      ??  ? ??
 ??  ?6+   ?? 5       ? ??
 ?????6+   ?? 5       ? ??$ 12(?6)   ?? 5       ? ??$ Center=?6;   5      ?? 5   
   "  6!   .?5      ??  ? ??$
 0.25q?60    5      P? 5       ?@ 
IsSelected	 . . ?5      ??  ? ??
 ??+??64   ?? 5       ? ??$ 2q?68   5      5      ?? 5   
   "  6"   .?5      ??  ? ??$
 0.25q?60   ?? 5        ? ??
 ?????6+   ?? 5!       ? ??
 ??  ?6+   5"      ?? 5#   
   "  6)   .?5$      ??  ? ??$
 0.25q?60    5%      P? 5&       ?@ 
IsSelected	 . . ?5'      ??  ? ??
 ?????63   ?? 5(       ? ??
 ??  ?63   ?? 5)       ? ??$ 
ExtraBlack"?63   5*      5,      5-      .??5.      ? ??5/   
   ?? 50      
 ?? ImageSource$2
 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T   6n   .+?52   
   ]? ??. .+?53      &   System.Windows.Controls.DataGrid  -   5          Name$ dgRowEditor??53      $
??Auto??6,   $??0??69   $	??Top=?6F     DataGridStyle1#??[ 6[      ColumnHeaderStyle# [  54   '      CanUserAddRows .     	CellStyle# [ 55   >   $y?Visible=?6e      CanUserDeleteRows .  $   HorizontalScrollBarVisibility$ Disabled=?56   A   ??54   [   ??    RowStyle 57      ?? 58      "  6   .?59      ??  ? ??$ 27.75??63   ?? 5:       ? ??$
 0.25q?6<    5;      P? 5<       ?@ 
IsSelected	 . . ?5=   "   ??  ? ??
 ??+??6@   ?? 5>   "    ? ??$ 2q?6D   5?      5A         Columns 5C      4  .System.Windows.Controls.DataGridTemplateColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$ Execute??6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 90 6>      MaxWidth$ 90Q?6I      MinWidth$ 90Q?6W      CellTemplate 5E      ?? 5F      .R?5G   "   ??  ?? Orientation$ 
Horizontal=?6-   .+?5H   &   ??  ?? Padding$ 3q?60   ??6<   ?? *	execute+5I   #   5J         CellEditingTemplate 5L      ?? 5M      .R?5N   "   ?? ??6+   ?? *ExecuteList+ ?? RelativeSource ??  ?? AncestorType" ? ?? UpdateSourceTrigger$ PropertyChanged=? ?? SelectedItem 5O   +   ?? *	execute+  ?? NotifyOnSourceUpdated  . $ PropertyChanged=???5P   &   5  -System.Windows.Controls.ItemContainerTemplate?  5Q   *   .R?5R   .   ?? $ 
Horizontal=?69   .+?5S   2   ?? $ 
3, 0, 0, 0q?6M   ??6<   ?? 6M   5T   /   5U   +   5W   #   5X      5Z        5[      $ Environment??6-   $ * 6B   $	 100Q?6L    5\      ?? 5]      .R?5^   "   ?? $ 
Horizontal=?6-   .+?5_   &   ?? $ 3q?60   ??6<   ?? *environment+5`   #   5a       5c      ?? 5d      .R?5e   "   ?? ??6+   ?? *EnvironmentList+ ?? " ?$ PropertyChanged=? 5f   +   ?? *environment+  . $ PropertyChanged=???5g   &     5h   *   .R?5i   .   ?? $ 
Horizontal=?69   .+?5j   2   ?? $ 
3, 0, 0, 0q?6M   ??6<   ?? 6M   5k   /   5l   +   5n   #   5o      5q        5r      $ Browser??6-   $	 100 6>   $	 100Q?6J    5s      ?? 5t      .R?5u   "   ?? $ 
Horizontal=?6-   .+?5v   &   ?? $ 3q?60   ??6<   ?? *Browser.Name+5w   #   5x       5z      ?? 5{      .R?5|   "   ?? ! ?? SelectedValuePath$
! Name??5   +   ??5|   +   ?? *BrowserList+ ?? " ?$ PropertyChanged=? 5}   +   ?? *	Browser+  . $ PropertyChanged=?" ?? SelectedValue" 5~   +   ?? *Browser.Name+# ?? Mode$# OneWay=?$ ?? 
GroupStyle$ 5?   &   (	  "System.Windows.Controls.GroupStyle	  5?   *   % 	  HeaderTemplate% 5?   .   ?? 5?   2   .R?5?   6   ?? $??	ExtraBold"?6V   {?.  ??6@   ?? *Name+6m   5?   3   5?   +   ??5?   &   ?? 5?   *   .R?5?   .   ?? ??68   ?? *Name+5?   +   5?   #   5?      5?        5?      $ 
Keep Open???6-   $
 Auto 6A   $ 70Q?6N   $ 70Q?6\    5?      ?? 5?      .R?5?   "   ?? & d? 	IsChecked& 6+   ?? *
keepopen+$# TwoWay=?$ PropertyChanged=?  . ??5?   &   ?? 5?   *   " ??60   .?5?   .   ??  ? ??$ Center=?6T   ?? 5?   .    ? ??$ Center=?6R   5?   +   5?   #   5?      5?      5?      5?      5?      5?      