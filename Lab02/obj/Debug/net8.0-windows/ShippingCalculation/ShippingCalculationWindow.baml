   M S B A M L   `   `   `  ����@  <Lab02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Lab02.ShippingCalculationLab02.ShippingCalculation  5   	   :    4Lab02.ShippingCalculation.ShippingCalculationWindows   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Lab02.ShippingCalculation     5   	     � Title$   ShippingCalculationWindows��5   	   $	��640��6,   $
��1024��69   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��192* �6   �� 5      $	��25* �6    � ColumnDefinitions 5   
   �� 5      ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FFD767F9��6    � Offset$ 0Q�61   � 5      $ 	#FF3C56BB��6   $ 1Q�61   5      .+�5   
   �� $��Shipping Calculator��6   $��Center=�6.   $��0,66,0,0q�6K   $	��Top=�6]   $��36(�6u   
�������$��58��6�   $	��320��6�   �� 5   
   $��Package Weight (kg):��6   $��Right=�6/   $��0,0,618,242q�6K   $��Bottom=�6`   $��18(�6{   
�������$��34��6�   $	��210��6�   $��Right=�6�   �� 5   
   -   5         ��Name$ packageWeightTextBox��5      $��432,278,432,0q�60    �� TextWrapping$
 Wrap=�6G   $��Weight��6[   $��18(�6i   ���	#84FFFFFF���	#FFEFEFEF$��34��6�   $	��Top=�6�   �� 5   
   -   5        	 ��Name$	 calculateButton��5      $��	Calculate��6*   $��794,496,63,10q�6>   6U   �� 5   
   -   5        
 ��Name$
 costTextBlock��5      $
��Wrap=�6+   $��Shipping Cost:��6?   
�������$��18(�6h   $��	0,0,63,77q�6v   $��51��6�   $��Bottom=�6�   $��Right=�6�   $	��254��6�   o� 5   
   $��Center=�6   $
��1024��6,   $��41��69   $	��Top=�6E   ���	#FFADADAD.@�5      m� -   5         m�Name$ distanceTraveledApp��5      $��Distance Traveled App��64   $��39��6u   $	��144��6�   $��14(�6�   m� 5      -   5        $ 	scoresApp��5      $��Test Scores App��6*   $��14(�6[   m� 5      -   5        $ rainfallAvgApp��5      $��14(�6/   $��Rainfall Average App��6=   6[   m� 5       -   5        $ mainApp��5       $��	Main Menu��6(   $��14(�6Q   5!      �� 5"   
   $ ��Distance Traveled (Miles):��6   $��Right=�65   $��0,0,618,165q�6Q   $��Bottom=�6f   $��18(�6�   
�������$��34��6�   $	��210��6�   �� 5#   
   -   5        $ distanceTextBox��5#      $��432,353,432,0q�6+   $
 Wrap=�6B   $��Distance��6V   $��18(�6f   ���	#84FFFFFF���	#FFEFEFEF$��34��6�   $	��Top=�6�   5%      5&      