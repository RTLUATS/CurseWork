   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	       CurseWork.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��16(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ Public catering��5   	   $	��650��6!   $
��1200��6.   .��5      � 
���   � � ColumnDefinitions 5   
   �� 5      $	��200 �6   6-   �� 5      $��5 �6   6+   �� 5      $
��167* �6   6.   �� 5      $
��136* �6   6.   �� 5      $
��156* �6   6.   �� 5      $
��190* �6   6.   �� 5      $
��149* �6   6.   �� 5      $
��169* �6   5       � RowDefinitions 5   
   �� 5      $��65 �6   6*   �� 5       6   .+�5"   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Custom=�6#   
���   �
�������$��16q�6W      CornerRadius$ 3��6d   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�6u   $��8��6�   $��70��6�   $��2��6�   .��5#      ��  �� Orientation$ 
Horizontal=�6   .+�5$      d� -   5        $��OpenCategories��5$       $  MaterialDesignSwitchToggleButton#��[ 65   	 d� 	IsChecked$	 FalseY�6o   6�   �� 5%      $��DemiBold"�6   $��Center=�62   $��16 0 0 0q�6M   Категории :5'      5(      5)      �� 5*   
   -   5        $��
Categories��5*   "   $��0��6   $��1��64   $��10q�6A   $y�Hidden=�6M   {�.  6a   �� 5+   
   -   5        $
��Menu��5+      $��1��6   $��2��6.   $��20q�6;   $��7��6G   �� 5,   
   -   5        $��Autorization��5,   h   $��0��6   $��6��6    " MaterialDesignRaisedDarkButton#��[ 60   $��DemiBold"�6|   $��16(�6�   $��Авторизация��6�   $��Center=�6�   $��
10,16,5,17q�6�   �� 5-   
   -   5        $��EditInfo��5-   f   $��0��6   $��6��6   #��[ 6.   {�.  $y�Hidden=�6�   $��DemiBold"�6�   $��14(�6�   $5��/Редактировать Информацию��6�   $��Center=�6�   $��
10,16,5,17q�6  �� 5.   
   -   5        $��Search��5.      $��0��6   #��[ 6,   $��3��6d   $��16(�6t   $��DemiBold"�6�   $��
Поиск��6�   $��10,16,10,17q�6�   �� 5/   
   -   5        $��LogOut��5/   f   $��0��6   $��7��6   #��[ 6.   $��16(�6t   $��DemiBold"�6�   $ ��Разлогиниться��6�   {�.  $y�Hidden=�6�   $
��Auto��6�   $��10,16,10,17q�6�   �� 50   
   -   5        $��WorkRoom��50   f   $��0��6   $��5��6   #��[ 6.   $#��Рабочий кабинет��6�   $��DemiBold"�6�   $��16(�6�   {�.  $y�Hidden=�6�   $
��Auto��6�   $��
10,16,5,17q�6�   �� 51   
   -	   5        $��Basket��51      $��0��6   #��[ 6,   $��4��6d   $��Корзина(0)��6t   $��DemiBold"�6�   $��16(�6�   $
��Auto��6�   $��10,16,10,17q�6�   �� 52   
   -
   5        $��SearchField��52      
 �� TextWrapping$

 Wrap=�6%   $��DemiBold"�69   
���   �
�������)  #MaterialDesignThemes.Wpf.HintAssist   Hint$&  Поиск по названию��6z   $��0��6�   $��2��6�   $��Center=�6�   $��10,18,10,17q�6�   6�   53      54      