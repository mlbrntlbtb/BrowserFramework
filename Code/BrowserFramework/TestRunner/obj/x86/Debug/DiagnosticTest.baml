   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	       TestRunner.DiagnosticTest   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestRunner     5   	     ? Title$  Diagnostic Test??5   	   $	??400??6!   $	??600??6.    ? WindowStartupLocation$ CenterOwner=?6:    ? WindowStyle$ 
ToolWindow=?6^    ? 
ResizeMode$ NoResize=?6w   ??5	      ?? 5
   
    ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6P   .??5      ?  ? RowDefinitions 5   
   ?? 5      $??70 ?6   ?? 5      $	??250 ?6   ?? 5      $??* ?6   .+?5   
   ? $??0??6   $??Stretch=?6   $
??auto??6:   .+?5      ?? $??10,0,0,0q?6   $
??Bold"?6&   .??68   Test Runner Diagnostics Tool6V   ?? 5      $??	10,20,0,0q?6   .??5      ?? -   5         ??Name$ 
tbToolPath??5      $
??Wrap=?60   	Tool Path6O   5      5      ? 5   
   $??1??6   $??Stretch=?6   $
??auto??6:   .+?5      ?? $??5,0,0,10q?6   $
??Bold"?6&   .??68   Choose a diagnostic test:6S   ?? 5      ???	#FF90D2FF ?? Orientation$ 
Horizontal=?60   $	??195??6I   $??Stretch=?6V   $??10q?6t   .+?5      ?? $
??Left=?6   $	??300??68   $??5,0,0,0q?6D   .+?5      ?? -   5        $??cbxFull??5      $??5,10,0,5q?6.   $??Full Diagnostic Test??6@   6_   ?? 5      -   5        $??	cbxMinReq??5      $??5,0,0,5q?60   $!??Minimum System Requirements??6A   6g   ?? 5      -   5        $??
cbxCoreDir??5      $??5,0,0,5q?61   $ ??Core Directories and Files??6B   6g   ?? 5      -   5        $??cbxTestScript??5      $??25,0,0,5q?64   $??10(?6F   $??Test Script??6T   6j   ?? 5       -   5        $??cbxTestScriptResult??5       $??25,0,0,5q?6:   $??10(?6L   $??Test Script Result??6Z   6w   ?? 5!      -   5        $??cbxTestSuite??5!      $??25,0,0,5q?63   $??10(?6E   $??
Test Suite??6S   6h   ?? 5"      -   5        $??cbxTestSuiteResult??5"      $??25,0,0,5q?69   $??10(?6K   $??Test Suite Result??6Y   6u   ?? 5#      -	   5        $??cbxInsBrowser??5#      $??5,0,0,5q?64   $-??'Installed Browser and WebDriver Version??6E   6w   ?? 5$      -
   5        $??cbxSharedFolder??5$      $??5,0,0,5q?66   $#??Shared Folder and Permissions??6G   6o   5%      ?? 5&      $??Right=?6   $ Vertical=?69   $??5,0,0,0q?6P   .+?5'      ?? -   5        	 ??Name$	 txtDesc??5'      $	??250??6/   $	??190??6;   $??	2,2.5,5,5q?6H   
 ?? TextWrapping$

 Wrap=?6[   {?.  " ?? VerticalScrollBarVisibility$
 Auto=?6?   5(      5)      5*      ? 5+   
   $??2??6   .+?5,      ?? $ 
Horizontal=?6   $??Right=?62   $??0,0,22,0q?6N   .+?5-      ?? -   5        $??btnStart??5-         ButtonStyle2#??[  6)   {?.  $??Start??6a   6q   ?? 5.      -   5        $??	btnCancel??5.      #??[  6*   $??Cancel??6P   6a   5/      50      51      52      