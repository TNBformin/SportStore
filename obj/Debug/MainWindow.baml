   M S B A M L   `   `   `  ����E  ASportStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:SportStore
SportStore  5   	       SportStore.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:SportStore     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��0.25* �6   �� 5      �� 5      $��0.20* �6   .+�5   
   � $��0��6   ���	#FF2BBB9D.+�5      �� $��Center=�6   $��Center=�65   $��36(�6P   ���	#FF5700F9$��Bahnschrift SemiBold)�6u   Спорт товары6�   �� 5      -   5         ��Name$ 
CartButton��5   �   $
��60px��6   $
��30px��6"   $��Right=�60   $y�Hidden=�6L   $��Center=�6`   $��15q�6{   .��6�   Корзина6�   �� 5      -   5        $ 
BackButton��5   �   $
��60px��6   $
��30px��6"   $
��Left=�60   $y�Hidden=�6K   $��Center=�6_   $��15q�6z   .��6�   
Назад6�   5      � 5   
   $��1��6   .+�5       � -   5          �Name$ 	MainFrame��5        � NavigationUIVisibility$ Hidden=�6'   6I   5      � 5   
   $��2��6   ���	#FF2BBB9D.+�5      � -   5         �Name$ Prices��5      $y�Hidden=�6#    � ColumnDefinitions 5      �� 5      �� 5      �� 5      .+�5      �� -   5         ��Name$ 	DateBlock��5   D   $��Center=�6   $��5q�69   6Y   �� 5      $��Center=�6   $��Bottom=�6:    �� Orientation$ 
Horizontal=�6U   $��5q�6n   .+�5       �� $��0 0 5 0q�6    $
��16px(�61   Пункт выдачи:6P   �� 5!      -   5        $��PickUpPointBox��5!      $��120px��6[   $
��25px��6i   6z   5"      �� 5$      $��Center=�6   $��Center=�6:   $��1��6U   $ Vertical=�6e   .+�5%      �� -   5        $ 	FullPrice��5%       65   �� 5&      -   5        $ DiscountAmount��5&       6:   �� 5'      -	   5        $ 	LastPrice��5'       65   5(      �� 5*      $��120px��6   $
��30px��6'   $��Center=�65   $��Center=�6R   $��2��6m   .��6}   Оформить заказ6�   5+      5-      5.      5/      