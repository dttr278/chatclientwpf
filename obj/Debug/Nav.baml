   M S B A M L   `   `   `  ����E  AchatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:chatClient
chatClient  5   	       chatClient.Nav   5   	   a ]DevExpress.Xpf.Core.v18.1, Version=18.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:chatClient     5   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   	     � Title$  Contact��5	   	   $	��600��6   $	��285��6&   .��5
      � ���	#FFE5E5E5.+�5   
   � -   5         �Name$ 	navHeader��5      $��50��6"   $	��Top=�6.   
�������.+�5      �� -   5         ��Name$ 
userAvatar��5      $
��Left=�6,   $��40��6G   $	��Top=�6S   $��40��6k   $��5,5,0,0q�6v   $
��Fill=�6�    J� Clip 5      @� 5       @� Center$ 20,20&�6*    @� RadiusX$ 20Q�69    @� RadiusY$ 20Q�6F   5      �� 5      -   5         ��Name$ tbName��5   8   $��12(�6   $
��Bold"�6&   $
��Left=�6H   $��	50,10,0,0q�6c   $
��Wrap=�6v   $��	TextBlock��6�   $	��Top=�6�   $	��168��6�   �� 5      -   5        $ 
tbUserName��5   &   $��10(�6   $
��Left=�6:   $��	50,26,0,0q�6U   $
��Wrap=�6h   $��	TextBlock��6|   $	��Top=�6�   $	��168��6�   �� 5      $
��Left=�6   $��
218,23,0,0q�63   $
��Wrap=�6G   $	��Top=�6\   �� 5      -   5        $��#?�5      .�6D   .�5      �� $��Logout��6    60   5      5      5      �� 5   
   $��0,50,0,0q�6   .@�5      �� $
��Chat��6   .��5      � 
������.+�5      �� $��20��6    �� TextWrapping$
 Wrap=�6*   $	��Top=�6>   �� 5       -   5        	 ��Name$	 chatList��5       $��0,25,0,0q�61   5!      5"      �� 5#      $��Group��6   .��5$      � 
������.+�5%      �� $��20��6   $
 Wrap=�6*   $	��Top=�6>   �� 5&      -   5        $	 	groupList��5&      $��0,25,0,0q�62   5'      5(      �� 5)      $��Contact��6   .��5*      � 
������.+�5+      �� $��20��6   $
 Wrap=�6*   $	��Top=�6>   �� 5,      -   5        $	 contactList��5,      $��0,25,0,0q�64   5-      5.      �� 5/      $��Search��6   .��50      � 
������.+�51      �� $��20��6   $
 Wrap=�6*   $	��Top=�6>   $��0,0,40,0q�6V   �� 52      $
��Tìm��6   $	��Top=�6+   $��20��6C   $��Right=�6O   $��35��6k   �� 53      -	   5        $	 
searchList��53      $��0,25,0,0q�63   54      56      58      59      5:      