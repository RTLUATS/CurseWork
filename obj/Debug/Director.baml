   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	       CurseWork.Director   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � 
ResizeMode$  NoResize=�5   	   
�������$��DemiBold"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	      MaterialDesignPaper#���   5   	     MaterialDesignFont#���  5   	    � Title$ Director��5   	   $	��450��6   $	��800��6'   .��5      � 
���   �.+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��TableIngredients��5      {�.  $y�Hidden=�6=      
IsReadOnly . $��277,10,244,10q�6c     5   
   -   5        $��TableOrders��5      {�.  $y�Hidden=�68    . $��0,10,522,10q�6^     5   
   -   5        $��CommonTable��5      {�.  $y�Hidden=�68    . $��10,10,249,10q�6^   �� 5   
   -   5        $��Income��5      
�������$y�Hidden=�61   $��Доход:��6E   $��550,256,169,131q�6V   �� 5   
   -   5        $��Expenses��5      
�������$y�Hidden=�63   $��Расход:��6G   $��550,295,169,92q�6Y   �� 5   
   -   5        $��CommonIncome��5      $y�Hidden=�6(   $��16(�6<   
�������$��625,256,10,131q�6]   �� 5   
   -   5        $��CommonExpenses��5      $y�Hidden=�6*   $��16(�6>   
�������$��625,295,10,92q�6_   �� 5   
   -   5        $
��Date��5      {�.  $y�Hidden=�6Z   $��550,183,10,188q�6o   
�������$��50��6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Период��6�    & "MaterialDesignFloatingHintComboBox#��[ 6�     SecondaryAccentBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �  6  .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ Inverted=�6e  .@�5      �� .��5      �� $��За всё время��6   5      �� 5      .��5      �� $$��За прошедший год��6   5       �� 5!      .��5"      �� $��За полгода��6   5#      �� 5$      .��5%      �� $��За 3 месяца��6   5&      �� 5'      .��5(      �� $��За месяц��6   5)      �� 5*      .��5+      �� $��За неделю��6   5,      �� 5-      .��5.      �� $��За 3 дня��6   5/      �� 50      .��51      �� $��За вчера��6   52      53      �� 54   
   $��560,49,10,308q�6   
�������$) #Просматреть отчёты��6A   #��[ 6u   # �  6�   $ Inverted=�6  $��64��67  .@�55      �� -	   5        $��Статистика��55      6*   �� 56      -
   5        $,��&Посмотреть все блюда��56      64   �� 57      -   5        $,��&Блюда, которые в Меню��57      65   �� 58      -   5        $8��2Потребность в ингредиентах��58      6:   59      5:      5;      