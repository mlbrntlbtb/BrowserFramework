   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:TestRunner.CommonTestRunner.Common  5   	   "    TestRunner.ImportDestination   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2commonclr-namespace:TestRunner.Common     5   	   -   5          ? Title$  Import Destination??5   	   $	??300??6$   $	??300??61   $	??300??6=   $	??300??6M   ???	#FF90D2FF ? WindowStartupLocation$ CenterOwner=?5       .??5      ?  ? RowDefinitions 5   
   ?? 5	      ?? 5
      $??40 ?6   .+?5   
   R? -   5         R?Name$ tvwFoldersExisting??5      $??0??6/   $??5,5,5,5q?6T    J? RenderTransformOrigin$ -0.747,0.508&?6e   ??6<   ?? ??5      ?? 5       ?? 
TargetType" Q?6   .?5      ??  ?? Property ?@Q?
IsExpanded ?? Value . 63   ?? 5       ?@Q?
IsSelected 63   ??  ?? Path$ 
IsSelected?	 ?? Mode$	 TwoWay=?5      
 ? 	Resources
 5       ?? DataType    TestRunner.Common.BFFolder ?? ItemsSource ?? Orientation ? ToolTip??%1  (??       5   +   *, +)?? 6   "  6+    6O   ?? *
DirItems+.R?5      ?? $ 
Horizontal=?6!   .+?5      ?? $??Stretch=?68   $??16??6T   $??16??6_   ??6    ?? *Icon+6_   ?? 5      $??3,0,0,0q?6S   ??6$   ?? *Name+ 6:   ?? *Path+6S   5      5      5      5      ?? 5   
   $ 
Horizontal=?6   $??2??6.   $??Right=?6;   $??0,0,10,0q?6W   .+?5      ?? -   5         ??Name$ 	btnImport??5         ButtonStyle2#??[  6(   $??Import??6N   6_   ?? 5      -   5        $ 	btnCancel??5      #??[  6(   $??Cancel??6N    ?? 	IsDefault . 6p   5      5       5!      