   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TestRunner.AdvancedSchedulerTestRunner.AdvancedScheduler  5   	   B    <TestRunner.AdvancedScheduler.BlacklistTestCheckLoadingDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:TestRunner.AdvancedScheduler     5   	     ? Title$%  BlacklistTestCheckLoadingDialog??5   	   $??65.869??61   $??358.694??6A    ? 
ResizeMode$ NoResize=?6Q    ? ShowInTaskbar .   ? WindowStartupLocation$ CenterScreen=?6?    ? AllowsTransparency .  ? WindowStyle$
 None=?6?   
???????$??1q?6  ??6g   Z? ??6?   Z?  ? DataContext 6$  ??  ?? RelativeSource ?? *Self+ ? 	Resources 5	      ??%    5
      .??5      ? 
???????$??Stretch=?6   $??Stretch=?6<   	 ? RowDefinitions	 5   
   ?? 5      $??28 ?6   ?? 5      $??40 ?6   .+?5   
   ? $??1??6   $??	0,-25,0,0q?6   .+?5      ? -   5        
 ?Name$
 progressBar??5      
??? ? ?$??23??6?   $??Center=?6?   $??Center=?6?   $	??310??6?    ? Value 6C   ??  ?? Path$ TestCheckProgress? ?? Mode$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=?6?   ?? 5       ?? Orientation$ 
Horizontal=?6   $??Center=?62   $??Center=?6O   .+?5      ?? -   5         ??Name$ 
txtPercent??5      $??10(?6?   
????  ? ?? Padding$	 4,0q?6?   $??Center=?6?   $??Center=?6?   ??61   ?? $ TestCheckProgress?$ TwoWay=?$ PropertyChanged=?6?   ?? 5      -   5        $ txtPercentSign??5      $??%??64   $??10(?6=   
????  ?$	 4,0q?6]   $??Center=?6k   $??Center=?6?   5      5      ?? 5   
   $ 
Horizontal=?6   $??1??6.   $??0??6>   $??	10,0,10,0q?6K   .+?5      ?? -   5        $ 
txtLoading??5   H   $	??340??6   $??CharacterEllipsis=?6'   $??10(?6?   
????  ?$	 4,0q?6?   $??Center=?6?   $??Italic$?6  ??6\   ?? $ CurrentItemProcessing?$ PropertyChanged=?$ OneWay=?6  5      5      5      