   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5	   	   1&clr-namespace:System;assembly=mscorlibSystem 5
   	       CurseWork.ChefViewFood   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   Bcol2clr-namespace:System.Collections;assembly=mscorlib     5	   	   6sys&clr-namespace:System;assembly=mscorlib     5
   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � 
ResizeMode$  NoResize=�5   	      MaterialDesignBody#���   5   	   $��DemiBold"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$
 Food��5   	   $	��450��6   $	��800��6#   .��5      � 
���   �.+�5   
   �� -   5        $��Image��5      $��10,10,376,219q�6   &   System.Windows.Controls.DataGrid  5   
   -   5        $��Table��5         CanUserAddRows .    CanUserDeleteRows . $��10,242,10,10q�6O      CanUserResizeColumns .     AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$5	 /Наименование ингредиента��6�   
 ^� ToolTip$V
 PЗдесь должно быть наименование ингредиента��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ IngredientName� �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�     5      $*	 $Колличество в блюде��6n   $�
 �Если у вас в блюде используется 300 грамм булочки, то вы заносите в таблицу как 0.3 киллограмм, если нпиток то сколько литров напитка например: 0.5 литров��6�    6%   �� $ Weight� . $ TwoWay=�6�   4  .System.Windows.Controls.DataGridComboBoxColumn  5      $$	 В чём измеряется��6)      SelectedValueBinding 6C   �� $
 Unit�$ TwoWay=�   ItemsSource 5      "  System.Collections.ArrayList  5      ��5      л.��6-   ��5      	кг.��6.   5       5"        5#      $8	 2Действия при приготовлении��6~    6%   �� $ CookingSteps�$ PropertyChanged=�$ TwoWay=�6~    � 	Resources 5%      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Property �� Value��%�   '        �� 5&      "  6   .�5'      ��  �@�ToolTip 60   �� *#!ErrorCollection[AdditionalAmount]+5(      5)      5*      �� 5+   
   -   5        $
��Name��5+   W   
������� �� 	MaxLength$ 20��6)    �� 	IsEnabled . )	  #MaterialDesignThemes.Wpf.HintAssist 	  Hint$! Наименование...��6�   $��431,87,209,290q�6�   �� 5,   
   -   5        $��Category��5,   W   
�������$ 20��6)    . $ Категория...��6�   $��431,167,209,213q�6�   �� 5-   
   -   5        $��ChangeImage��5-   H   
�������$��DemiBold"�6$   $��14(�6:   $%��Выбрать картинку��6[   $��431,10,189,370q�6v   $
��Auto��6�   �� 5.   
   -   5        $��AddFood��5.   H   
�������$��DemiBold"�6$   $��14(�6:   $"��Добавить в меню��6W   $��612,167,10,213q�6q   $
��Auto��6�   �� 5/   
   -   5        $��
SaveChange��5/   H   
�������$��DemiBold"�6$   $��14(�6:   $'��!Сохранить измения��6Z   $��612,117,10,263q�6v   $
��Auto��6�   50      51      