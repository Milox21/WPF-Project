   M S B A M L   `   `   `  ����D  @Kosmodrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Kosmodrom.Views.SingleKosmodrom.Views.Single  5      I)clr-namespace:Kosmodrom.ViewModels.SingleKosmodrom.ViewModels.Single  5      ;"clr-namespace:Kosmodrom.Views.ManyKosmodrom.Views.Many  5      (    "Kosmodrom.Views.Single.VehicleView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Kosmodrom.Views.Single     5      <Single)clr-namespace:Kosmodrom.ViewModels.Single     5      1UC"clr-namespace:Kosmodrom.Views.Many     5        � DataContext  5      2   ,Kosmodrom.ViewModels.Single.VehicleViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��0.3* �6#   �� 5      �� 5      �� 5      �� 5      �� 5      $
��0.5* �6$   �� 5      �� 5      $
��0.3* �6#    5!      �� 5"      �� 5#      $
��0.5* �6    �� 5$      $
��0.5* �6    �� 5%      �� 5&      $
��auto �6    .+�5)      �� $��1��6   $��1��6%   $
��Name��65   �� 5*      $��1��6   $��2��6%   $
��Type��65   �� 5+      $��1��6   $��3��6%   $��	SeatCount��65   �� 5,      $��1��6   $��4��6%   $��Speed��65   �� 5.      $��2��6   $��1��6#   ��63   �� *Name+�� 5/      $��2��6   $��2��6#   ��63   �� *Type+�� 50      $��2��6   $��3��6#   ��63   �� *	SeatCount+�� 51      $��2��6   $��4��6#   ��63   �� *Speed+�� 53      $��2��6   $��6��6"   $��ADD VEHICLE��62   ��6H   �� *
AddVehicle+54      '   !Kosmodrom.Views.Many.VehiclesView  56   
   $��1��6   57      58      