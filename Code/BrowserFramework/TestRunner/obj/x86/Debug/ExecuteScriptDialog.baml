   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	   $    TestRunner.ExecuteScriptDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'trclr-namespace:TestRunner     5   	   -   5          ? Title$  Execute Script??5   	    ? 
ResizeMode$ NoResize=?6!   $	??700??67   $	??700??6D    ? WindowStyle$ 
ToolWindow=?6g    ? WindowStartupLocation$ CenterScreen=?6?   
??????? ? ShowInTaskbar .   ? 	Resources 5      '   !TestRunner.DlkStepNumberConverter   snc#   TestRunner.DlkScreenConverter  scc$   TestRunner.DlkExecuteConverter  exc??%W   &	        &	      &	 &       5   
   6$     5   
   6      5	   
   6!   5
      .??5      ? $??Center=?6   ??5   
   ?? 5       ?? ImageSource$< 6pack://siteoforigin:,,,/Resources/DeltekBackground.png??6    j? Stretch$
 None=?6^   .+?5   
   ??  ?? Orientation$ Vertical=?6   $??5,5,5,5q?6,   $??4??6=   .+?5      ? 	 ? RowDefinitions	 5      ?? 5      $
??Auto ?6$   .+?5      ?? $ 
Horizontal=?6   $??0??66   $??0??6C   .+?5      ?? $??Execution Result :??6   ?? 5       -   5        
 ??Name$
 	lblResult??5       $?? ??6/   $
??Bold"?6:   5!      5#      ?? 5%      -   5         ??Name$ tabControlViews??5%   +   $??5,10,0,0q?6   $
??Left=?6D   $	??590??6_   $	??Top=?6l   $	??661??6?   .@?5&      ?? -   5         ??Name$ tabSteps??5&   0   $??Step Results??6   ???	#FF90D2FF.??5'      &   System.Windows.Controls.DataGrid  -   5          Name$ dgLogTestSteps??5'      $
??Auto??6O      RowHeaderWidth$ 0Q?6]     DataGridStyle1#??[ 5(        DataGridCellStyle3   	CellStyle# [ 6G      SelectionMode$ Single=?5)      $   HorizontalScrollBarVisibility$ Visible=?66   "   VerticalScrollBarVisibility$ Visible=?5*         RowDetailsVisibilityMode$ 	Collapsed=?6E   $??Stretch=?5+   F    %? IsVirtualizing .  ??5'   7   ?? ??5,      ?? 5-       ?? 
TargetType)  #System.Windows.Controls.DataGridRow"  6$   .?5.   "   ??  ?? Property ? ?? ?? Value$
 0.25q?6D   -   5         ?? Triggers 50   "   P? 51   &    P? Property ?@ 
IsSelected P? Value . . ?52   *   ??  ? ??
 ??+??6H   ?? 53   *    ? ??$ 2q?6L   54   '   ?? 55   &    ?? Binding 62   ?? *
mKeyword+ ?? Value 
LogComment6O   .a?56   *   ??  ? ??
 ? ???6G   ?? 57   *    ? ??
 ? E??6H   ?? 58   *    ? ??$ 1q?6L   59   '   ?? 5:   &    62   ?? *
mKeyword+ Note6O   .a?5;   *   ??  ? ??
 ? ???6G   ?? 5<   *    ? ??
 ?2͚?6H   ?? 5=   *    ? ??$ 1q?6L   5>   '   ?? 5?   &    62   ?? *
mKeyword+ Hint6O   .a?5@   *   ??  ? ??
 ?????6G   ?? 5A   *    ? ??
 ? ? ?6H   ?? 5B   *    ? ??$ 1q?6L   5C   '   ?? 5D   &    62   ?? *
mKeyword+ ExpectedResult6O   .a?5E   *   ??  ? ??
 ?????6G   ?? 5F   *    ? ??
 ????6H   ?? 5G   *    ? ??$ 1q?6L   5H   '   ?? 5I   &    62   ?? *
mKeyword+ Step6O   .a?5J   *   ??  ? ??
 ?????6G   ?? 5K   *    ? ??
 ????6H   ?? 5L   *    ? ??$ 1q?6L   5M   '   ?? 5N   &    62   ?? *
