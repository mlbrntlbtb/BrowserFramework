   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:TestRunner.CommonTestRunner.Common  5   	   'clr-namespace:TestRunner
TestRunner  5   	       TestRunner.OSRecorder   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2commonclr-namespace:TestRunner.Common     5   	   /
TestRunnerclr-namespace:TestRunner     5   	   -   5          ? Title$  Object Store Recorder??5   	   $	??600??6'   $	??427??64   ???	#FF90D2FF ? WindowStartupLocation$ CenterOwner=?5       .??5	      ]? ??. .+?5
   
   o?  o? 
IsMainMenu . ???	#FF90D2FF$	??Top=?68   .@?5      m? $??_File??6   .@?5      m? -   5         m?Name$ mnuNew??5      $
??_New??6+   69   ?? 5      m? 5      -   5        $ mnuOpen??5      $??_Open??6,   6;   ?? 5      m? 5      -   5        $ mnuSave??5      $??_Save??6,   {?.  6Q   m? 5      -   5        $ 	mnuSaveAs??5      $??Save _As??6.   6@   ?? 5      m? 5      -   5        $ mnuExit??5      $??E_xit??6,   6;   5      5      ?? 5   
    ?? Orientation$ 
Horizontal=?6   $??5,0,5,0q?6.   ???	#FF90D2FF$	??Top=?6V   .+?5      ?? -   5         ??Name$ 	lblScreen??5      $??Screen :    ??6'   $??0,0,0,0q?6>   ?? 5      -   5         ??Name$ 	txtScreen??5      $??5,0,0,0q?6N   $	??339??6_   $??20??6k   $??10(?6w   ??6)   ?? *Screen+ ?? Mode$ TwoWay=?6w   5      ?? 5   
   $ Vertical=?6   $??Bottom=?6,   .+?5      ?? -	   5         ??Name$ 
tabControl??5      $??5,5,5,0q?6-   .@?5      ?? -
   5        	 ??Name$	 tabProperties??5      $??
Properties??61   ???	#FF90D2FF
 ?? 
IsSelected
 . .??5      ?? $ Vertical=?6!   ???	#FF90D2FF.+?5      ? ???	#FF90D2FF$??0,5,0,0q?66    ? ColumnDefinitions 5       ?? 5!   "   $	??120 ?63   ?? 5"   "   $??* ?63    ? RowDefinitions 5$      ?? 5%   "   $
??Auto ?60   ?? 5&   "   $
??Auto ?60   ?? 5'   "   $
??Auto ?60   ?? 5(   "   $
??Auto ?60   .+?5*      ?? -   5        $ lblControlType??5*   $   $??Control Type :??6<   $??5,0,0,0q?6U   $??0??6f   $??0??6v   ?? 5+      -   5        $ lblControlName??5+   $   $??Control Name :??6<   $??5,0,0,0q?6U   $??0??6f   $??1??6v   ?? 5,      -   5        $ lblSearchType??5,   $   $??Search Type :??6;   $??5,0,0,0q?6S   $??0??6d   $??2??6t   ?? 5-      -   5        $ lblSearchValue??5-   $   $??Search Value :??6<   $??5,0,0,0q?6U   $??0??6f   $??3??6v   ?? 5.      -   5         ??Name$ cboControlType??5.   '    ?? 
IsEditable . $??5,0,5,0q?6{   $??1??6?   $??0??6?    ?? Text 6?   ?? *ControlType+$ TwoWay=?6?   ?? 5/      -   5        $ txtControlName??5/   &   $??5,0,5,0q?6h   $??1??6y   $??1??6?   ??6>   ?? *ControlName+$ TwoWay=?6?   ?? 50      -   5        $ cboSearchType??50   '    . $??5,0,5,0q?6y   $??1??6?   $??2??6?    6>   ?? *
SearchType+$ TwoWay=?6?   ?? 51      -   5        $ txtSearchValue??51   &   $??5,0,5,0q?6h    ?? TextWrapping$
 Wrap=?6y    ?? AcceptsReturn .  $??60??6?   $??1??6?   $??3??6?   ??6>   ?? *SearchValue+$ TwoWay=?6?   52      ? 53       54      ?? 55   "   $
??Auto ?63   ?? 56   "   $??* ?63   ?? 57   "   $
??Auto ?63    59      ?? 5:   "   $
??Auto ?60   .+?5<      ?? -   5        $??
btnInspect??5<   %      
RecordIcon#??[  67     BigButtonStyle#??[ 6]   $??0??6?   $??0??6?   $??5,0,0,0q?6?    ? ToolTip$ Inspect??6?   ?? 5>      $ 
Horizontal=?6)   $??Right=?6B   $??2??6^   $??0??6n   $??	0,10,5,10q?6{   .+?5?   "   ?? -   5         ??Name$ btnHighlight??5?   )   $??	Highlight??6?   $??Right=?6S     ButtonStyle2#??[ 6o   $??0,0,5,0q?6?   6?   ?? 5@   "   -   5        $ btnSaveControl??5@   )   $??Update??6A   #??[ 6R   6x   ?? 5A   "   -   5        $ 	btnCancel??5A   )   $??Cancel??6<   #??[ 6M   6s   5B      5D      5F      5H      ?? 5I      -   5        $	 tabDOM??5I      $	??DOM??6*   ???	#FF90D2FF.??5J         TestRunner.XmlViewer  -   5          Name$ 	DOMViewer??5J   +   $??0??6>   $??2??6N   $??5,5,5,5q?6[   $	??204??6l   5K      5L      5N      `? 5O   
   $	??Top=?6   $??5,0,5,0q?6+   $??Stretch=?6<    `? 
Background ?	#FF90D2FF.?5P      c? $??Stretch=?6   ???	#FF90D2FF.@?5Q      ?? -   5        $ btnAdd??5Q        AddIcon#??[ 6)     ToolbarButtonStyle#??[ 6L   $	 Add??6x   6?   ?? 5R      -   5        $ btnEdit??5R        EditIcon#??[ 6*   #??[ 6N   $
 Edit??6z   6?   ?? 5S      -   5        $ 	btnDelete??5S        
DeleteIcon#??[ 6,   #??[ 6R   $ Delete??6~   6?   ?? 5T      ?? 5U      $??Find: ??6   ?? 5V      -   5        $ txtFind??5V      $	??180??6+   ?? 5W      -   5        $ btnFindNext??5W        FindIcon#??[ 6.   #??[ 6R   $
 Find??6~   6?   ?? 5X      -   5        $ btnClear??5X       	 Clear#??[ 6+   #??[ 6L   $ Clear??6x    J? RenderTransformOrigin$ 
0.88,-0.72&?6?   5Z      5[      &   System.Windows.Controls.DataGrid  5\   
   -   5          Name$ 
dgControls??5\         RowHeaderWidth$ 0Q?6?   $??5,0,5,5q?6R    	 DataGridStyle1#??[	 5]      $   HorizontalScrollBarVisibility$ Hidden=?6?      SelectionMode$ Single=?5^         
IsReadOnly .     CanUserAddRows .     CanUserDeleteRows .   
 DataGridRowStyle1   RowStyle# [
 5`      ??5\   '   ??    Columns 5b      0  *System.Windows.Controls.DataGridTextColumn  5c      ,  &System.Windows.Controls.DataGridColumn    CanUserSort  .  !   CanUserReorder! .  "   CanUserResize" .  #   Header$# Control Type??6?     DataGridColumnHeaderStyle0$   HeaderStyle#$ [ 6?   %   Width5  /System.Windows.Controls.DataGridLengthConverter$% * 6?   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6%   ?? *mControlType+6?     5d        .  ! .  " .  $
# Name??6?   #$ [ 6?   $% * 6?   & 6%   ?? *mKey+6?   5f      5h      5     