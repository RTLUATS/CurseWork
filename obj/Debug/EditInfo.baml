   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	       CurseWork.EditInfo   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          � 
ResizeMode$  NoResize=�5   	   
�������$��DemiBold"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	      MaterialDesignPaper#���   5   	     MaterialDesignFont#���  5   	    � Title$ EditInfo��5   	   $	��450��6   $	��800��6'   .��5      � 
���   � � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��Label��5      $��0��6   
�������$��Center=�6>   $��Center=�6Y   $��26(�6v   $7��1Редактирование информации��6�   $	��376��6�   $��218,51,200.667,42q�6�   $��3��6�   $��48��6�   �� 5   
   -   5        $��
MiddleName��5   X   $��1��6    �� 	MaxLength$ 20��6   $��0��6.   $	��Top=�6j   $
��Left=�6�   $	��160��6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Имя��6�   $��
10,102,0,0q�6�   $��27��6�      
HelperText$0 *Только русские символы��6�   �� 5    
   -   5        $��	FirstName��5    X   $��0��6   $ 20��6"   $��1��61   $	��Top=�6i   $
��Left=�6�   $	��160��6�   $��10,0,0,0q�6�   $ Фамилия��6�   $0 *Только русские символы��6�   �� 5!   
   -   5        $��LastName��5!   �   $��1��6   $ 20��6"   $��1��61   $	��Top=�6X   $
��Left=�6p   $	��160��6�   $ Отчество��6�   $��	10,10,0,0q�6�   $��26��6�   $0 *Только русские символы��6�   �� 5"   
   -   5        $��	Telephone��5"   �   $��1��6   $ 13��6"   $��1��61   $	��Top=�6X   $
��Left=�6p   $	��160��6�   $ Телефон��6�   $��
10,102,0,0q�6�   $ Только цифры��6�   G� 5#   
   -   5        $��Password��5#   �   $��2��6   	 G� 	MaxLength$	 20��6&   $��1��65   
�������$	��Top=�6w   $
��Left=�6�   $	��160��6�   $ Пароль��6�   $% от 6 до 20 символов��6�   $��	10,10,0,0q�6)  G� 5$   
   -   5        $��PasswordCheck��5$   �   $��2��6   $	 20��6&   $��1��65   
�������$	��Top=�6x   $
��Left=�6�   $	��160��6�   $% Повторите пароль��6�   $��
10,101,0,0q�6�   �� 5%   
   -	   5        $��2��5%      $��2��6!   
�������$��17(�6A   $��Bottom=�6P   $��Right=�6k   $��Сохранить��6�   $��	0,0,10,10q�6�   6�   5&      5'      