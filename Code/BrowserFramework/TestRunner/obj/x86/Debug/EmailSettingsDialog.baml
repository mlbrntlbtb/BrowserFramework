   M S B A M L   `   `   `  ????  
TestRunner$    TestRunner.EmailSettingsDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          ? Title$  Email Notification??5   	   $	??250??6$   $	??350??61    ? WindowStartupLocation$ CenterOwner=?6T    ? WindowStyle$ 
ToolWindow=?6x    ? 
ResizeMode$ NoResize=?6?    ? DataContext 5   	   ??  ?? RelativeSource ?? *self+.??5      ? ??5   
   ?? 5       ?? ImageSource$< 6pack://siteoforigin:,,,/Resources/DeltekBackground.png??6    j? Stretch$ UniformToFill=?6^   .+?5
   
   ?? $??Center=?6   .+?5      ?? $??0,20,0,0q?6   $
??Bold"?6*   $??14(?6<   Send the results to:6a   ?? 5      $??0,1,0,0q?6   $??10(?6)   =;Separate each email address with a semicolon (;) No spaces.6u   ?? 5      -   5         ??Name$ txtMailAddresses??5      $	??300??60   $??0,20,0,0q?6<   	 ?? TextWrapping$
	 Wrap=?6N   
 ?? AcceptsReturn
 . " ?? VerticalScrollBarVisibility$
 Auto=?6w   $??80??6?   6?   ?? 5       ?? Orientation$ 
Horizontal=?6   $??Right=?62   $??0,20,0,0q?6N   .+?5      ?? -   5         ??Name$ Ok??5      $??OK??6%    ?? 	IsDefault . $??80??6T   
?????F?
???????6v   ?? 5      -   5        $ Cancel??5      $??Cancel??6)    ?? IsCancel . $??80??6J   $??5,0,0,0q?6U   
?????F?
???????6}   5      5      5      5      