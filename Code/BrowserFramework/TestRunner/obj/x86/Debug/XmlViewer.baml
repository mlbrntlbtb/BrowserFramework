   M S B A M L   `   `   `  ????Q  MSystem.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;,clr-namespace:System.Xml;assembly=System.Xml
System.Xml  5      E ATestRunner, Version=3.8.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:TestRunner.CommonTestRunner.Common 5         TestRunner.XmlViewer   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Axmlstack,clr-namespace:System.Xml;assembly=System.Xml     5      1localclr-namespace:TestRunner.Common     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        ? 	Resources  5
      *  $TestRunner.Common.DlkStringToBoolean   StringToBoolean ?? Color  xmlValueBrush  xmAttributeBrush  xmlTagBrush  xmlMarkBrush  attributeTemplate ?? Orientation ?? Path ?? 
TargetType ?? Property ?? Value ?? Mode ?? 	Converter  treeViewTemplate	 ?? ItemsSource
 ?? XPath ??Name ?? Triggers ?? Binding ?? Value   System.Xml.XmlNodeType ?? 
TargetName??%p  &	        &	      &	 6     &	 X     &	 ?     &	 ?     7  7  7  7  'Q? ?    7   &	 ?    7  7  7  7    5   
   6#   ?? 5   
   $
 Blue??6   6'   ?? 5   
   $	 Red??6   6&   ?? 5   
   $ DarkMagenta??6   6.   ?? 5   
   $
 Blue??6   6'   ?? 5   
   .R?5      ?? $ 
Horizontal=?6   $??3,0,0,0q?5      $??Center=?6*   .+?5      ?? 8??  ??6   ?? $
 Name?5      ?? 5      $??="??6   8?? 5      ?? 5      8?? ??6   ?? $ Value?5      ?? 5      $??"??6   8?? 5      5      5      ?? 5   
   " Q?6   .?5       ??  ?@Q?
IsExpanded . 6+   ?? 5!       ?@Q?
IsSelected 6+   ?? $ Attributes[dlkselected]?$ OneWay=?8   5"      ?? 5$   
   	 5%   #   ?? $
 child::node()??.R?5&      ?? $ 
Horizontal=?6   $??3,0,0,0q?62   $??Center=?5'      .+?5(      ?? $ startTag??5*      $??<??5(      $??Center=?6(   8??  5*      ?? 5,      $ xmlTag??5/      $??0q?5-      $??Center=?5.      8?? ??5,      ?? $
 Name?50      ?? 52      8?? $??Center=?55      ??54      ?? $ 
Attributes???56      ?? 57      .R?58      ?? $ 
Horizontal=?6)   59      5;      ?? 5=      $ endTag??5?      $??>??5=      $??Center=?6(   8?? 5?      5@       5B      ?? 5C       6   ?? *
NodeType+ 5D       5E      Text=?65   .a?5G      ?? $ xmlTag??5H       5G   -   ?? *	InnerText+ ? ??5H      ?? 5I      $ xmlTag??5J       ? ??
 ??  ??? 5K      $ startTag??5L       ? y?$ 	Collapsed=?5K   3   5L      ?? 5M      $ endTag??5N       ? y?$ 	Collapsed=?5M   3   5N      5O      ?? 5Q      $ False??6@    6   ?? *HasChildNodes+.a?5R      ?? $ endTag??6;    ? ??$ />??6-   6;   5S      5U      5V      .??5X      ? .+?5Y   
   R? -   5        $??xmlTree??5Y   4   $??2??6   $??2??6    #??[ 5Z      ??5[      ??  ?? UpdateSourceTrigger$ PropertyChanged=?$
 child::node()??5]      5^      5_      