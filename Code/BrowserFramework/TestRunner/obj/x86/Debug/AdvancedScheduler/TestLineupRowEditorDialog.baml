   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TestRunner.AdvancedSchedulerTestRunner.AdvancedScheduler  5   	   W0clr-namespace:TestRunner.AdvancedScheduler.Model"TestRunner.AdvancedScheduler.Model  5   	   9!clr-namespace:TestRunner.ControlsTestRunner.Controls  5   	   <    6TestRunner.AdvancedScheduler.TestLineupRowEditorDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   <local*clr-namespace:TestRunner.AdvancedScheduler     5   	   Bmodel0clr-namespace:TestRunner.AdvancedScheduler.Model     5   	   6controls!clr-namespace:TestRunner.Controls     5   	   -   5          ? Title$  
Row Editor??5   	   $??77??6   $	??612??6(    ? WindowStartupLocation$ CenterOwner=?64   $	??612??6X   $??77??6g   $??77??6v   $	??970??6?    ? WindowStyle$ 
ToolWindow=?6?    ? 	Resources 5         ColHeaderStyle ?? 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader ?? Property ?? Value  ColHeaderStyleCentered  RowStyle)  #System.Windows.Controls.DataGridRow ?? Triggers P? Property	 P? Value  	CellStyle*  $System.Windows.Controls.DataGridCell  CellStyleResults=   7TestRunner.AdvancedScheduler.Model.EnumerationConverter  
EConverter;   5TestRunner.AdvancedScheduler.Model.StartTimeConverter  STConverter:   4TestRunner.AdvancedScheduler.Model.ScheduleConverter  
SConverterE   ?TestRunner.AdvancedScheduler.Model.BlacklistVisibilityConverter  BlacklistConverter=   7TestRunner.AdvancedScheduler.DropdownBlacklistConverter 	 DDBlacklistConverter??%?  &	        &	 ?     &	 ~    &	 R    &	 ?    &	 ?    &	 ?    &	     &	 $    &		 7    ?? 5	   
   ColHeaderStyle??"  6?   .?5
      ??  ? ??
 ??  ?6+   ?? 5       ? ??
 ?????6+   ?? 5       ? ??$ 11(?6)   5      ?? 5   
   ColHeaderStyleCentered??"  6O   .?5      ??  ? ??
 ??  ?6+   ?? 5       ? ??
 ?????6+   ?? 5       ? ??$ 11(?6)   ?? 5       ? ??$ Center=?6;   5      ?? 5   
   "  6!   .?5      ??  ? ??$
 0.25q?60    5      P? 5       ?@ 
IsSelected	 . . ?5      ??  ? ??
 ??+??64   ?? 5       ? ??$ 2q?68   5      5      ?? 5   
   "  6"   .?5      ??  ? ??$
 0.25q?60   ?? 5       ? ??
 ?????6+   ?? 5        ? ??
 ??  ?6+   5!      ?? 5"   
   "  6)   .?5#      ??  ? ??$
 0.25q?60    5$      P? 5%       ?@ 
IsSelected	 . . ?5&      ??  ? ??
 ?????63   ?? 5'       ? ??
 ??  ?63   ?? 5(       ? ??$ 
ExtraBlack"?63   5)      5+        5,   
   6%     5-   
   6#     5.   
   6"     5/   
   6-     50   
   6+   51      .??52      ? ??53   
   ?? 54      
 ?? ImageSource$2
 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6T   .+?56   
   ]? ??. .+?57      &	   System.Windows.Controls.DataGrid	  -   5         	 Name$ dgRowEditor??57      $??0??6,   $	??Top=?69    
 DataGridStyle1#??[
 6N    	  ColumnHeaderStyle# [  58   '    	  CanUserAddRows .   	  	CellStyle# [ 59   >   $y?Visible=?6e    	  CanUserDeleteRows .  $ 	  HorizontalScrollBarVisibility$ Disabled=?5;   A   ??58   [   ??  5<        EditingDateTemplate(
  "System.Windows.Controls.DatePicker 
  SelectedDate ?? Mode ?? NotifyOnSourceUpdated ?? UpdateSourceTrigger  EditingListTemplate ?? RelativeSource ?? AncestorType ?? Text  UneditableTemplate??%y  &	       &	 m     &	      ?? 5=      .R?5>      
   6%   ?? *
