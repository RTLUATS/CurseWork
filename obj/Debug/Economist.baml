   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	       CurseWork.Economist   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � 
ResizeMode$  NoResize=�5   	   
�������$��DemiBold"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	      MaterialDesignPaper#���   5   	     MaterialDesignFont#���  5   	    � Title$ 	Economist��5   	   $	��650��6   $
��1000��6(   .��5      � 
���   �.+�5   
    � -   5        $��Frame��5      $��10,10,302,10q�6   �� 5   
   -   5        $��LabelExpenses��5      
�������$��DemiBold"�68   $��17(�6N   $��697,369,141,207q�6\   .��6u   Сумма расходов:6�   �� 5   
   -   5        $��LabelIncome��5      
�������$��DemiBold"�66   $��17(�6L   $��695,471,143,105q�6Z   .��6s   Сумма доходов:6�   �� 5   
   -   5        $��Income��5      
�������$��697,521,143,55q�65   �� 5   
   -   5        $��Expenses��5      
�������$��697,419,143,157q�68   �� 5   
   -   5        $
��Date��5       �� SelectedIndex$ 0��6   $��734,283,10,288q�6Z   
�������$��50��6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Период��6�    & "MaterialDesignFloatingHintComboBox#��[ 6�     SecondaryAccentBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �  6�   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ Inverted=�6P  .@�5      �� 
�������$��За всё время��6,   �� 5      
�������$$��За прошедший год��6+   �� 5      
�������$��За полгода��6+   �� 5      
�������$��За 3 месяца��6+   �� 5      
�������$��За месяц��6+   �� 5      
�������$��За неделю��6+   �� 5      
�������$��За 3 дня��6+   �� 5       
�������$��За вчера��6+   5!      �� 5"   
   -   5        $��Foods��5"   $   
�������$��16(�61   $��Доходы��6?   $��815,10,10,571q�6P   $
��Auto��6{   �� 5#   
   -   5        $��Ingredients��5#   $   
�������$��16(�67   $��Расходы��6E   $��815,89,10,492q�6W   $
��Auto��6�   �� 5$   
   -	   5        $��Report��5$      $��734,167,10,404q�6L   
�������$��50��6{   $ Отчёты��6�   #��[ 6�   # �  6�   $ Inverted=�6B  .@�5%      �� .��5&      �� 
�������$$��Отчёт по доходам��6/   5'      �� 5(      .��5)      �� 
�������$&�� Отчёт по расходам��6/   5*      5+      5,   	   5-      