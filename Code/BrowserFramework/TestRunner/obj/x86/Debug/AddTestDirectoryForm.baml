   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:TestRunner.CommonTestRunner.Common  5   	   %    TestRunner.AddTestDirectoryForm   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2commonclr-namespace:TestRunner.Common     5   	   -   5          ? Title$  
Add Folder??5   	   $	??460??6   $	??500??6)   $	??300??65   $	??300??6E   ???	#FF90D2FF ? WindowStartupLocation$ CenterOwner=?5        ? 
ResizeMode$ NoResize=?6D   .??5      ?  ? RowDefinitions 5   
   ?? 5	      $	??250 ?6   ?? 5
      $??35 ?6   ?? 5      $	??100 ?6   ?? 5      $??40 ?6   .+?5   
   R? -   5         R?Name$ tvwFoldersExisting??5      $??0??6/   $??5,5,5,5q?6T    J? RenderTransformOrigin$ -0.747,0.508&?6e   ??6<   ?? ??5      ?? 5       ?? 
TargetType" Q?6    ?? Triggers 5      ?? 5       ?? Binding 6&   ?? *
IsExpanded+	 ?? Value	 True6E   .a?5      ?? 
 ?? Property
 ?@Q?
IsExpanded ?? Value . 6;   5      .?5      ?? 
 ?@Q?
IsSelected 63   ??  ?? Path$ 
IsSelected? ?? Mode$ TwoWay=?5       ? 	Resources 5       ?? DataType    TestRunner.Common.BFFolder ?? ItemsSource ?? Orientation ? ToolTip??%1  (??       5   +   *, +)?? 6   "  6+    6O   ?? *
DirItems+.R?5      ?? $ 
Horizontal=?6!   .+?5      ?? $??Stretch=?68   $??16??6T   $??16??6_   ??6    ?? *Icon+6_   ?? 5      $??3,0,0,0q?6S   ??6$   ?? *Name+ 6:   ?? *Path+6S   5      5      5       5!      ?? 5"   
   $??1??6   $ 
Horizontal=?6"   $
??Left=?6;   $??0,0,10,0q?6V   .+?5#      ?? $??Center=?6   .??6/   Folder Name :6>   ?? 5$      -   5         ??Name$ txtFolderName??5$      $??Center=?6-   $??25??6H   $	??400??6T   6?   5%      ?? 5&   
   $??2??6   $ 
Horizontal=?6"   $
??Left=?6;   $??0,0,10,0q?6V   .+?5'      ?? $??Center=?6   .??6/   Folder Path :6>   ?? 5(      -   5        $ txtCurrentPath??5(      $??8,0,0,0q?6.   $??Center=?6?   $	??100??6Z   $	??400??6g   {?.   ?? TextWrapping$ WrapWithOverflow=?6?   .??6?   5)      5*      ?? 5+   
   $ 
Horizontal=?6   $??3??6.   $??Right=?6;   $??0,3,10,0q?6W   .+?5,      ?? -   5         ??Name$ btnOK??5,         ButtonStyle2#??[  6$   $??OK??6J    ?? 	IsDefault . 6i   ?? 5-      -   5        $ 	btnCancel??5-      #??[  6(   $??Cancel??6N   6_   5.      50      51      