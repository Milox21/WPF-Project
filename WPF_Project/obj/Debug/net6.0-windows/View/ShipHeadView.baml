   M S B A M L   `   `   `  ����F  BWPF_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WPF_Project.ViewWPF_Project.View  5      #    WPF_Project.View.ShipHeadView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:WPF_Project.View     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $# Resources/PanelResources.xaml?�6%   %    5      .��5      � ���#71797E � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��3* �6   �� 5      �� 5      $��3* �6   �� 5      $
��0.5* �6    � RowDefinitions 5   
   �� 5      �� 5      $
��1.5* �6   �� 5      �� 5      $
��1.5* �6   �� 5      .+�5!   
   �� $��Ship Head Specyfication��6   $��1��64   $��1��6A   $��3��6Q   
�������.��5"      �  5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      �� 5*       5-      �� 5.      $��0.65* �6'   �� 5/      .+�52      ��    TextManipulator#��[  6   $��0��6A   $��0��6N   $
��Type��6^   �� 53      #��[  6   $��1��6A   $��0��6N   $��Needed Crew��6^   �� 54      #��[  6   $��2��6A   $��0��6N   $��Is renewable��6^   �� 55      #��[  6   $��3��6A   $��0��6N   $��Sensor Range��6^   �� 56      #��[  6   $��4��6A   $��0��6N   $��System��6^   �� 57      #��[  6   $��5��6A   $��0��6N   $��Controllability��6^   �� 58      #��[  6   $��6��6A   $��0��6N   $��	AI assist��6^   �� 5:      $��0��6   $��1��6(   $��25��68   .@�5;      �� $��Milliteh research head��6#   �� 5<      $��RusKocmoc resourcegather��6#   �� 5=      $��
Uchu atama��6#   �� 5>      $��NanoGyro��6#   �� 5?      $��Cosmic Nexus��6#   5@      �� 5B      $��4��6   $��1��6(   $��25��68   .@�5C      �� $��	KosmoCore��6#   �� 5D      $��
InfinityOS��6#   �� 5E      $��QuantumControl��6#   5F      �� 5H      $��5��6   $��1��6(   $��25��68   .@�5I      �� $��1.0 UAP��6#   �� 5J      $��2.0 UAP��6#   �� 5K      $��2.8 UAP��6#   �� 5L      $��
SaproGen 4��6#   �� 5M      $��SaproGen 5 EX��6#   5N      �� 5P      $��1��6   $��1��6'   $��25��67   �� 5Q      $��3��6   $��1��6'   $��25��67   �� 5S       �� Orientation$ 
Horizontal=�6   $��2��66   $��1��6C     StackHorizontalConstructor#��[ 6S   .+�5T      	� $	��Yes��6"   $��0,0,25,0q�60   
�������6B   	� 5U      $��No��6"   $��0,0,0,0q�6/   
�������6@   5V      �� 5X      $ 
Horizontal=�6   $��6��66   $��1��6C   #��[ 6S   .+�5Y      	� $	��Yes��6"   $��0,0,25,0q�60   
�������6B   	� 5Z      $��No��6"   $��0,0,0,0q�6/   
�������6@   5[      5\      5]      �� 5_   
   $
��Crew��6   $��1��6!   $��3��6.   $��3��6>   
�������.��5`      �  5a      �� 5b      �� 5c      �� 5d      �� 5e      �� 5f      �� 5g      �� 5h      �� 5i      .+�5l      �� $��Acquired Crew��6   #��[  60   �� 5m      $ 
Horizontal=�6   $��1��66   #��[ 6C   .+�5n      �� $��Pilot��6    d� 	IsChecked$
 TrueY�6/   $��0,0,10,0q�6@   
�������6R   �� 5o      $��Pilot 2��6   
�������61   5p      �� 5r      $ 
Horizontal=�6   $��2��66   #��[ 6C   .+�5s      �� $��Mechanic��6   $��0,0,10,0q�62   
�������6D   �� 5t      $��	Navigator��6   $��0,0,10,0q�63   
�������6E   �� 5u      $��	Signaller��6   
�������63   5v      �� 5x      $��3��6   $��Crew Search��6%   #��[  6;   �� 5z      $ 
Horizontal=�6   $��4��66   #��[ 6C   .+�5{      �� $��Pilots��6   $��0,0,10,0q�60   
�������6B   �� 5|      $��	Mechanics��6   $��0,0,10,0q�63   
�������6E   �� 5}      $��
Navigators��6   
�������64   5~      �� 5�      $ 
Horizontal=�6   $��5��66   #��[ 6C   .+�5�      �� $��Communicators��6   $��0,0,10,0q�67   
�������6I   �� 5�      $��
Researcher��6   $��0,0,10,0q�64   
�������6F   �� 5�      $��Medic��6   
�������6/   5�      �� 5�      $ 
Horizontal=�6   $��6��66   #��[ 6C   .+�5�      �� $��Gun operator��6   $��0,0,10,0q�66   
�������6H   �� 5�      $��Commanding Officer��6   $��0,0,10,0q�6<   
�������6N   5�      5�      5�      �� 5�   
   $��2��6   $��2��6!   $��50��6.   $	��150��6:   $
��SAVE��6F   
���OO/�$��30(�6p   
�������
�������6�   5�      5�      