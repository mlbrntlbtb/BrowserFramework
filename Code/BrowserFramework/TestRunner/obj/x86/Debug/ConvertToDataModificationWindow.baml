   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	   5clr-namespace:TestRunner.CommonTestRunner.Common  5   	   9!clr-namespace:TestRunner.ControlsTestRunner.Controls  5   	   0    *TestRunner.ConvertToDataModificationWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'trclr-namespace:TestRunner     5   	   1localclr-namespace:TestRunner.Common     5   	   6controls!clr-namespace:TestRunner.Controls     5   	   -   5          ? Title$"  Select Parameters to Convert??5   	   $	??550??6.   $
??1280??6>   $
??1280??6N   $	??550??6[    ? WindowStartupLocation$ CenterOwner=?6h    ? ShowInTaskbar .  ? 	Resources 5	      +   %TestRunner.ParameterContainsConverter   	paramConv??%   &	          5
   
   6(   5      .??5      ? ??5   
   ?? 5       ?? ImageSource$< 6pack://siteoforigin:,,,/Resources/DeltekBackground.png??6    j? Stretch$ UniformToFill=?6^   .+?5   
   ? -   5         ?Name$ grdMainGrid??5      .+?5      ]? ??. .+?5      ??  ?? Orientation$ 
Horizontal=?6   $??1??66   $??Center=?6C   $??Bottom=?6`   $??45??6x   $??	0,-20,0,0q?6?   .+?5      ?? -   5         ??Name$ btnConvertModifiedData??5        ButtonStyle2#??[ 6=   $??Convert??6c   6u   ?? 5      -   5        $ 	btnCancel??5      #??[ 60   $??Cancel??6V   $??	2,12,2,13q?6g   6z   5      ?? 5      -   5        	 ??Name$	 panelMainRight??5   4   $ Vertical=?6   $??1??6L   $??20,50,20,21q?6\   $
??Left=?6q   $??2??6?   $??Right=?6?   $??Stretch=?6?   .+?5      ?? -   5        $	 panelmMainRightBottom??5   8   $ Vertical=?6!   .+?5      ?? -   5        
 ??Name$
 	lblSelect??5       $$??Choose parameters to convert: ??63   $??0,0,0,0q?6\   $??SemiBold"?6v   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgParameters??5   #     DataGridStyle3#??[ 6Q      SelectionUnit$
 Cell=?6y      SelectionMode$ Single=?6?   $??25??5      $	??128??6.   $
??Auto??6>   $??1q?6L   "   VerticalScrollBarVisibility$
 Auto=?5      $	??250??6B   ??5   9   ??    Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5   "   ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    CanUserSort .     CanUserReorder .     CanUserResize .     Header$" Parameter Conversion Setting??6?     DataGridColumnHeaderStyle1   HeaderStyle# [ 6?      Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 6?      	CellStyle 5   &   ?? 5   *    ?? 
TargetType*  $System.Windows.Controls.DataGridCell"  60    ?? Triggers 5    .   P? 5!   2    P? Property ?@ 
IsSelected P? Value . . ?5"   6   ??  ?? Property ? ?? ?? Value
 ?????6S   5#   3   .?5%   .   ??  ? ?? .  6J   5&   +      CellTemplate 5(   &   ?? 5)   *   .R?5*   .   ? .+?5+   2   ?? -	   5        $??0??5.   C   $??1??6S   {?5+   ;   ??  ?? Path$ mValue?  ?? 	Converter#  [  ! ?? Mode$! OneTime=?" d? 	IsChecked" 5,   4   ?? *mParamConversionSetting+$! TwoWay=?# ?? UpdateSourceTrigger$# PropertyChanged=?$ ?? NotifyOnSourceUpdated$ . ??5/   :   ?? 50   :   " ??6@   .?51   >   ??  ? ??$ Center=?6b   ?? 52   >    ? ??$ Center=?6d   ?? 53   >    ? ??$ 0,3,0,0q?6W   ?? 54   >    ? ??$ 11(?6Y   ?? 55   >    ? ??$
 0.25q?6`   ?? 56   >    ? ??
 ?????6[   ?? 57   >    ? ??$ Center=?6i   ?? 58   >    ? ?? .  6Z   59   ;   5;   3   5<   1   5=   +   5?   #   0  *System.Windows.Controls.DataGridTextColumn  5@   "    .  .   .   .  $ 
Parameters??6?     DataGridColumnHeaderStyle0# [ 6?   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6G   ?? *mParameterName+ 5A   &   ?? 5B   *   "  60   .?5C   .   ??  ? ??$	 100??6I   ?? 5D   .    ? ??$
 Auto??6F   ?? 5E   .    ? ??$ 25??6J   ?? 5F   .    ? ??$ 11(?6I   ?? 5G   .    ? ??$
 0.25q?6P   ?? 5H   .    ? ??# Z?6K   ?? 5I   .    ? ??
 ???? 6L   ?? 5J   .    ? ??$ Center=?6Y   ?? 5K   .    ? ?? .  6J    5L   .   P? 5M   2    ?@ 
IsSelected . . ?5N   6   ??  ? ??
 ?   ?6S   5O   3   5Q   +   &   ElementStyle& 5S   &   ?? 5T   *   " ??60   .?5U   .   ??  ? ??$ Center=?6R   ?? 5V   .    ? ??$ 5,0,0,0q?6G   5W   +   5Y   #     5Z   "    .  .   .   .  $ Values??6?   # [ 6?   % 6G   ?? *mValue+ 5[   &   ?? 5\   *   "  60   .?5]   .   ??  ? ??$	 120??6I   ?? 5^   .    ? ??$
 Auto??6F   ?? 5_   .    ? ??$ 25??6J   ?? 5`   .    ? ??$ 11(?6I   ?? 5a   .    ? ??$
 0.25q?6P   ?? 5b   .    ? ??
 ?????6K   ?? 5c   .    ? ??
 ???? 6L   ?? 5d   .    ? ??$ Center=?6Y   ?? 5e   .    ? ?? .  6J    5f   .   P? 5g   2    ?@ 
IsSelected . . ?5h   6   ??  ? ??
 ?   ?6S   5i   3   5k   +   & 5m   &   ?? 5n   *   " ??60   .?5o   .   ??  ? ??$ Center=?6R   ?? 5p   .    ? ??$ 5,0,0,0q?6G   5q   +   5s   #   5u      5v      5w      ]? 5x      -
   5        ' ]?Name$' panelMainTest??5x      $??Bottom=?63   $??Stretch=?6N   $??3??6l   $??	20,25,3,0q?6|   $	??Top=?6?   ??. .+?5y      ?? $??	Test Path??6     LabelStyle1#??[ 60   $
??Left=?6U   $??	0,0,-10,0q?6k   $??SemiBold"?6~   ?? 5z      -   5        ( ??Name$( txtTestName??5z        TextBoxStyle1#??[ 62   $	??480??6Y   $
??Auto??6h   $??Stretch=?6u   {?.  6?   5{      ?? 5|      -   5        $??tabMain??5|      $??	20,0,5,20q?6,   .@?5}      ?? -   5        $??tabKW??5}      $??Keyword??6+   $y?	Collapsed=?6<   ) ?? 
IsSelected) . .??5~      ]? -   5        $' dpKWMain??5~   $   .+?5      ? -   5        $ grdMain??5   #   * ? RowDefinitions* 5?   "   ?? 5?   &   -   5        + ??Name$+ 
rowKeyword??5?   4   ?? 5?   &   -   5        $+ rowSplitter??5?   4   $
??Auto ?6I   .+?5?   "   ]? -   5        $
' dpKW??5?   ,   $??0??6:   $??Stretch=?6H   .+?5?   &   ]? $	??Top=?60   ??5?   *   ?? 5?   .   " ]?64   .?5?   2   ??  ? {? . 6N    5?   2   ?? 5?   6   , ?? Binding, 6B   ?? $ IsTestEditorBusy?$# PropertyChanged=?$ . - ?? RelativeSource- ?? *FindAncestor+. ?? AncestorType". ?/ ?? Value/ true6?   .a?5?   :   ??  ? {? .  6V   5?   7   5?   /   .+?5?   *   ?? 
???????$??1,0,0,0q?6J   5?   '     5?   &   -   5        $ dgTestSteps??5?   /   $??0??6\   $ Visible=?6l     DataGridStyle1#??[ 5?   "     DataGridRowStyle10   RowStyle#0 [ 6J    	 DataGridCellStyle51   	CellStyle#1 [	 6x   $2   HorizontalScrollBarVisibility$2 Hidden=?5?   "   $ Single=?5?   "   $??Stretch=?6:   $
??Auto??6W   3   RowHeaderWidth$3 0Q?6e   4   
IsReadOnly4 . ???	#FF90D2FF5 %? IsVirtualizing5 .  ??5?   D   ??  5?   *     5?   .    .   .   . $
 Step??6?   # [ 6?   $ 40 6?   % 6A   ?? *mStepNumber+6?     5?   .    .   .   . $ Execute??6?   # [ 6?   $ 60 6?   % 6A   ?? *
mExecute+6?     5?   .    .   .   . $ Screen??6?    
 DataGridColumnHeaderStyle3# [
 6?   $	 196 6?   % 6A   ?? *	mScreen+6?     5?   .    .   .   . $ Control??6?     DataGridColumnHeaderStyle2# [ 6?   $	 197 6?   % 6A   ?? *
mControl+6?     5?   .    .   .   . $ Keyword??6?   # [
 6?   $	 197 6?   % 6A   ?? *
mKeyword+6?     5?   .    .   .   . $ 
Parameters??5?   A     DataGridColumnHeaderStyle4# [ 6U   $	 196 6?    5?   2   ?? 5?   6   .R?5?   :   ]? .+?5?   >   ?? 6 ??Name$6 	txtParams??6H   
???p?$??Stretch=?6?   $??CharacterEllipsis=?6?   ??6[   ?? *mParameterString+6?   5?   ;   5?   7   5?   /     5?   .    .   .   . $ Delay??6?     DataGridColumnHeaderStyle5# [ 6?   $ * 6?   % 6A   ?? *
mStepDelay+6?   5?   '   5?   #   5?      5?      5?      5?      5?      5?      5?      5?      