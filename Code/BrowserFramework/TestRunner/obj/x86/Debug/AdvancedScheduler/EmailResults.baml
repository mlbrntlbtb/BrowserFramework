   M S B A M L   `   `   `  ????  
TestRunner/    )TestRunner.AdvancedScheduler.EmailResults   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          ? Title$  Email Results??5   	   $	??400??6   $	??600??6,    ? WindowStyle$ 
ToolWindow=?68    ? WindowStartupLocation$ CenterOwner=?5   	    ? 
ResizeMode$ NoResize=?6-   .??5      ? ???	#FF90D2FF ? ColumnDefinitions 5   
   ?? 5      $??10 ?6   ?? 5	      ?? 5
      $??10 ?6    ? RowDefinitions 5   
   ?? 5      $??5 ?6   ?? 5      ?? 5      $??5 ?6   .+?5   
   ? $??1??6   $??1??6    5       5      ?? 5      $??70 ?6    ?? 5      $??90 ?6    ?? 5      ?? 5      $??30 ?6    .+?5      ?? $??0??6   $??Sending Options??6$   $??0,5,0,5q?6=   .??5      ?? $??Center=?6   $??10,0,0,0q?68   .+?5      	? -   5         	?Name$ rdoSendAsBody??5   "    d? 	IsChecked$
 TrueY?69   $??11(?6J   .??5      ?? Send as email body ?? 5       
???????	[DEFAULT]6E   5!      5"      ?? 5#      $??2??6    	? 5$      -   5        $ rdoSendAsAttachment??5$   "   $??11(?6?   .??6M   Send as attachment6a   5%      5&      ?? 5'      $??1??6   $??Subject??6$   $??0,0,0,5q?65   .??5(      ?? -   5         ??Name$ 
txtSubject??5(      $??0,5,0,5q?6.   	 ?? TextWrapping$
	 Wrap=?6?   6U   5)      ?? 5*      $??2??6   $??
Recipients??6$   $??0,0,0,5q?68   .??5+      ]? .+?5,      ?? $??10(?6    $??Italic$?6.   $??Bottom=?6A   ?=* Separate each email address with a semicolon (;) No spaces.6?   ?? 5-      -   5        $ txtRecipients??5-      $??0,5,0,5q?65   $
	 Wrap=?6F   6\   5.      5/      ?? 50      $??3??6   
 ?? Orientation$
 
Horizontal=?6&   $??Right=?6?   $??0,0,10,0q?6[   .+?51      ?? -   5         ??Name$ btnSend??51         ButtonStyle2#??[  6*   .??6f   Send6l   ?? 52      -   5        $ 	btnCancel??52      #??[  6,   .??6j   Cancel6r   53      54      55      56      