mExecute+	 False6O   .a?5O   *   ??  ? ??
 ?????6H   ?? 5P   *    ? ??$ Italic$?6G   5Q   '   5S         Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   CanUserSort .     CanUserReorder .      CanUserResize  .  !   Header$
! Step??6?     DataGridColumnHeaderStyle0"   HeaderStyle#" [ 6?   #   
IsReadOnly# . 1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 61   ?? *mStepNumber+% ?? 	Converter#% [  6?     5W       .   .    .  $! Execute??6?     DataGridColumnHeaderStyle1#" [ 6?   # . $ 61   ?? *
mExecute+#% [ 6?     5X       .   .    . $! Screen??6?   #" [ 6?   # . &   Width5	  /System.Windows.Controls.DataGridLengthConverter$	& 100	 6	  $ 61   ?? *	mScreen+#% [ 6	    5Y       .   .    . $! Control??6?   #" [ 6?   # . $	& 100	 6?   $ 61   ?? *
mControl+6?     5Z       .   .    . $! Keyword??6?   #" [ 6?   # . $	& 125	 6?   $ 61   ?? *
mKeyword+6?     5[       .   .    . $! 
Parameters??6?     DataGridColumnHeaderStyle5#" [ 6?   $	& 135	 6?     DataGridCellStyle2'   	CellStyle#' [ 6?   # . $ 61   ?? *mParameterString+(   ElementStyle( 5\   "   ?? 5]   &   .?5^   *   ??  ? ??$	 390??6O   ?? 5_   *    ? ??$	 390??6O   ?? 5`   *    ? ??$
 Wrap=?6S   5a   '   5c        5d       .   .    .  $! Status??6?   #" [ 6?   # . $ 61   ?? *mStepStatus+6?   )   RowDetailsTemplate) 5f      ?? 5g      .R?5h   "   ?? $??3q?6)   * ?? Padding$* 3q?64   
?????F?$??3q?6X   + ?? CornerRadius$+ 5??6l   ._?5i   "     -   5        $ 0Q?5i   T   #??[ 6g   $ Single=?5j   1   $	??595??5k   1   $ Visible=?5l   1   $	??190??5m   1    .  ??5i   +   ?? *mStepLogMessages+??5n   *   ?? 5o   .   "  64   .?5p   2   -   5        5q   /    5s   *     5t   .    .   .    .  $! DateTime??6?   $	& 100	 6?   # . $ 6A   ?? *mMessageDateTime+6?     5u   .    .   .    .  $
! Type??6?   $& 95	 6?   # . $ 6A   ?? *mMessageType+6?     5v   .    .   .    .  $! Details??6?   $	& 400	 5w   =   # . $ 5v   A   ?? *mMessageDetails+( 5x   2   ?? 5y   6   .?5z   :   ??  ? ??$
 Wrap=?6c   ?? 5{   :    ?@??TextAlignment$
 Left=?6d    5|   :   ?? 5}   >    6J   ?? *mMessageType+ EXCEPTIONIMG6k   .a?5~   B   ??  ? ??
 ??  ?6i   ?? 5   B    ? ??$ 	Underline{?6n   5?   ?   ?? 5?   >    6J   ?? *mMessageType+ 
OUTPUTFILE6k   .a?5?   B   ??  ? ??
 ??  ?6i   ?? 5?   B    ? ??$ 	Underline{?6n   5?   ?   5?   7   5?   /   5?   '   5?   #   5?      5?      5?      5?      ?? 5?      $ 
Horizontal=?6   .+?5?      ?? -	   5        $??	btnExport??5?      , ? ToolTip$, Export Results??6*   $??Export??6C   $??10,0,0,0q?6T   $
??Left=?6f    	 ButtonStyle2#??[	 6?   $??80??6?   6?   ?? 5?      -
   5        - ??Name$- btnClose??5?      $??210,6,293,5q?6+   $??Close??6@   $??Center=?6P   #??[	 6m   . J? RenderTransformOrigin$. 
-5.22,1.25&?6?   $??80??6?   5?      5?      5?      5?      