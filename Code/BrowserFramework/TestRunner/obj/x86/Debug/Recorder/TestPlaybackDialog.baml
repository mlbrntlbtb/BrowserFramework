   M S B A M L   `   `   `  ????  
TestRunner!    Recorder.TestPlaybackDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     ? Title$  New Test??5   	   $	??210??6   $	??450??6'    ? WindowStyle$ 
ToolWindow=?63    ? WindowStartupLocation$ CenterOwner=?6L   .??5      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/splash.png??6    ?? Opacity$
 0.25Q?6T    ? RowDefinitions 5	   
   ?? 5
      ?? 5      $??45 ?6   .+?5   
   ? $??1??6   $??0,0,0,0q?6    ? ColumnDefinitions 5      ?? 5      $
??221* ?6#   ?? 5      $
??178* ?6#   ?? 5      $	??43* ?6#   .+?5      ?? -   5         ??Name$ btnOK??5      $??0??6$      ButtonStyle4#??[  64   $??Right=?6Z   $??OK??6v   $??	0,12,2,12q?6?   ?? 5      -   5        $ 	btnCancel??5      $??1??6(   #??[  68   $
??Left=?6^   $??Cancel??6z   $??	2,12,0,12q?6?   5      ]? 5   
   $??0??6   .+?5      ?? -   5         ??Name$ txtInstructions??5      $	??Top=?61   $??
20,20,20,5q?6I   $??11(?6]   $
??Wrap=?6k   
?????F?$	??Top=?6?   $??38??6?   ?? 5      	 ?? Orientation$	 Vertical=?6   $
??20,5q?60   $??61??6>   $??Bottom=?6J   .+?5      	? -   5        
 	?Name$
 
rbPlayback??5      $??0,0,20,5q?62   $??11(?6^   $=??7Save test and run until the last step before recording.??6l    d? 	IsChecked$
 TrueY?6?   $??Center=?6?   	? 5      -   5        $
 rbManual??5      $??0,0,20,5q?60   $??11(?6Z   $G??AKeep the existing steps but do not run the test before recording.??6h   $ FalseY?6?   $??Center=?6?   	? 5      -   5        $
 rbClear??5      $??0,0,20,5q?6/   $??11(?6X   $Q??KClear the existing steps. I would like to start the recording from scratch.??6f   $ FalseY?6?   $??Center=?6?   5      5      5      5      