   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	       CurseWork.Chef   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          � 
ResizeMode$  NoResize=�5   	      MaterialDesignBody#���   5   	   $��DemiBold"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$
 Chef��5   	   $	��450��6   $	��800��6#   .��5      � 
���   �.+�5   
   �� -   5        $
��Menu��5      $��10,10,289,10q�6   &   System.Windows.Controls.DataGrid  5   
   -   5        $��Table��5   k   $y�Hidden=�6   {�.     CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .  $��	0,0,260,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   CanUserResize .  	   
IsReadOnly	 . 
   Header$
 Название��6l      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6~   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   ��  �� Path$
 Name�6~     5       .  	 . $
 Сколько есть��6l   $	 140 6�    6;   �� $ Count�6�     5       .  	 . $
 Измерение��6k   $	 110 6~    6;   �� $
 Unit�6~     5       .  	 .  $%
 Сколько докупить��6�   $ * 6�    6;   �� $ AdditionalAmount� �� ValidatesOnDataErrors .  �� Mode$ TwoWay=�   	CellStyle 5      �� 5       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6    .�5      ��  �� Property �@�ToolTip �� Value 68   �� $' !ErrorCollection[AdditionalAmount]�5      5!      5#      �� 5$   
   $��539,10,10,10q�6   .+�5%      �� -   5        $��SearchField��5%      $��DemiBold"�6)   
���   �
������� � ToolTip$r lДля показа всех элементов отчистите строку и нажмите Поиск��6j   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$&  Поиск по названию��6�   $��0��6�   $��2��6�   $��Center=�6�   $��10q�6  $��35��6(  �� 5&      -   5        $��Search��5&       " MaterialDesignRaisedDarkButton#��[ 6#   $��16(�6[   $��DemiBold"�6i   $��
Поиск��6   $��10q�6�   �� 5'      -   5        $��AddFood��5'   L   
�������$��DemiBold"�6(   $��16(�6>   $��10q�6[   $!��Добавить блюдо��6g   6�   �� 5(      -   5        $��AllFood��5(   L   
�������$��DemiBold"�6(   $��16(�6>   $��10q�6[   $*��$Посмтреть все блюда��6g   {�.  6�   �� 5)      -   5        $��Inquiry��5)   L   
�������$��DemiBold"�6(   $��11(�6>   $��10q�6\   $B <Запрос необходимых ингредиентов��6h   $B��<Запрос необходимых ингредиентов��6�   {�. 6�   �� 5*      -	   5        $��InquiryExecute��5*   L   
�������$��DemiBold"�6(   $��16(�6>   $��10q�6b   {�.  $!��Сделать запрос��6�   6�   �� 5+      -
   5        $��RequestReport��5+   L   
�������$��DemiBold"�6(   $��15(�6>   $3��-Сделать отчёт о запросах��6a   $��10q�6�   6�   5,      5.      5/      