   M S B A M L   `   `   `  ????   
TestRunner    Recorder.AddVariable   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     ? Title$  Assign Variable??$	??300??$	??300?? ? WindowStyle$ 
ToolWindow=? ? WindowStartupLocation$ CenterOwner=? ? 
ResizeMode$ NoResize=?.???? $??
10,5,10,10q????	#FF90D2FF.@??? $y?Hidden=?.???  ? RowDefinitions ?? $??45 ??? $??30 ??? $??30 ??? $	??100 ??? .+??? $??0?? ?? Orientation$ 
Horizontal=?$??Center=?.+??? $??Center=?.??Name :?? -    ??Name$ cboName??$??Center=?$??25??$	??198?? ?? 
IsEditable .  ?? DisplayMemberPath$
 Name?????? ?? $ 
Horizontal=?$??4??$??Right=?$??0,0,10,0q?.+??? -   	 ??Name$	 btnReset??   ButtonStyle2#??[  $??Center=?.??Reset?? -   $	 btnOK??#??[  $??Center=?.??OK?? $??1??$ 
Horizontal=?$??Center=?.+??? $ 
Horizontal=?.+??? $??Center=?.??Start Index :?? $ 
Horizontal=?.+??? -   
 ??Name$
 txtStartIndex??$??35??$??18??$??9(?????  ?? Path$ 
StartIndex? ?? RelativeSource ?? *FindAncestor+ ?? AncestorType" ? ?? Mode$ TwoWay=? ?? NotifyOnSourceUpdated .  ?? UpdateSourceTrigger$ PropertyChanged=??? $ Vertical=?$??Center=?.+??? -   $	 btnSIUp??  FlatButtonStyle#??[ $??9??$??18??$??0,0,0,0q?.???? $??Center=?$??Center=?$??7(?▲?? -   $	 	btnSIDown??#??[ $??9??$??18??.???? $??Center=?$??Center=?$??7(?▼?? $??12???? $ 
Horizontal=?.+??? $??Center=?.??
Length :?? $ 
Horizontal=?.+??? -   $
 	txtLength??$??35??$??18??$??9(????? $ Length? ?? *FindAncestor+" ?$ TwoWay=? . $ PropertyChanged=??? $ Vertical=?$??Center=?.+??? -	   $	 btnLUp??#??[ $??9??$??18??$??0,0,0,0q?.???? $??Center=?$??Center=?$??7(?▲?? -
   $	 btnLDown??#??[ $??9??$??18??.???? $??Center=?$??Center=?$??7(?▼?? $??2??$??Bottom=?$??5,0,0,0q?.??Current Value :?? -   $
 txtCurrentValue??$??3??$??	15,0,11,4q? ?? 
IsReadOnly .  ?? TextWrapping$
 Wrap=?" ?? VerticalScrollBarVisibility$
 Auto=????? $ CurrentValue? ?? *FindAncestor+" ? . $ PropertyChanged=?.?????? .???  ?? Property ? ?? ?? Value# ? ????  ? ??# ? ??.??