   M S B A M L   `   `   `  ????  
TestRunner*    $TestRunner.OpenInBrowserOptionDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     ? Title$  Open in Browser??5   	   $	??210??6!   $	??450??6.    ? WindowStyle$ 
ToolWindow=?6:    ? WindowStartupLocation$ CenterOwner=?6S   .??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/splash.png??6    ?? Opacity$
 0.25Q?6T    ? RowDefinitions 5	   
   ?? 5
      ?? 5      $??54 ?6   .+?5   
   ? $??1??6   $??0,5,0,0q?6    ? ColumnDefinitions 5      ?? 5      $
??221* ?6#   ?? 5      $
??178* ?6#   ?? 5      $	??43* ?6#   .+?5      ?? -   5         ??Name$ btnOK??5      $??0??6$      ButtonStyle4#??[  64   $??Right=?6Z   $??25??6v   $??OK??6?   $??	0,12,2,12q?6?   ?? 5      -   5        $ 	btnCancel??5      $??1??6(   #??[  68   $
??Left=?6^   $??25??6z   $??Cancel??6?   $??	2,12,0,12q?6?   5      ]? 5   
   $??0??6   .+?5      ?? -   5         ??Name$ openInBrowserOption??5      $	??Top=?65   $??20,20,20,20q?6M   $??14(?6b   
?????F?$	??Top=?6?   $
??Wrap=?6?   $O??IWould you like to open exported test HTML to Browser after sending email???6?   ?? 5      	 ?? Orientation$	 Vertical=?6   $
??20,5q?60   $??61??6>   $??Bottom=?6J   .+?5      	? -   5        
 	?Name$
 rbEmailAndOpen??5      $??0,0,20,3q?66   $%??Yes, Email and Open in Browser.??6f    d? 	IsChecked$
 TrueY?6?   $??Center=?6?   	? 5      -   5        $
 rbEmailOnly??5      $??0,0,20,3q?63   $??No, Email only.??6`   $ FalseY?6z   $??Center=?6?   5      5      5      5      