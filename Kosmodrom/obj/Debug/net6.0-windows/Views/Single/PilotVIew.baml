   M S B A M L   `   `   `  ����D  @Kosmodrom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Kosmodrom.Views.SingleKosmodrom.Views.Single  5      I)clr-namespace:Kosmodrom.ViewModels.SingleKosmodrom.ViewModels.Single  5      ;"clr-namespace:Kosmodrom.Views.ManyKosmodrom.Views.Many  5      &     Kosmodrom.Views.Single.PilotVIew   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Kosmodrom.Views.Single     5      <Single)clr-namespace:Kosmodrom.ViewModels.Single     5      1UC"clr-namespace:Kosmodrom.Views.Many     5        � DataContext  5      0   *Kosmodrom.ViewModels.Single.PilotViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��0.3* �6#   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $
��0.3* �6#    5!      �� 5"      �� 5#      $
��0.5* �6    �� 5$      $
��0.5* �6    �� 5%      $
��0.5* �6    �� 5&      $
��0.5* �6    �� 5'      �� 5(      $
��auto �6    .+�5+      �� $��1��6   $��1��6%   $
��Name��65   �� 5,      $��1��6   $��2��6%   $��Surname��65   �� 5-      $��1��6   $��3��6%   $��Can be Lead Pilot��65   �� 5.      $��3��6   $��1��6%   $	��Sex��65   �� 5/      $��3��6   $��2��6%   $��Nationality��65   �� 50      $��3��6   $��3��6%   $	��Age��65   �� 51      $��3��6   $��4��6%   $��
Experience��65   �� 53      $��2��6   $��1��6#   ��63   �� *	FirstName+�� 54      $��2��6   $��2��6#   ��63   �� *
LastName+�� 55      $��4��6   $��1��6&   .+�56      	� $
��Male��6    	� 	GroupName$ First Group��6-    d� 	IsChecked 6E   �� *SexArray[0]+	� 57      $��Female��6   $ First Group��6/    6G   �� *SexArray[1]+58      �� 59      $��2��6   $��3��6$   $	��Yes��64    6B   �� *	LeadPilot+�� 5:      $��4��6   $��2��6#   ��63   �� *Nationality+�� 5;      $��4��6   $��3��6#   ��63   �� *Age+�� 5<      $��4��6   $��4��6#   ��63   �� *
Experience+�� 5>      $��2��6   $��6��6"   $��	ADD PILOT��62   ��6F   �� *SaveAndRefreshCommand+5?      %   Kosmodrom.Views.Many.PilotsView  5A   
   $��1��6   5C      5E      