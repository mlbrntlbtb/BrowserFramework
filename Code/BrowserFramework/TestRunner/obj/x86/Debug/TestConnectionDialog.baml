   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null'clr-namespace:TestRunner
TestRunner  5   	   %    TestRunner.TestConnectionDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestRunner     5   	     ? Title$  Test Connection??5   	   $	??300??6!   $	??380??6.    ? WindowStartupLocation$ CenterScreen=?6:    ? 
ResizeMode$ NoResize=?6_    ? WindowStyle$ 
ToolWindow=?6u   .??5	      ? ??5
   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/splash.png??6    ?? Opacity$
 0.25Q?6T   .+?5   
   ??  ?? Orientation$ Vertical=?6   $??25,10,20,20q?6,   $
??Left=?6A   .+?5      ?? -   5         ??Name$" lblTestConnectionPerformance??5      $????A connection test will perform the following:

- Launch preferred browser



- Check if the application login page is reached
- Attempt a user login with the provided login information

The connection may take 30s to finish.

Would you like to proceed with the test???6:   $??Center=?6{  $??Center=?6?  $	??330??6?  $	??200??6?  $??12(?6?  
?????F?6?  5      ?? 5   
   $ 
Horizontal=?6   $??
44,75,39,0q?6.   $??Center=?6B   $	??291??6_   $	??Top=?6k   $??73??6?   .+?5      ?? -   5         ??Name$ cboTargetBrowser??5      	 ?? 
IsEditable	 .  $	??250??6D   $??26??6P   $	??Top=?6\   
 ?? SelectedIndex$
 0??6t   $??Center=?6?   $??5,0,0,0q?6?   5      ?? 5   
   $ 
Horizontal=?6   $??Center=?6.   }?. $??110,215,80,0q?6_   $	??210??6u   .+?5      ?? -   5         ??Name$ btnYes??5      $	??Yes??6%   
???????
?????F?$??10(?6]   $??80??6k   $??20??6v   $??18,0,5,0q?6?   ?? 5      -   5        $ btnNo??5      $??No??6$   
???????
?????F?$??10(?6[   $??80??6i   $??20??6t   $??5,0,0,0q?6?   5      5      5      