Schedule+$ TwoWay=? . $ PropertyChanged=?5?      ?? 5@      .R?5A      ?? -   5        ??5A   #   ?? *
DaysOfWeek+ ?? " ? 5B   3   ?? *
Schedule+$ TwoWay=? . $ PropertyChanged=?6?   5C      ?? 5D      .R?5E      ?? {?.  $?? ??66   5F      5G       	  RowStyle 5H      ?? 5I      "  6   .?5J      ??  ? ??$ 27.75??63   ?? 5K       ? ??$
 0.25q?6<    5L      P? 5M       ?@ 
IsSelected	 . . ?5N   "   ??  ? ??
 ??+??6@   ?? 5O   "    ? ??$ 2q?6D   5P      5R       	  Columns 5T      4  .System.Windows.Controls.DataGridTemplateColumn  5U      ,  &System.Windows.Controls.DataGridColumn   Header$ Enabled??6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6>      CanUserResize .     CellTemplate 5V      ?? 5W      .R?5X   "   ?? {?6+   ?? *IsEnabledAndNotGroupMember+ d? 	IsChecked 5Y   -   ?? *	Enabled+$ TwoWay=?$ PropertyChanged=? . ??5Z   &   ?? 5[   *   " ??60   .?5\   .   ??  ? ??$ Center=?6T   ?? 5]   .    ? ??$ Center=?6R   5^   +   5`   #   5a      5c        5d      $ Agent??6-   $	 100 6<    5e      ?? 5f      .R?5g   "   ??   ?? Padding$  3q?6,   ??68   ?? *AssignedAgentName+5h      !   CellEditingTemplate! 5j      ?? 5k      .R?5l   "   ?? -   5        " ?? SelectedValuePath$
" Name??5o   9   ??5l   +   ?? *AgentsListView+ ?? " ?$ PropertyChanged=?# ?? SelectedItem# 5m   9   ?? *RunningAgent+ . $ PropertyChanged=?$ ?? SelectedValue$ 5n   9   ?? *RunningAgentName+$ OneWay=?{?5o   R   ?? *IsEnabledAndNotGroupMember+% ?? 
GroupStyle% 5p   &   (  "System.Windows.Controls.GroupStyle  5q   *   &   HeaderTemplate& 5r   .   ?? 5s   2   .R?5t   6   ?? $??	ExtraBold"?6V   {?.  ??6@   ?? *Name+6m   5u   3   5w   +   ??5y   &   ?? 5z   *   .R?5{   .   ?? ??68   ?? *Name+5|   +   5~   #   5      5?        5?      $ Environment??6-   $ * 6B   '   MinWidth$' 90Q?6L    5?      ?? 5?      .R?5?   "   ?? ( ?? Orientation$( 
Horizontal=?6-   .+?5?   &   ?? $0??*pack://siteoforigin:,,,/Resources/warn.ico??6,   $??Center=?6`   $??12??6{   $??12??6?   $??
2, 0, 0, 3q?6?   ) ? ToolTip$ ) Environment is blacklisted??6?   y?6?   ?? * ?? Path$* IsBlacklisted?+ ?? 	Converter#+ [ $ TwoWay=?$ PropertyChanged=?6X  ?? 5?   &   $  3q?60   ??6<   ?? *Environment+5?   #   5?      ! 5?      ?? 5?      .R?5?   "   ?? -   5        {?5?   +   ?? *IsEnvironmentEnabled+??6V   ?? *EnvironmentList+ ?? " ?$ PropertyChanged=?# 5?   9   ?? *Environment+ . $ PropertyChanged=???5?   &   5  -System.Windows.Controls.ItemContainerTemplate?  5?   *   .R?5?   .   ?? $( 
Horizontal=?69   .+?5?   2   ?? $0??*pack://siteoforigin:,,,/Resources/warn.ico??68   $??12??6l   $??12??6x   $??Center=?6?   $ ) Environment is blacklisted??6?   y?6?   ?? #+ [	 $ PropertyChanged=?61  ?? 5?   2   $  
3, 0, 0, 0q?6M   ??6<   ?? 6M   5?   /   5?   +   5?   #   5?      5?        5?      $ Browser??6-   $ 90 6>    5?      ?? 5?      .R?5?   "   ?? $  3q?6,   ??68   ?? *Browser.Name+5?      ! 5?      ?? 5?      .R?5?   "   ?? -   5        $
" Name??5?   ?   ??5?   +   ?? *BrowserList+ ?? " ?$ PropertyChanged=?# 5?   ?   ?? *	Browser+ . $ PropertyChanged=?{?5?   >   ?? *IsEnabledAndNotGroupMember+$ 5?   ?   ?? *Browser.Name+$ OneWay=?% 5?   &     5?   *   & 5?   .   ?? 5?   2   .R?5?   6   ?? $??	ExtraBold"?6V   {?.  ??6@   ?? *Name+6m   5?   3   5?   +   ??5?   &   ?? 5?   *   .R?5?   .   ?? ??68   ?? *Name+5?   +   5?   #   5?      5?        5?      $ 
Start Time??6-   $	 100 6A    .   5?      ?? 5?      .R?5?   "   ?? $??Center=?6,   , ?? TextAlignment$, Center=?6G   ??6^   ?? *	StartTime+$ TwoWay=? . $ PropertyChanged=?#+ [ 5?   }   5?      ! 5?      ?? 5?      .R?5?   "   (   "TestRunner.Controls.TimePickerCtrl  -  Name$- tpckrStartTime??6:   {?6R   ?? *IsEnabledAndNotGroupMember+5?   #   5?      5?        5?      $ 
Recurrence??6-   $ 80 6A    .   5?      ?? 5?      .R?5?   "   ?? . ?? 
IsEditable. . {?6=   ?? *IsEnabledAndNotGroupMember+ 5?   1   ?? *
Recurrence+#+ [ $ TwoWay=? . $ PropertyChanged=???5?   1   ?? *RecurrenceTypes+ ?? " ?5?      5?        5?      $ When???6-   $	 100 6<   $	' 100Q?6H    5?      ?? 5?      .R?5?   "   ?? $??Center=?6,   $, Center=?6G   ??5?   &   ?? 5?   *   " ??60   .?5?   .   ??  ? ?? 5?   2   `? 5?   6   / `? 	Converter#/ [ 6C   .1?5?   :   ?? $* Schedule?6B   $ TwoWay=?6S    . $ PropertyChanged=?6~   0 ?? StringFormat$0 d??6?   ?? 5?   :   $* 
Recurrence?6B   $ TwoWay=?6T    . $ PropertyChanged=?6   $0 d??6?   5?   7   5?   /   5?   +   5?   #   5?      ! 5?      ?? 5?      .R?5?   "   ?? 1 ??Name$1 	Presenter??63   #??[ 6?   ??6F   ?? {?6Z   ?? *IsEnabledAndNotGroupMember+6?   2 ?? Triggers2 5?   "   ?? 5?   &   3 ?? Value$3 Weekly??6x   4 ?? Binding4 62   ?? *
Recurrence+#+ [ .a?5?   *   ?? 5 ?? 
TargetName$5 	Presenter??61   # [ 6c    ? ??5?   '   ?? 5?   &   $3 Daily??6x   4 62   ?? *
Recurrence+#+ [ .a?5?   *   ?? $5 	Presenter??61   # [ 6c    ? ??5?   '   ?? 5?   &   $3 Weekdays??6x   4 62   ?? *
Recurrence+#+ [ .a?5?   *   ?? $5 	Presenter??61   # [ 6c    ? ??5?   '   5?      5?      5?      5?      5?      5?      