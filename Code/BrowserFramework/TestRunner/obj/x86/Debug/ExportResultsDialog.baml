   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	   $    TestRunner.ExportResultsDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestRunner     5   	     ? WindowStartupLocation$  CenterScreen=?5   	    ? Title$ Export Results??5	   	   $	??300??6    $	??300??6-    ? 
ResizeMode$ NoResize=?69   ??5
      ?? 5   
    ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/Splash.png??6    j? Stretch$ UniformToFill=?6P   .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   .+?5   
   ?? -   5         ??Name$ spExportOptions??5   "   $??0??6    ?? Orientation$ Vertical=?6;   $	??100??6R   $y?Visible=?6_   .+?5      ?? -   5        $??lblExportOptions??5      $??10,0,0,0q?6,   $
??Bold"?6>   $y?Visible=?6P   .??6e   Export Options6u   	? 5      -   5         	?Name$ rdTestQueueSummary??5   :   $??20,5,0,0q?6   $??11(?6,   $??Test Queue Summary??6V   6s   	? 5      -   5        $ rdTestResult??5   :   $??20,5,0,0q?6   $??11(?6,   $??Test Result??6P   6f   ?? 5      -   5        	 ??Name$	 cboTest??5   )   $??20,5,0,0q?6   $??20??6R   $	??250??6^   {?.  ??6:   ?? ??5      ?? 5      .R?5      ?? $ 
Horizontal=?6%   .+?5      ?? 
 ??Name$
 tbTestId??6(   $??20??6:   
?????????6W   ?? *TestId+?? 5      $
 
tbTestName??6(   ??6<   ?? *
TestName+5      5      5       5!      ?? 5"   
   $??1??6   $ Vertical=?6"   $	??120??69   .+?5#      ?? $??10,0,0,0q?6   $
??Bold"?6&   .??68   Output Options6H   	? 5$      -   5        $ rdHtml??5$   :   $??20,5,0,0q?6   $??11(?6,   $
??HTML??6J   6Y   	? 5%      -   5        $ rdExcel??5%   :   $??20,5,0,0q?6   $??11(?6,   $??Excel??6K   6[   	? 5&      -   5        $ rdEmail??5&   :   $??20,5,0,0q?6   $??11(?6,   $??Email??6K   6[   ?? 5'      -	   5         ??Name$ txtEmail??5'   (   $??20,5,0,0q?6   $??20??6:   $	??250??6F   {?.  6R   ?? 5(      $??35,2,0,0q?6   $??8(?6*   $??Italic$?67   ?=* Separate each email address with a semicolon (;) No spaces.6?   5)      ?? 5*   
   $??2??6   $ 
Horizontal=?6"   $??Right=?6;   $??50??6W   .+?5+      ?? -
   5         ??Name$ 
btnPublish??5+   '   $??10,0,0,0q?6   $??Publish??6;      ButtonStyle2#??[  6M   6s   ?? 5,      -   5        $ 	btnCancel??5,   (   $??	10,0,10,0q?6   $??Cancel??6;   #??[  6L   6r   5-      5.      5/      