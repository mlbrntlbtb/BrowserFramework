   M S B A M L   `   `   `  ????E  ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:TestRunner.RecorderTestRunner.Recorder  5   	   %    TestRunner.Recorder.TestCapture   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6recorder!clr-namespace:TestRunner.Recorder     5   	   -   5          ? Title$  Test Capture??5   	   $	??550??6   $	??750??6.   $	??720??6>   $
??1280??6K    ? WindowStartupLocation$ CenterOwner=?6X    ? 	Resources 5      0   *TestRunner.Recorder.DlkEditingStateChecker 
  IsEdit  	BoolToVis??%:   &	        &	        5   
   6*   ?? 5   
   6'   5	      .??5
      ? ??5   
   ?? 5       ?? ImageSource$2 ,pack://siteoforigin:,,,/Resources/splash.png??6    j? Stretch$ UniformToFill=?6T   .+?5   
   ]? -   5        $??
dockPanel1??5   ;   $??24??6   $
??Left=?6    $	??Top=?6M   $	??Top=?6e   .+?5      o? -   5        $??menuBar??5      $??23??6   ??5      ?? 5      .@?5      m? $
??File??6   .@?5      m? -   5         m?Name$ 	mniImport??5      $??Import??62   6B   m? 5      -   5        $ 	mniExport??5      $??Export??62   6B   m? 5      -   5        $ mniClose??5      $??Close??61   6@   5      m? 5      $??Action??6   .@?5      m? -   5        $ 	mniRecord??5      $??Record??62   6B   m? 5      -   5        $ mniPause??5      $??Pause??61   6@   m? 5      -	   5        $ mniStop??5      $
??Stop??60   6>   5      m? 5      $
??Help??6   .@?5       m? -
   5        $ mniHelp??5       $ ??View quick reference guide??60   6T   5!      5"      5#      ]? 5$   
   ??. $??10 30q?6)   .+?5%      ? -   5        $??TCGrid??5%       ? RowDefinitions 5&      ?? 5'      -   5         ?Name$ R0??5'   $   $	??500 ?6.   ?? 5(      -   5        $ R1??5(   $   $??5 ?6.   ?? 5)      -   5        $ R2??5)   $   $??* ?6.   .+?5+      ]? $??Stretch=?6   $??Bottom=?6:   $??2??6R   $??0 8 0 0q?6_   .+?5-      ?? .@?5.      ?? $??Action Logs??6"    ? ToolTip$# Displays info on user actions??67   .??5/      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 	dgActions??5/   '   $??0??6R   "
   VerticalScrollBarVisibility$
 Visible=?6b     DataGridStyle1#??[ 50   "     DataGridRowStyleNoSelect   RowStyle# [ 6J     DataGridCellStyleNoSelect   	CellStyle# [ 6   $   HorizontalScrollBarVisibility$ Hidden=?51   "      SelectionMode$ Extended=?52   "   $??Stretch=?6<   $
??Auto??6Y      RowHeaderWidth$ 0Q?6g      
IsReadOnly . ???	#FF90D2FF??5/   :   ??    Columns 54   "   0  *System.Windows.Controls.DataGridTextColumn  55   &   ,  &System.Windows.Controls.DataGridColumn   CanUserSort .     CanUserReorder .     CanUserResize .     Header$ Block??6?     DataGridColumnHeaderStyle0   HeaderStyle# [ 6?      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   ?? *mBlock+6?     56   &    .   .   .  $ Action??6?     DataGridColumnHeaderStyle3# [ 6?   $ 80 6?    69   ?? *	mAction+6?     57   &    .   .   .  $ Screen??6?     DataGridColumnHeaderStyle2# [ 6?   $ * 6?    69   ?? *mScreenTarget+6?     58   &    .   .   .  $ Target Type??6?   # [ 6?      MinWidth$	 100Q?6?   $ * 6?    69   ?? *mTargetType+6?     59   &    .   .   .  $ Descriptor Type??6?   # [ 6?   $	 100Q?6?   $ * 6?    69   ?? *	mDescType+6?     5:   &    .   .   .  $ Descriptor Value??6?   # [ 6?   $	 150Q?6?   $ * 6?    69   ?? *
mDescValue+6?     5;   &    .   .   .  $ Value??6?   # [ 6?   $	 100Q?6?   $ * 6?    69   ?? *mValue+6?   5=      5?      ?? 5@      $??Output Variables??6"   $1 +Select output variable to use in the script??6<   .??5A        -   5        $	 dgVariables??5A   '   $??0??6T   $
 Visible=?6d   #??[ 5B   &   # [ 6N   # [ 6?   $ Hidden=?5C   &   $ Extended=?5D   &   $??Stretch=?6@   $
??Auto??6]   $ 0Q?6k    . ???	#FF90D2FF??5A   <   ??  5F   "   4  .System.Windows.Controls.DataGridTemplateColumn  5G   &   $ Active??6=    .   .   .    DataGridColumnHeaderStyle6# [ 6?   $ 90 6?      CellTemplate 5H   *   ?? 5I   .   IsOpenDataTemplate??.R?5J   2   ?? -   5        $??Center=?5J   ;   $??Center=?6s    d? 	IsChecked 6?   ?? *InUse+ ?? Mode$ TwoWay=? ?? NotifyOnSourceUpdated .  ?? UpdateSourceTrigger$ PropertyChanged=?5K   /   5M   '     5N   &    .   .   .  $
 Name??6?   # [ 6?   $	 200 6?    69   ?? *Name+6?     5O   &    .   .   .  $ Current Value??6?   # [ 6  $ * 6;   69   ?? *CurrentValue+$ OneWay=? . $ PropertyChanged=?6;  5Q      5R      5S      5U      ?? 5V      -   5         ??Name$ horizontalSplitter??5V      $??1??6;     ?? ShowsPreview  .  $
??Auto??6]   
???????$??Stretch=?6   $??1,0,0,1q?5W   +   $??Stretch=?6E   ! J? Opacity$! 0Q?6c   6o   ]? 5X      .+?5Z      ?? $	??Top=?6!   $??0??66   .@?5[      ?? $??Script??6"   $y?	Collapsed=?62   .??5\      ]? $??Stretch=?6(   $	??Top=?6F   .+?5]   "   ?? $??
-5,-7,0,-2q?6-   $	??Top=?6A   
?????? .@?5^   &   ?? $y?	Collapsed=?6.   .??5_   *   c? -   5        $
??Left=?5_   2   
?????? " `? IsLocked" . $??-3,-3,-3,-3q?6?   .@?5a   .   ?? .+?5b   2   c? -   5        # c? IsOverflowOpen# .  $
??Left=?5b   Q   
?????? " . .@?5c   6   ?? -   5        $ ??Name$$ btnStartRecording??5c   s     	 TestEditorToolBarButtonStyle#??[	 6=   $ Record??6?   .??5d   :   ?? % ?? Orientation$% Vertical=?6E   .+?5e   >   ?? -   5        $??recImage??5e   D   $7??1pack://siteoforigin:,,,/Resources/recordstart.ico??6U   $??16??6?   $??16??6?   ?? 5f   >   -   5        $??recordLabel??5f   H   $??Center=?6[   $??9(?6x   $
??Wrap=?6?   $??SemiBold"?6?   Start6?   5g   ;   5h   7   ?? 5i   6   -   5        $$ btnPauseRecording??5i   s   #??[	 6=   $ Pause??6?   .??5j   :   ?? $% Vertical=?6E   .+?5k   >   ?? $7??1pack://siteoforigin:,,,/Resources/recordpause.png??6D   $??16??6   $??16??6?   ?? 5l   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Pause6?   5m   ;   5n   7   ?? 5o   6   -   5        $$ btnStopRecording??5o   s   #??[	 6=   $
 Stop??6?   .??5p   :   ?? $% Vertical=?6E   .+?5q   >   ?? $6??0pack://siteoforigin:,,,/Resources/stoprecord.png??6D   $??13??6~   $??13??6?   ?? 5r   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Stop6?   5s   ;   5t   7   5u   3   ?? 5v   2   $??Center=?6<   
