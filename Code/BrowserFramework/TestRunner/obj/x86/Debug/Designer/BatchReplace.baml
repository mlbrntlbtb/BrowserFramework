   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:TestRunner.CommonTestRunner.Common  5   	   9!clr-namespace:TestRunner.DesignerTestRunner.Designer  5   	   9!clr-namespace:TestRunner.DesignerTestRunner.Designer  5   	   &     TestRunner.Designer.BatchReplace   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2commonclr-namespace:TestRunner.Common     5   	   0td!clr-namespace:TestRunner.Designer     5   	   3local!clr-namespace:TestRunner.Designer     5   	     ? Title$  Batch Replace??5	   	    ? WindowStartupLocation$ CenterOwner=?6    ? 
ResizeMode$ NoResize=?6C   $	??500??6Y   $	??800??6f    ? 	Resources 5
      /   )TestRunner.Designer.DlkDataParamConverter   DataConverter??%   &	          5   
   6#   5      .??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T    ? ColumnDefinitions 5   
   ?? 5      $	??250 ?6   $	??250??6+   $	??400??6:   ?? 5      $??5 ?6   ?? 5      $??* ?6   .+?5   
   ?? -   5         ??Name$ tabTestExplorer??5   &   $??5,0,0,0q?6   $??Stretch=?6?   $
