   M S B A M L   `   `   `  ????  
TestRunner!    TestRunner.ManageSuiteLinks   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          ? Title$  Manage Suite Links??5   	   $	??500??6$   $	??500??61    ? WindowStyle$ 
ToolWindow=?6=   ???	#FF90D2FF ? WindowStartupLocation$ CenterOwner=?5        ? 
ResizeMode$ NoResize=?6D   .??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T    ? RowDefinitions 5
   
   ?? 5      $??5 ?6   ?? 5      $??40 ?6   ?? 5      ?? 5      $??40 ?6   ?? 5      $??5 ?6    ? ColumnDefinitions 5   
   ?? 5      $??5 ?6   ?? 5      ?? 5      $??5 ?6   .+?5   
   c? $??1??6   $??1??6"    `? IsLocked . $??3??6K   
?????? .@?5      ?? -   5        	 ??Name$	 	btnAddRow??5   K       TestEditorToolBarButtonStyle#??[  6   
 ? ToolTip$	
 Add??6^   .??5      ??  ?? Orientation$ Vertical=?6   .+?5      ?? $/??)pack://siteoforigin:,,,/Resources/add.png??6   $??16??6O   $??16??6[   ?? 5      $??Center=?6    $??9(?6=   $
??Wrap=?6J   $??SemiBold"?6^   Add6y   5      5      ?? 5      -   5        $	 
btnEditRow??5   K   #??[  6   $

 Edit??6_   .??5      ?? $ Vertical=?6   .+?5       ?? $8??2pack://siteoforigin:,,,/Resources/documentedit.png??6   $??16??6X   $??16??6d   ?? 5!      $??Center=?6    $??9(?6=   $
??Wrap=?6J   $??SemiBold"?6^   Edit6z   5"      5#      ?? 5$      -   5        $	 	btnMoveUp??5$   K   #??[  6   $
 Move Row Up??6^   .??5%      ?? $ Vertical=?6   .+?5&      ?? $4??.pack://siteoforigin:,,,/Resources/arrow_up.png??6   $??12??6T   $??12??6`   $??0,0,0,4q?6k   ?? 5'      $??Center=?6    $??9(?6=   $
??Wrap=?6J   $??SemiBold"?6^   Up6x   5(      5)      ?? 5*      -   5        $	 btnMoveDown??5*   K   #??[  6   $
 Move Row Down??6`   .??5+      ?? $ Vertical=?6   .+?5,      ?? $6??0pack://siteoforigin:,,,/Resources/arrow_down.png??6   $??12??6V   $??12??6b   $??0,0,0,4q?6m   ?? 5-      $??Center=?6    $??9(?6=   $
??Wrap=?6J   $??SemiBold"?6^   Down6z   5.      5/      ?? 50      -   5        $	 btnDeleteRow??50   K   #??[  6   $
 Delete??6a   .??51      ?? $ Vertical=?6   .+?52      ?? $2??,pack://siteoforigin:,,,/Resources/delete.ico??6   $??16??6R   $??16??6^   ?? 53      $??Center=?6    $??9(?6=   $
??Wrap=?6J   $??SemiBold"?6^   Delete6|   54      55      56      &   System.Windows.Controls.DataGrid  57   
   -   5          Name$ dgLinks??57      $??1??6$   $??2??64     DataGridStyle1#??[ 6A     DataGridCellStyle8   	CellStyle# [ 6i      SelectionMode$ Single=?58         
IsReadOnly .    HeadersVisibility$ Column=?6<   "   VerticalScrollBarVisibility$ Visible=?6W    J? 	AllowDrop .    RowStyle 5:      ?? 5;       ?? 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .?5<      ??  ?? Property ?@?ToolTip ?? Value 5=      ?? 5>      ??6(   ?? *
LinkPath+5@      ?? 5A       ? ??$
 0.25q?68    ?? Triggers 5B      P? 5C       P? Property ?@ 
IsSelected P? Value . . ?5D      ??  ? ??
 ??+??6<   ?? 5E       ? ??$ 2q?6@   5F      5H         Columns 5J      4  .System.Windows.Controls.DataGridTemplateColumn  5K      ,  &System.Windows.Controls.DataGridColumn   Header$ Display Name??6)     DataGridColumnHeaderStyle4   HeaderStyle# [ 6?      CellTemplate 5L      ?? 5M      .R?5N      ?? $??CharacterEllipsis=?6E   ??6(   ?? *DisplayName+6E   5O      5Q        5R      $ 	Link Path??6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6<   # [ 6F    5S      ?? 5T      .R?5U      ?? $??CharacterEllipsis=?6B   ??6(   ?? *
LinkPath+6B   5V      5X      5Z      ?? 5[   
   $??1??6   $??3??6%   $ 
Horizontal=?62   $??Right=?6K   $??2??6g   .+?5\      ?? -   5        $	 btnOK??5\        ButtonStyle3#??[ 6$   $??0,0,5,0q?6J   .??6o   OK6s   ?? 5]      -	   5        $	 	btnCancel??5]      #??[ 6(   $??0,0,20,0q?6N   .??6x   Cancel6?   5^      5_      5`      