   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	       CurseWork.Basket   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � 
ResizeMode$  NoResize=�5   	      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ Basket��5   	   $	��600��6   $	��800��6%   .��5      � 
���   �.+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��BasketTable��5      $��	0,0,481,0q�6&      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ 
Номер��6T      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6c   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� 
 �� Path$	
 Num�6c     5       . $ Название��6U   $	 150 6g   	 6%   �� $

 Name�6g     5       . $ Цена��6V   $ 80 6d   	 6%   �� $
 Price�6d   5      �� 5   
   
�������$��DemiBold"�6#   $��24(�69   $��617,0,12,527q�6G   $	��165��6]   .��6i   	Итог:6p   �� 5   
   -   5        $��AllSum��5   K   $��DemiBold"�6   
�������$��24(�6=   $��617,49,12,471q�6Y   $	��165��6p   $��51��6|   �� 5   
   -   5        $��Delete��5   �    �� TextWrapping$
 Wrap=�6   $��DemiBold"�6&   
�������)  #MaterialDesignThemes.Wpf.HintAssist   Hint$D >Введите через пробел номера блюда��6S   $��15(�6�   $��318,69,196,467q�6�   $	��280��6�   $��35��6�   �� 5   
   -   5        $��
MiddleName��5   h   $��DemiBold"�6   
�������$ Фамилия��6?   $��318,194,196,338q�6z   �� 5   
   -   5        $��	FirstName��5   d   $��DemiBold"�6   
�������$ Имя��6?   $��318,259,196,273q�6u   �� 5   
   -   5        $��LastName��5   i   $��DemiBold"�6   
�������$ Отчество��6?   $��318,332,196,200q�6y   �� 5    
   -   5        $��	Telephone��5    h   $��DemiBold"�6   
�������$ Телефон��6?   ;  5MaterialDesignThemes.Wpf.Transitions.TransitionAssist   DisableTransitions . $��318,419,196,113q�6�   �� 5!   
   -   5        $��DeleteFromBasket��5!   9   $��DemiBold"�6   {�.  
�������$��16(�6d   $&�� Убрать из корзины��6r   $��318,112,196,417q�6�   $
��Auto��6�   �� 5"   
   -	   5        $	��Buy��5"   '   $��DemiBold"�6   
�������$��Покупка��6E   $��616,112,12,417q�6W   {�.  $
��Auto��6�   5#      5$      