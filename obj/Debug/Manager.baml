   M S B A M L   `   `   `  ����D  @CurseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CurseWork	CurseWork  5   	       CurseWork.Manager   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CurseWork     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          � 
ResizeMode$  NoResize=�5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��16(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ Manager��5   	   $	��650��6   $	��800��6&   .��5      � 
���   �.+�5   
   �� -   5        $��Table��5   8   $y�Hidden=�6   {�.  $��10,10,267,10q�6F   �� 5   
   -   5        $��InquiryListBox��5   9   $y�Hidden=�6   {�.  $��10,63,267,10q�6O   �� 5   
   -   5        $��LName��5      $y�Hidden=�6   $��Наименование��61   
�������$��10,10,644,568q�6[   �� 5   
   -   5        $��LCount��5      $y�Hidden=�6   $��Количество��62   
�������$��198,10,485,568q�6Z   �� 5   
   -   5        $��LDate��5      $y�Hidden=�6   $(��"Дата подачи заявки��61   
�������$��345,10,267,568q�6a   �� 5   
   
�������$��16(�6$   $��DemiBold"�62   $-��'Заказать ингредиенты��6H   {�.  $y�Hidden=�6y   $��575,67,10,507q�6�   �� 5   
   -   5        $��BoxWithFoods��5      $��532,155,10,416q�6T    & "MaterialDesignFloatingHintComboBox#��[ 6l     SecondaryAccentBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �  6�   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ Inverted=�6�   
�������)  #MaterialDesignThemes.Wpf.HintAssist   Hint$( "Действия с блюдами��6F  $��50��6z  .@�5      �� .��5      �� -   5        $��AllFoods��5   +   
�������$��Все блюда��6;   $	��222��6L   5      �� 5      .��5      �� -	   5        $��FoodWithOutPrice��5   +   
�������$ ��Блюда без цены��6C   $	��222��6Y   5       �� 5!      .��5"      �� -
   5        $��FoodWithOutDescription��5"   /   
�������$*��$Блюда без описанний��6M   $	��222��6h   5#      �� 5$      .��5%      �� -   5        $��FoodNotInMenu��5%   /   
�������$!��Блюда не в меню��6D   $	��222��6[   5&      5'      �� 5(   
   -   5        $��BoxWithIngredients��5(      $��532,357,10,214q�6d   #��[ 6|   # �  6�   $ Inverted=�6  
�������$4 .Действия с ингредиентами��6V  $��50��6�  .@�5)      �� .��5*      �� -   5        $��AllIngredients��5*   /   
�������$#��Все ингредиенты��6E   $	��222��6\   5+      �� 5,      .��5-      �� -   5        $��
InquiryBuy��5-   0   
�������$(��"Запросы на покупку��6B   $	��222��6\   5.      �� 5/      .��50      �� -   5        $��IngredientsCountNull��50   /   
�������$2��,Ингредиенты которых нет��6K   $	��222��6j   51      52      �� 53   
   -   5        $��
DishReport��53      
�������$ ��Отчёт о блюдах��66   $��570,482,10,94q�6O   $
��Auto��6   �� 54   
   -   5        $��IngredientReport��54      
�������$.��(Отчёт об ингредиентах��6<   $��570,532,10,44q�6\   $
��Auto��6�   �� 55   
   -   5        $��SearchButton��55   $   
�������$��
Поиск��68   $��700,18,10,560q�6H   $
��Auto��6_   6o   �� 56   
   -   5        $��
SearchText��56   S   
�������$ Поиск...��6)   $��564,18,99,560q�6e   6~   57      58      