   M S B A M L   `   `   `  ????  
TestRunner(    "TestRunner.Controls.TimePickerCtrl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .??5      ?   ? ColumnDefinitions  5	   
   ?? 5
      $??0.02* ?6   ?? 5      $??0.005* ?6   ?? 5      $??0.02* ?6   ?? 5      $??0.025* ?6   ?? 5      $??0.02* ?6    ? RowDefinitions 5   
   ?? 5      $??10 ?6   ?? 5      $??10 ?6   .+?5   
   ? -   5         ?Name$
 hour??5      |?. $??2??6.   .+?5      ?? -   5         ??Name$ txtHr??5       ?? TextWrapping$
 Wrap=?6%   $??1,1,0,1q?6|    ?? TextAlignment$ Right=?6?   $??Center=?6?    ?? 	MaxLength$ 3??6?    ?? MaxLines$ 1??6?   $??1.9,1,0.1,1q?6?  ??69   ??  ?? Path$ Hours?	 ?? ElementName$	 UserControl??
 ?? Mode$
 Default=?6?  5      ? 5   
   $??1??6   $??2??6    .+?5      ?? -   5        $ txtSeparator1??5      $
 Wrap=?6-   $??Center=?6A   $??0,1,0,1q?6r   $??:??6?   $ Center=?6?    ?? 
IsReadOnly . $??10??6?   $	??0,1q?6?   ??6\   Z? 6?   5      ? 5   
   -   5        $	 min??5       $??2??6   |?. $??2??6>   .+?5      ?? -   5        $ txtMin??5      $
 Wrap=?6(   $??0,1,0,1q?6?   $ Center=?6?   $??Center=?6?   $ 3??6?   $ 1??6   $??1.9,1,0.1,1q?6?  ??6<   ?? $ Minutes?$	 UserControl??$
 Default=? ?? UpdateSourceTrigger$ PropertyChanged=?6?  5      ? 5#   
   -   5        $
 ampm??5#       $??3??6   |?. $??2??6?   .+?5$      ?? -   5        $ txtAMPM??5$      $
 Wrap=?6)   $??0,1,1,1q?6?   $
 Left=?6?   $??Center=?6?   $ 1??6?   $ 2??6?    . $??0.2,1,1.6,1q?6t  ??6=   ?? $
 AMPM?$	 UserControl??$
 Default=? ?? FallbackValue$ AM??6t  5%      ? 5'   
   -   5        $ upDownButtons??5'       $??4??6   |?. .+?5(      ? -	   5        $ upHolder??5(       $??1??6   |?. ???	#FFF9F9F9$??0,0,0,-9q?6Z   .+?5)      ?? -
   5         ??Name$ 	btnUpTime??5)       ? UseLayoutRounding .  $??0,0,0,8q?6^      ButtonTransparentStyle#??[  6o   $??Center=?6?   .??5*      ?? $??Marlett)?6    $??14(?65   56F   5/      50      ? 51      -   5        $ 
downHolder??51       $??2??6   |?. .+?52      ?? -   5        $ btnDownTime??52       .  $??	0,8,0,-10q?6H   #??[  6u   $??Center=?6?   .??56      ?? $??Marlett)?6    $??14(?65   66F   57      58      59      5:      5;      