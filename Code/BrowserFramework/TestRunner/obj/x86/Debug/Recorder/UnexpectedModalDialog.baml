   M S B A M L   `   `   `  ????  
TestRunner$    Recorder.UnexpectedModalDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     ? Title$  CustomModalDialog??5   	   $	??300??6#   $??637.313??60    ? WindowStyle$ 
ToolWindow=?6@   .??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/splash.png??6    ?? Opacity$
 0.25Q?6T    ? RowDefinitions 5	   
   ?? 5
      ?? 5      $??55 ?6   .+?5   
   ? $??1??6   $??0,0,0,0q?6    ? ColumnDefinitions 5      ?? 5      ?? 5      .+?5      ?? -   5         ??Name$ btnOK??5      $??0??6$      ButtonStyle4#??[  64   $??Right=?6Z   $??25??6v   $??Accept [OK])??6?   6?   ?? 5      -   5        $ 	btnCancel??5      $??1??6(   #??[  68   $
??Left=?6^   $??25??6y   $??Dismiss [Cancel]??6?   6?   5      ?? 5   
   -   5         ??Name$ txtInstructions??5      $	??Top=?6-   $??0??6E   $??20,20,20,20q?6R   $??14(?6g   $
??Bold"?6u   
?????F?97An unexpected dialog is blocking test script recording!6?   ?? 5   
   -   5         ??Name$ 
txtMessage??5      $??Center=?6&   {?.  $	??100??6S   $??0??6`   $??20,20,20,20q?6m   	 ?? TextWrapping$
	 Wrap=?6?   .??6?   TEXT6?   ?? 5   
   -   5        $ txtFooterTop??5      $??Bottom=?6*   $??0??6E   $??
20,0,20,35q?6R   
???  ??$
??Bold"?6w   NOTE:6?   ?? 5   
   -   5        $ txtFooterMain??5      $??Bottom=?6+   $??0??6F   $??20,35,20,18q?6S   $??Italic$?6h   USYou need to click your preferred action HERE to proceed with test script recording.6?   ?? 5   
   -   5        $ txtFooterEnd??5      $??Bottom=?6*   $??0??6E   $??
20,35,20,0q?6R   $??Italic$?6f   pnTest Capture will attempt to accept [OK] or dismiss [Cancel] the dialog based from the action you will choose.6?   5      5      