   M S B A M L   `   `   `  ����L  HPavilions_program, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Pavilions_programPavilions_program  5   	   "    Pavilions_program.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Pavilions_program     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      � .+�5   
   �� -   5         ��Name$ enter��5      $��
Войти��6    $
��Left=�60   $	��Top=�6K   $��75��6c   $��359,233,0,0q�6�   �� 5   
   $��Логин (Почта)��6   $
��Left=�6(   $	��Top=�6C   $��93��6[   $��287,150,0,0q�6f   �� 5   
   -   5         ��Name$ 
login_text��5      $
��Left=�6&   $��23��6A    �� TextWrapping$
 Wrap=�6M   $	��Top=�6a   $	��120��6y   $��380,153,0,0q�6�   $��Feafan@gmai.com��6�   G� 5   
   -   5         G�Name$ password_text��5      $
��Left=�6-   $	��Top=�6H   $	��120��6`   $��23��6l   $��380,181,0,0q�6x    G� Password$ Kc1PeS��6�   �� 5   
   $��Пароль��6   $
��Left=�6!   $��287,181,0,0q�6<   $	��Top=�6Q   $��93��6i   �� 5   
   -   5         ��Name$ er_mes��5      $
��Left=�6    $��Center=�6;   $	��Top=�6_   $	��222��6w   $��287,266,0,0q�6�   $��27��6�   ���	#FFD600006�   �� 5   
   $
��Left=�6   $	��Top=�6+   $	��170��6C   $��
330,76,0,0q�6O   $��39��6c   
���   �$��Авторизация��6�   $��20(�6�   $
��Gigi)�6�   �� 5   
   -   5        $ captcha_field��5      $
��Gigi)�6'   $��Center=�69   $�� ��6]   $
��Left=�6h   $��330,298,0,0q�6�   $	��Top=�6�   $	��137��6�   $��18(�6�   $y�Hidden=�6�   �� 5   
   -   5        $ captcha_input��5      $
��Left=�6*   $��23��6E   $��359,338,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $��75��6�   $y�Hidden=�6�   �� 5   
   -   5        $ CaptchaEnter��5      $��Отправить��6'   $
��Left=�6;   $	��Top=�6V   $��75��6n   $��359,366,0,0q�6z   $y�Hidden=�6�   5      5      