   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	   &     CurseWork.AdministratorViewUsers   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � 
ResizeMode$  NoResize=�5   	      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ AdministratorViewUsers��5   	   $	��450��6(   $	��800��65   .��5      � 
���   �.+�5   
   �� 
�������$��DemiBold"�6#   $��10,10,732,372q�69   $��16(�6P   .��6^   	Имя:6d   �� 5   
   
�������$��DemiBold"�6#   $��16(�69   $��10,52,697,328q�6G   .��6^   Фамилия:6h   �� 5   
   
�������$��DemiBold"�6#   $��16(�69   $��10,96,697,288q�6G   .��6^   Отчество:6i   �� 5   
   
�������$��DemiBold"�6#   $��16(�69   $��10,136,697,245q�6G   .��6_   Телефон:6i   �� 5   
   
�������$��DemiBold"�6#   $��16(�69   $��10,179,697,206q�6G   .��6_   Id:6d   �� 5   
   
�������$��DemiBold"�6#   $��16(�69   $��10,218,697,162q�6G   .��6_   	Роль:6f   �� 5   
   -   5        $��	FirstName��5   K   
�������$��DemiBold"�6'   $��16(�6=   $��100,13,522,372q�6\   �� 5   
   -   5        $��
MiddleName��5   K   
�������$��DemiBold"�6'   $��16(�6=   $��100,51,522,328q�6]   �� 5   
   -   5        $��LastName��5   K   
�������$��DemiBold"�6'   $��16(�6=   $��100,96,522,288q�6[   �� 5   
   -   5        $��	Telephone��5   K   
�������$��DemiBold"�6'   $��16(�6=   $��100,136,522,245q�6\   �� 5   
   -   5        $��Id��5   K   
�������$��DemiBold"�6'   $��16(�6=   $��100,179,522,206q�6U   �� 5   
   -   5        $
��Role��5      $��100,220,470,155q�6     & "MaterialDesignFloatingHintComboBox#��[ 69     SecondaryAccentBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �  6u   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ Inverted=�6�   
�������)  #MaterialDesignThemes.Wpf.HintAssist   Hint$0 *Нажмите чтобы изменить��6  $��46��6K  .@�5      �� .��5       �� -   5        $��
SimpleUser��5    E   
�������$��DemiBold"�6/   $��16(�6W   $��Пользователь��6e   $	��160��6y   5!      �� 5"      .��5#      �� -   5        $��Manager��5#   E   
�������$��DemiBold"�6/   $��16(�6T   $��Менеджер��6b   $	��160��6r   5$      �� 5%      .��5&      �� -	   5        $
��Chef��5&   E   
�������$��DemiBold"�6/   $��16(�6Q   $��Шеф-повар��6_   $	��160��6p   5'      �� 5(      .��5)      �� -
   5        $��	Economist��5)   E   
�������$��DemiBold"�6/   $��16(�6V   $��Экономист��6d   $	��160��6u   5*      �� 5+      .��5,      �� -   5        $��Administrator��5,   E   
�������$��DemiBold"�6/   $��16(�6Z   $ ��Администратор��6h   $	��160��6}   5-      �� 5.      .��5/      �� -   5        $��Director��5/   E   
�������$��DemiBold"�6/   $��16(�6U   $��Директор��6c   $	��160��6s   50      51      �� 52   
   -   5        $��IsBlock��52   @   
�������$��DemiBold"�6*   $��275,10,367,381q�6O   $��16(�6g   $��Заблокирован?��6v   �� 53   
   -   5        $
��Save��53      
�������$��DemiBold"�60   $��16(�6F   $'��!Сохранить измения��6T   $��582,357,10,15q�6p   $
��Auto��6�   54      55      