??Auto??6]   $??Stretch=?6k   $
??Auto??6?   $??0??5      ???	#FF90D2FF J? RenderTransformOrigin$ 
0.506,0.85&?6<   ??5      ?? 5      	 ?? 
TargetType"	 ??6   .?5      ?? 
 ?? Property
 ? {? ?? Value . 62   5      .@?5      ?? -   5         ??Name$ 
tabKeyword??5   -   $??Test Explorer??6   ???	#FF90D2FF$y?	Collapsed=?6X   .??5      ?  ? RowDefinitions 5      ?? 5       .+?5"      ? $??0??6   $??2??6+   .+?5#      ]? $??2??6$   $??0??65   $??0,0,0,0q?6E   .+?5$      R? -   5         R?Name$ tvwAllTests??5$   '   $ -0.747,0.508&?6T     ctxSaveDialogTreeView ? ContextMenu# [ 5%   '    %? IsVirtualizing .   %? VirtualizationMode$ 	Recycling=?6?   ??5$   <   ?? ??5&   "   ?? 5'   &   "	 Q?6,   .?5)   *   ?? 
 ?@Q?
IsSelected 6G   ??  ?? Path$ 
IsSelected? ?? Mode$ TwoWay=? ?? NotifyOnSourceUpdated .  ?? UpdateSourceTrigger$ PropertyChanged=? ?? Triggers 5*   *   5/   '    51   "    ?? DataType    TestRunner.Common.KwFolder ?? ItemsSource ?? Orientation ? ToolTip   TestRunner.Common.KwFile??%O  (??       52   ?   *, +)(??     58   ?   *, +)?? 52   &   "  6?    6c   ?? *
DirItems+.R?53   *   ?? $ 
Horizontal=?65   .+?54   .   ?? $??Stretch=?6L   $??16??6h   $??16??6s   ??64   ?? *Icon+6s   ?? 55   .   $??3,0,0,0q?6g   ??68   ?? *Name+ 6N   ?? *Path+6g   56   +   57   '   ?? 58   &   "  6?   .R?59   *   ?? $ 
Horizontal=?65   .+?5:   .   ?? $??Stretch=?6L   $??15??6h   $??13??6s   ??64   ?? *Icon+6s   ?? 5;   .   $??3,0,0,0q?6g   ??68   ?? *Name+ 6N   ?? *Path+6g   5<   +   5=   '   5>   #   5?      5@      5A      5B      5C      5D      ?? 5E   
   -   5         ??Name$ gridSplitter??5E      $??1??6-   $??1??6=   $??2??6J    ?? ShowsPreview .  $
??Auto??6p   
???????$??Stretch=?6?   $??0,0,0,1q?5F   +   $??Stretch=?6E    J? Opacity$ 1Q?6c   ?? 5G   
   -   5        $ tabFieldSettings??5G   &   $??0,0,0,0q?6   $??Stretch=?6@   $
??Auto??6^   $??Stretch=?6l   $
??Auto??6?   $??3??5H      $??1??6%   $??2??62   ???	#FF90D2FF??5I      ?? 5J      "	 ??6   .?5K      ?? 
 ? {? . 62   5L      .@?5N      ?? -   5        $ 	tabFields??5N   0   $??Replace Settings??6   ???	#FF90D2FF$y?	Collapsed=?6Z   .??5O      ?  5P      ?? 5Q      .+?5S      ? $??1??6    5T      ?? 5U      $??20 ?6,   ?? 5V      $??30 ?6,   ?? 5W      $	??130 ?6,   ?? 5X      $??* ?6,   ?? 5Y      $??20 ?6,   ?? 5Z      $??30 ?6,   ?? 5[      $??35 ?6,   .+?5^      ?? $??0??6%   $ Vertical=?62   .+?5_      ?? 
??????$
??Bold"?6@   Replace Settings6d   5`      ? 5a      $??1??6    5b      ?? 5c   "   $??25 ?60   .+?5e      ?? -   5         ??Name$ stpReplaceSettings??5e   )   $??0??6E   $ 
Horizontal=?6R   $	??Top=?6k   .+?5f   "   ?? $??10??6,   ?? 5g   "   Replace Field:6<   ?? 5h   "   $??10??6,   ?? 5i   "   
??????6F   ?? 5j   "   $??10??6,   	? 5k   "   -   5         	?Name$ rdoParameter??5k   .     d? 	IsChecked$
  TrueY?6D   {?. .??6f   	Parameter6q   ?? 5l   "   $??10??6,   	? 5m   "   -	   5        $ 
rdoControl??5m   .   $  FalseY?6B   {?. .??6e   	Control6n   5n      5o      ?? 5p      $??2??6%   $ Vertical=?62   .+?5q      ]? $??0??6(   $??30??65   .+?5r   "   ?? -
   5        ! ??Name$! lblFindText??5r   ,   $??	10,0,10,0q?6A   $??Center=?6T   $??26??6o   	Find What6?   ?? 5s   "   -   5        " ??Name$" txtFindWhat??5s   *   $??	21,0,10,0q?6?   $??22??6R   ??6^   ?? *Name+6v   5t      ]? 5u      $??1??6(   $??30??65   .+?5v   "   ?? -   5        $! lblReplaceText??5v   ,   $??	10,0,10,0q?6D   $??Center=?6W   $??26??6r   Replace With6?   ?? 5w   "   -   5        $" 
txtReplace??5w   *   $??5,0,10,0q?6>   $??22??6P   ??6\   ?? *Name+6t   5x      ?? 5y      $??3??6)   $ 
Horizontal=?66   $??25??6O   $
??Left=?6[   $??	10,0,20,0q?6v   .+?5z   "   ?? -   5        # ??Name$# chkExactMatch??5z   +   $??2??6B   $??Bottom=?6R   $  FalseY?6m   .??6   Exact Match6?   5{      ?? 5|      $??4??6)   $ 
Horizontal=?66   $??25??6O   $??Right=?6[   $??0,0,10,0q?6w   .+?5}   "   ?? -   5        $ ??Name$$ btnAdvanced??5}   )   $??65??6>     ButtonStyle1#??[ 6I   $??0q?6o   {?.  .??6?   
Advanced6?   ?? 5~   "   $??10??6,   ?? 5   "   -   5        $$ 
btnReplace??5   )   $??65??6=   #??[ 6H   $??0q?6n   .??6?   	Replace6?   5?      ?? 5?      $??5??6)   $ 
Horizontal=?66   $??25??6O   $??Right=?6[   $??0,5,0,0q?6w   .+?5?   "   ?? -   5        $! lblTestUpdates??5?   ,   $??	10,0,10,0q?6D   $??Center=?6W   $??26??6r   6?   ?? 5?   "   -   5        $! lblTotalUpdates??5?   ,   $??	10,0,10,0q?6E   $??Center=?6X   $??26??6s   6?   5?      5?      &   System.Windows.Controls.DataGrid  5?      -   5        %  Name$% dgTestSteps??5?   0   $??3??6#   "&   VerticalScrollBarVisibility$& Visible=?6]     DataGridStyle1#??[ 5?   "     DataGridRowStyle1'   RowStyle#' [ 6J     DataGridCellStyle5(   	CellStyle#( [ 6x   $)   HorizontalScrollBarVisibility$
) Auto=?5?   "   *   SelectionMode$* Extended=?5?   "   $??Stretch=?6<   $
??Auto??6Y   +   RowHeaderWidth$+ 0Q?6g   ,   
IsReadOnly, . ???	#FF90D2FF .  $??	0,10,0,10q?5?   g   $??1??6z   $ 0.497,0.649&?6?   ??5?   E   ?? -   Columns- 5?      0  *System.Windows.Controls.DataGridTextColumn  5?   "   ,  &System.Windows.Controls.DataGridColumn.   CanUserSort. .  /   CanUserReorder/ .  0   CanUserResize0 . 1   Header$
1 Step??6?     DataGridColumnHeaderStyle02   HeaderStyle#2 [ 6?   3   Width5  /System.Windows.Controls.DataGridLengthConverter$3 30 6?   1  +System.Windows.Controls.DataGridBoundColumn4   Binding4 65   ?? *mStepNumber+6?     5?   "   . .  / .  0 . $1 Execute??6?     DataGridColumnHeaderStyle1#2 [ 6?   $3 50 6?   4 65   ?? *
mExecute+6?     5?   "   . .  / .  0 . $1 Screen??6?     DataGridColumnHeaderStyle3#2 [ 6?   $	3 100 6?   4 65   ?? *	mScreen+6?     5?   "   . .  / .  0 . $1 Control??6?    	 DataGridColumnHeaderStyle2#2 [	 6?   $3 80 6?   4 65   ?? *
mControl+6?     5?   "   . .  / .  0 . $1 Keyword??6?   #2 [ 6?   $3 80 6?   4 65   ?? *
mKeyword+6?   4	  .System.Windows.Controls.DataGridTemplateColumn	  5?   "   . .  / .  0 . $1 
Parameters??5?   A    
 DataGridColumnHeaderStyle4#2 [
 6U   $3 * 6?   5 	  CellTemplate5 5?   &   ?? 5?   *   .R?5?   .   ]? .+?5?   2   ?? $! 	txtParams??6<   
???p?$??Stretch=?6?   $??CharacterEllipsis=?6?   ??6O   ?? $ mParameterString?6 ?? 	Converter#6 [  6?   5?   /   5?   +   5?   #   5?      ?? 5?      $??4??6%   $ Vertical=?62   .+?5?      ?? $??Center=?6(   Data Row/s:6R   5?      ?? 5?      $??5??6%   $ Vertical=?62   .+?5?      ? $??Center=?6#    5?   "   ?? 5?   &   $??45 ?67   ?? 5?   &   $??45 ?67   ?? 5?   &   $??45 ?67   ?? 5?   &   $??45 ?67   ?? 5?   &   $??45 ?67   ?? 5?   &   $??45 ?67   ?? 5?   &   $??45 ?67   .+?5?   "   ?? $??0??6-   .+?5?   &   ?? $ 
Horizontal=?61   $??Center=?6J   $??35??6g   .+?5?   *   ?? -   5        $$ btnDataRowFirst??5?   1   $??35??6J   #??[ 6U   $??|<??6{   {?.  6?   5?   '   5?   #   ?? 5?   "   $??1??6-   .+?5?   &   ?? $ 
Horizontal=?61   $??Center=?6J   $??35??6g   .+?5?   *   ?? -   5        $$ btnDataRowPrevious??5?   1   $??35??6M   #??[ 6X   $??<??6~   {?.  6?   5?   '   5?   #   ?? 5?   "   $??2??6-   .+?5?   &   ?? $ 
Horizontal=?61   $??Center=?6J   $??35??6g   .+?5?   *   ?? -   5        $" txtDataRowCurrent??5?   2   $??40??6M   $??25??6X   {?.  6d   5?   '   5?   #   ?? 5?   "   $??3??6-   .+?5?   &   ?? $ 
Horizontal=?61   $??Center=?6J   $??35??6g   .+?5?   *   ?? -   5        $! lblOf??5?   4   $??Center=?6C   of6b   5?   '   5?   #   ?? 5?   "   $??4??6-   .+?5?   &   ?? $ 
Horizontal=?61   $??Center=?6J   $??35??6g   {?.  .+?5?   *   ?? -   5        $" txtDataRowTotal??5?   2   $??40??6K   $??25??6V   5?   '   5?   #   ?? 5?   "   $??5??6-   .+?5?   &   ?? $ 
Horizontal=?61   $??Center=?6J   $??35??6g   .+?5?   *   ?? -   5        $$ btnDataRowNext??5?   1   $??35??6I   #??[ 6T   $??>??6z   {?.  6?   5?   '   5?   #   ?? 5?   "   $??6??6-   .+?5?   &   ?? $ 
Horizontal=?61   $??Center=?6J   $??35??6g   .+?5?   *   ?? -   5        $$ btnDataRowLast??5?   1   $??35??6I   #??[ 6T   $??>|??6z   {?.  6?   5?   '   5?   #   5?      5?      ?? 5?      $??6??6%   $ Vertical=?62   .+?5?      ?? $ 
Horizontal=?6)   $??0??6B   $??Right=?6O   $??Bottom=?6k   $??45??6?   $??0,0,5,0q?6?   .+?5?   "   ?? -   5        $$ btnSave??5?   )   $??65??6:   #??[ 6E   $
??Save??6k   6z   5?      5?      5?      5?      5?      5?      5?      5?      