???????$??9(?6m   	Recording6?   5w   /   ?? 5x   .   
???????$??	0,0,0.6,0q?6N   $	! 0.2Q?6j   $??5,2,5,2q?6x   ?? 5z   .   .+?5{   2   c? -   5        # .  $
??Left=?5{   Q   
?????? " . .@?5?   6   d? -   5        & d?Name$& 	optVerify??5?   C    
 ToggleButtonStyle#??[
 6V   $ Verify??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $1??+pack://siteoforigin:,,,/Resources/check.ico??6D   $??15??6y   $??15??6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Verify6?   5?   ;   5?   7   ?? 5?   6   
???????$??	0,0,0.6,0q?6V   $	! 0.2Q?6r   $??0q?6?   
???   ?6?   ?? 5?   6   6?   o? 5?   6   -   5        ' o?Name$' mnuVerifyTypes??5?   ;   
?????? $??35??6l   $??20??6x   $??-8,0,0,0q?6?   .@?5?   :   m? -   5        $ btnSetVerifyType??5?   C   $??Center=?6|   $??-1q?6?   $ Verify Options??6?   ( m? Icon( 5?   >   ?? 5?   B   $=??7pack://siteoforigin:,,,/Resources/bullet_arrow_down.png??6H   $??35??6?   ) ? ContextMenu) 5?   >   ?? 5?   B   -   5        * ??Name$* cmnuVerifyTypes??5?   N   5?   ;   5?   7   d? 5?   6   -   5        $& optGetValue??5?   C   #??[
 6X   $ Assign Value??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $??Center=?6H   $??13(?6e   $??Italic$?6s   $??	ExtraBold"?6?   
????iA?O{}6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   AssignValue6?   5?   ;   5?   7   ?? 5?   6   
???????$??	0,0,0.6,0q?6V   $	! 0.2Q?6r   $??0q?6?   
???   ?6?   o? 5?   6   -    5        $' mnuGetValueTypes??5?   ;   
?????? $??35??6n   $??20??6z   $??0,0,0,0q?6?   .@?5?   :   m? -!   5        $ btnGetValueType??5?   C   $??Center=?6z   $??-1q?6?   $ Assign Value Options??6?   ( 5?   >   ?? 5?   B   $=??7pack://siteoforigin:,,,/Resources/bullet_arrow_down.png??6H   $??35??6?   ) 5?   >   ?? 5?   B   -"   5        $* cmnuGetValueTypes??5?   N   5?   ;   5?   7   5?   3   ?? 5?   2   $??Center=?6<   
???????$??9(?6m   	Keyword6?   5?   /   ?? 5?   .   
???????$??	0,0,0.6,0q?6N   $	! 0.2Q?6j   $??5,2,5,2q?6x   
???   ?6?   ?? 5?   .   .+?5?   2   c? -#   5        # .  $
??Left=?5?   Q   
?????? " . .@?5?   6   ?? -$   5        $$ 
btnEditRow??5?   s   #??[	 6=   $ 	Edit Step??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $8??2pack://siteoforigin:,,,/Resources/documentedit.png??6D   $??16??6?   $??16??6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Edit6?   5?   ;   5?   7   ?? 5?   6   -%   5        $$ 	btnMoveUp??5?   s   #??[	 6=   $ Move Row Up??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $4??.pack://siteoforigin:,,,/Resources/arrow_up.png??6D   $??12??6|   $??12??6?   $??0,0,0,4q?6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Up6?   5?   ;   5?   7   ?? 5?   6   -&   5        $$ btnMoveDown??5?   s   #??[	 6=   $ Move Row Down??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $6??0pack://siteoforigin:,,,/Resources/arrow_down.png??6D   $??12??6~   $??12??6?   $??0,0,0,4q?6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Down6?   5?   ;   5?   7   ?? 5?   6   -'   5        $$ btnDeleteRow??5?   s   #??[	 6=   $ Delete Step??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $2??,pack://siteoforigin:,,,/Resources/delete.ico??6D   $??16??6z   $??16??6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Delete6?   5?   ;   5?   7   ?? 5?   6   -(   5        $$ btnClearScript??5?   s   #??[	 6=   $ 	Clear All??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $2??,pack://siteoforigin:,,,/Resources/eraser.png??6D   $??16??6z   $??16??6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Clear6?   5?   ;   5?   7   5?   3   ?? 5?   2   $??Center=?6<   
???????$??9(?6m   Step6?   5?   /   ?? 5?   .   
???????$??	0,0,0.6,0q?6N   $	! 0.2Q?6j   $??5,2,5,2q?6x   
???   ?6?   ?? 5?   .   .+?5?   2   c? -)   5        # .  $
??Left=?5?   Q   
?????? " . .@?5?   6   ?? -*   5        $$ 	btnImport??5?   s   #??[	 6=   $ Import??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $2??,pack://siteoforigin:,,,/Resources/Import.ico??6D   $??16??6z   $??16??6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Import6?   5?   ;   5?   7   ?? 5?   6   -+   5        $$ 	btnExport??5?   s   #??[	 6=   $ Export??6?   .??5?   :   ?? $% Vertical=?6E   .+?5?   >   ?? $0??*pack://siteoforigin:,,,/Resources/save.png??6D   $??16??6x   $??16??6?   ?? 5?   >   $??Center=?6H   $??9(?6e   $
??Wrap=?6r   $??SemiBold"?6?   Export6?   5?   ;   5?   7   5?   3   ?? 5?   2   $??Center=?6<   
???????$??9(?6m   File6?   5?   /   5?   +   5?   '   5?   #     5?   "   -,   5        $	 dgScript??5?   +   $??0??6U   $
 Visible=?6e   #??[ 5?   "     DataGridRowStyle1# [ 6J     DataGridCellStyle5# [ 6x   $ Hidden=?5?   "   $??Bottom=?6I   $ Extended=?5?   "   $??Stretch=?6<   $
??Auto??6Y   $ 0Q?6g    . ???	#FF90D2FF??5?   =   ??  5?   &     5?   *    .   .  $
 Step??6l   # [ 6z   $ 40Q?6?    5?   .   ?? 5?   2   .R?5?   6   ]? .+?5?   :   ?? + ??Name$+ 	txtParams??6D   $??Stretch=?6t   ??6W   ?? *mStepNumber+6?   5   7   , ?? Triggers, 5  6   ?? 5  :   - ?? Value$
- True??6?   . ?? Binding. 6F   ?? *mStepNumber+/ ?? 	Converter#/ [  .a?5  >   ?? 0 ?? 
TargetName$0 	txtParams??6E   1 ?? Property1 ? ??2 ?? Value
2 ?????6r   ?? 5  >   $0 	txtParams??6E   1 ? ??
2 ?iii?6r   ?? 5  >   $0 	txtParams??6E   1 ? ??$2 Italic$?6q   5  ;   ?? 5  :   $- False??6?   . 6F   ?? *mStepNumber+#/ [  .a?5  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5	  >   $0 	txtParams??6E   1 ? ??
2 ?p?6r   ?? 5
  >   $0 	txtParams??6E   1 ? ??$2 Normal$?6q   5  ;   5  3   5  +     5  *    .   .  $ Execute??6l   # [ 6}   $ 40Q?6?    5  .   ?? 5  2   .R?5  6   ]? .+?5  :   ?? $+ 	txtParams??6D   $??Stretch=?6q   ??6W   ?? *
mExecute+6?   5  7   , 5  6   ?? 5  :   $
- True??6?   . 6F   ?? *mStepNumber+#/ [  .a?5  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5  >   $0 	txtParams??6E   1 ? ??
2 ?iii?6r   ?? 5  >   $0 	txtParams??6E   1 ? ??$2 Italic$?6q   5  ;   ?? 5  :   $- False??6?   . 6F   ?? *mStepNumber+#/ [  .a?5  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5  >   $0 	txtParams??6E   1 ? ??
2 ?p?6r   ?? 5  >   $0 	txtParams??6E   1 ? ??$2 Normal$?6q   5   ;   5"  3   5$  +     5%  *    .   .  $ Screen??6l   # [ 6|   $	 100Q?6?    5&  .   ?? 5'  2   .R?5(  6   ]? .+?5)  :   ?? $+ 	txtParams??6D   $??Stretch=?6p   ??6W   ?? *	mScreen+6?   5*  7   , 5+  6   ?? 5,  :   $
- True??6?   . 6F   ?? *mStepNumber+#/ [  .a?5-  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5.  >   $0 	txtParams??6E   1 ? ??
2 ?iii?6r   ?? 5/  >   $0 	txtParams??6E   1 ? ??$2 Italic$?6q   50  ;   ?? 51  :   $- False??6?   . 6F   ?? *mStepNumber+#/ [  .a?52  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 53  >   $0 	txtParams??6E   1 ? ??
2 ?p?6r   ?? 54  >   $0 	txtParams??6E   1 ? ??$2 Normal$?6q   55  ;   57  3   59  +     5:  *    .   .  $ Control??6l   # [ 6}   $	 280Q?6?   $ * 6?    5;  .   ?? 5<  2   .R?5=  6   ]? .+?5>  :   ?? $+ 	txtParams??6D   $??Stretch=?6q   ??6W   ?? *
mControl+6?   5?  7   , 5@  6   ?? 5A  :   $
- True??6?   . 6F   ?? *mStepNumber+#/ [  .a?5B  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5C  >   $0 	txtParams??6E   1 ? ??
2 ?iii?6r   ?? 5D  >   $0 	txtParams??6E   1 ? ??$2 Italic$?6q   5E  ;   ?? 5F  :   $- False??6?   . 6F   ?? *mStepNumber+#/ [  .a?5G  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5H  >   $0 	txtParams??6E   1 ? ??
2 ?p?6r   ?? 5I  >   $0 	txtParams??6E   1 ? ??$2 Normal$?6q   5J  ;   5L  3   5N  +     5O  *    .   .  $ Keyword??6l   # [ 6}   $	 180Q?6?    5P  .   ?? 5Q  2   .R?5R  6   ]? .+?5S  :   ?? $+ 	txtParams??6D   $??Stretch=?6q   ??6W   ?? *
mKeyword+6?   5T  7   , 5U  6   ?? 5V  :   $
- True??6?   . 6F   ?? *mStepNumber+#/ [  .a?5W  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5X  >   $0 	txtParams??6E   1 ? ??
2 ?iii?6r   ?? 5Y  >   $0 	txtParams??6E   1 ? ??$2 Italic$?6q   5Z  ;   ?? 5[  :   $- False??6?   . 6F   ?? *mStepNumber+#/ [  .a?5\  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5]  >   $0 	txtParams??6E   1 ? ??
2 ?p?6r   ?? 5^  >   $0 	txtParams??6E   1 ? ??$2 Normal$?6q   5_  ;   5a  3   5c  +     5d  *    .   .  $ 
Parameters??6l   # [ 6?   $	 200Q?6?   $ * 6?    5e  .   ?? 5f  2   .R?5g  6   ]? .+?5h  :   ?? $+ 	txtParams??6D   $??Stretch=?6y   ??6W   ?? *mParameterString+6?   5i  7   , 5j  6   ?? 5k  :   $
- True??6?   . 6F   ?? *mStepNumber+#/ [  .a?5l  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5m  >   $0 	txtParams??6E   1 ? ??
2 ?iii?6r   ?? 5n  >   $0 	txtParams??6E   1 ? ??$2 Italic$?6q   5o  ;   ?? 5p  :   $- False??6?   . 6F   ?? *mStepNumber+#/ [  .a?5q  >   ?? $0 	txtParams??6E   1 ? ??
2 ?????6r   ?? 5r  >   $0 	txtParams??6E   1 ? ??
2 ?p?6r   ?? 5s  >   $0 	txtParams??6E   1 ? ??$2 Normal$?6q   5t  ;   5v  3   5x  +   5  #   5?     5?     5?     5?     5?     5?     ?? 5?  
   --   5        3 ??Name$3 	StatusBar??5?     $??Stretch=?6(   $??25??6F   $??	0,480,0,0q?6R   $??Bottom=?6e   $
??Auto??6?   
???????.@?5?     ?? -.   5        $+ Status??5?     $??11(?6(   $
??Bold"?66   ?? 5?     -/   5        $+ 	SubStatus??5?     $??11(?6+   5?     5?     5?     