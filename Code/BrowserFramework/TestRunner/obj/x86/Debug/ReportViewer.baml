   M S B A M L   `   `   `  ????e  aDe.TorstenMandelkow.MetroChart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d5b8d1bbc99c4a2awTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChartDe.TorstenMandelkow.MetroChart  5   	   E ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:TestRunner.CommonTestRunner.Common 5   	      TestRunner.ReportViewer   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   fchartTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChart     5   	   2commonclr-namespace:TestRunner.Common     5   	   -   5          ? Title$  ReportViewer??5   	   $	??720??6   $
??1280??6+   ???	#FF90D2FF ? WindowStartupLocation$ CenterOwner=?5        ? 	Resources 5
      1  +TestRunner.Common.DlkReportTemplateSelector   reportSelectorA   ;De.TorstenMandelkow.MetroChart.ResourceDictionaryCollection  StatusPalette  Key ?? Color  ExecutionSummaryChart-   'De.TorstenMandelkow.MetroChart.PieChart.   (De.TorstenMandelkow.MetroChart.ChartBase   
ChartTitle   Palette   ChartSubTitle ?? Path   Series0   *De.TorstenMandelkow.MetroChart.ChartSeries	   SeriesTitle
   DisplayMember   ValueMember??%t  &	        &	      &	 >    7    5   
   6+     5   
   ?? 5      ?? 5      
Brush1 $ 	#FFADFF2F??61   5      ?? 5      ?? 5      
Brush2 $ 	#FFCD5C5C??61   5      ?? 5      ?? 5      
Brush3 $ 	#FF5F9EA0??61   5      ?? 5      ?? 5      
Brush4 $ 	#FF808080??61   5      5      ?? 5   
   .R?5        $??*??5      $??*??6   $ Execution Summary??5      8    5      ?? $ SummaryDate? 5"        $	 Status??5#      $
 Status??5$      $ Number??5%      ??5&      ?? $ SummaryResults?5(      5)      5*      .??5+      ?  ? RowDefinitions 5,   
   ?? 5-      $??50 ?6   ?? 5.      $	??300 ?6   ?? 5/      $??* ?6    ? ColumnDefinitions 51   
   ?? 52      $??* ?6   .+?54   
   ??  ?? Orientation$ 
Horizontal=?6   $??Center=?6.   $??0??6I   $??0??6V   .+?55      ?? $??	Machine: ??6   ?? 56      -   5         ??Name$ 
cboMachine??56       ?? Text$	 All??6+    ?? 
IsEditable . $	??150??6H   $??20??6T   57      ?? 58   
   -   5         ??Name$ Charts??58      $??1??6?   $??0??6L   $??5q?6\   #??[  6g   ??6'   ?? 6g   ?? 59   
   $ 
Horizontal=?6   $??Center=?6.   $??1??6K   .+?5:        -   5          Name$ chartSummary??5;      $	??300??5<      $	??300??6   $??1??6*   $ Execution Summary??5=      # [ 5?       5>      ?? $ SummaryDate? 5A        $	 Status??5B      $
 Status??5C      $ Number??5D      ??5E      ?? $ SummaryResults?5G      7   1De.TorstenMandelkow.MetroChart.StackedColumnChart  5H      -   5          Name$ 
chartSuite??5I      $	??450??5J      $	??300??6   $??5,0,0,0q?6*   $ Suite Results??5K      # [ 5M       5L      ?? $ SummaryDate?   SeriesSource 5N      ?? $ SuitesResults?   SeriesTemplate 5O      ?? 5P      .R?5Q        $
 Status??5S      $ Number??5T      	 5R      ?? $ Suite???5U      ?? $ SuiteResults?5V      5X        5Y      -   5        $ chartHistory??5Z      $	??450??5[      $	??300??6   $??5,0,0,0q?6*   $ Execution History??5\      # [ 5^       5]      ?? $ 
ReportDate? 5_      ?? $ HistoryRecords? 5`      ?? 5a      .R?5b        $
 Status??5d      $ Number??5e      	 5c      ?? $
 Date???5f      ?? $ HistoryResults?5g      5i      5j      &   System.Windows.Controls.DataGrid  5k   
   -   5          Name$ dgExecutions??5k      $??2??6)   $??0??66   $??5,10,5,5q?6F     DataGridStyle1#??[ 5l      $   HorizontalScrollBarVisibility$ Hidden=?6;     DataGridRowStyle1   RowStyle# [ 5m   ?   ??6   ?? $ SuitesResults?   Columns 5n      0  *System.Windows.Controls.DataGridTextColumn  5o      ,	  &System.Windows.Controls.DataGridColumn 	  CanUserSort .   	  CanUserReorder .   	  CanUserResize .   	  Header$ Suite??6?     DataGridColumnHeaderStyle1 	  HeaderStyle# [ 6?     	  
IsReadOnly  . ! 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$	! 400
 6?   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 6%   ?? *Suite+6?     5p       .   .   .  $ Passed??6?   # [ 6?     . $! *
 6?   " 6%   ?? *Passed+6?     5q       .   .   .  $ Failed??6?   # [ 6?     . $! *
 6?   " 6%   ?? *Failed+6?     5r       .   .   .  $ NotRun??6?   # [ 6?     . $! *
 6?   " 6%   ?? *NotRun+6?   5t      5u      5v      