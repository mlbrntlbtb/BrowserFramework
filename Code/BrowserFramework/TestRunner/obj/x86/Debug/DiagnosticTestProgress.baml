   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	   '    !TestRunner.DiagnosticTestProgress   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestRunner     5   	     ? Title$  Diagnostic Test Progress??5   	   $	??450??6*   $	??600??67    ? WindowStartupLocation$ CenterOwner=?6C    ? WindowStyle$ 
ToolWindow=?6g    ? 
ResizeMode$ NoResize=?6?   ??5	      ?? 5
   
    ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6P   .??5      ?  ? RowDefinitions 5   
   ?? 5      $??70 ?6   ?? 5      $	??100 ?6   ?? 5      $	??200 ?6   ?? 5      $??* ?6   .+?5   
   ? $??0??6   $??Stretch=?6   $
??auto??6:   .+?5      ?? $??10,0,0,0q?6   $
??Bold"?6&   .??68   Test Runner Diagnostics Tool6V   ?? 5      $??	10,20,0,0q?6   .??5      ?? -   5         ??Name$ 
tbToolPath??5      $
??Wrap=?60   	Tool Path6O   5      5      ? 5   
   $??1??6   .+?5      ?? -   5         ??Name$ 	spExecute??5      	 ?? Orientation$	 Vertical=?6,   $??Stretch=?6C   $y?Visible=?6_   .+?5      ?? -   5        $??lblTestExecuted??5      $??10,0,0,0q?6/   $??16(?6A   $
??Bold"?6O   ??6a   ?? 
 ?? Path$
 CurrentTestName? ?? UpdateSourceTrigger$ PropertyChanged=? ?? Mode$ OneWay=??? 5      -   5        $??lblTestProgress??5      $??	10,-5,0,0q?6/   ??6B   ?? $
 CurrentTestProcessing?$ PropertyChanged=?$ OneWay=?? 5      -   5        $??pbExecution??5      $??	10,5,10,0q?61   $??30??6D   5      ?? 5      -   5        $ spFinish??5      $	 Vertical=?6+   $??Stretch=?6B   $y?Hidden=?6^   .+?5       ?? -   5         ??Name$ 	lblFinish??5       $??	10,0,10,0q?6+   $??Diagnostic Test Finished??6>   $??16(?6a   $
??Bold"?6o   ?? 5!      .??5"      ?? -   5        $ tbLogLoc??5"   3   $??	10,0,10,0q?6    $????A summarized report will open in your default browser shortly. If the process doesn't start automatically, click the View HTML Report button to launch the report manually.??6E   $
??Wrap=?6?   5#      ?? 5$      -	   5         ??Name$ btnViewReport??5$      $??	10,0,10,0q?60   $??Right=?6C   $	??100??6_   $??View HTML Report??6k      ButtonStyle2#??[  6?   6?   5%      5&      ? 5'   
   $??2??6   .+?5(      ?? -
   5         ??Name$ txtOutputLog??5(      $??	10,0,10,0q?6,   $ ?? HorizontalScrollBarVisibility$ Disabled=?5)      " ?? VerticalScrollBarVisibility$
 Auto=?6?   ??. $	??180??6?    ?? TextWrapping$
 Wrap=?6?    ?? 
IsReadOnly . ??5(   ?   ?? $
 DiagnosticTestOutputLog?$ PropertyChanged=?$ OneWay=?5)   ?   5*      ? 5+   
   $??3??6   .+?5,      ?? $	 
Horizontal=?6   $??Right=?62   .+?5-      ?? -   5        $ 	btnCancel??5-      $??	10,0,10,0q?6,   #??[  6?   $??Cancel??6e   6v   5.      5/      50      51      