   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   C ?Notebook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:NotebookNotebook 5   	      Notebook.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   (localclr-namespace:Notebook     5   	     � Title$  Notebook��5	   	    � SizeToContent$ WidthAndHeight=�5
   	    � 
ResizeMode$ CanMinimize=�5   	    � 	Resources 5      &   Notebook.SortMultiValueConverter   SortMultiValueConverter��%   &	          5   
   6(   5      .   (System.Windows.Interactivity.Interaction   Triggers 5      /   )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   -   5        Closing��.   (System.Windows.Interactivity.TriggerBase   Actions. 5      6   0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *ExitCommand+5       � DataContext 5        Notebook.MainVM  5   
   .��5      � 	 � ShowGridLines	 . 
 � RowDefinitions
 5   
   �� 5      $��20 �6   �� 5      $��60 �6   �� 5      $	��491 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5       $	��800 �6   .+�5"   
   o� $��20��6   $	��Top=�6   $��2��63   $��0��6G   .@�5#      m� $��Файл��6   .@�5$      m� $��Импорт��6   .@�5%      m� $	��WPF��6   6/   m� 5&      $��Импорт из CSV...��6   6;   5'      m� 5(      $��Экспорт��6   .@�5)      m� $	��WPF��6   6/   m� 5*      $��Импорт из CSV...��6   6;   5+      �� 5,      m� 5-      $��
Выход��6    m� Command 6*   �� *ExitCommand+6L   5.      m� 5/      $
��Edit��6   .@�50      m� $��Добавить��6    6-   �� *
AddCommand+6N   m� 51      $��Выполена��6    6-   �� *ToDoCommand+6O   m� 52      $ ��Редактировать��6    62   �� *EditCommand+6T   m� 53      $��Удалить��6    6,   �� *RemoveCommand+6P   54      m� 55      $��Справка��6   .@�56      m� $��Статистика��6   61   �� 57      m� 58      $��О программе��6   62   59      5:      �� 5;   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��2��6;   .+�5<      �� $��30(�5=      $��12q�5>      ��5<      �� *	TimeNow+5>      5?      �� 5@   
   $ Vertical=�6   $��2��6,   $��0��69   .+�5A      �� $��Добавить��6   $
��20 7q�5B      $	��add��5D      ��5C      �� *
AddCommand+5E      �� 5F      $��Выполнена��6   $
��20 7q�5G      ��5H      �� *ToDoCommand+��5I      ��  �� ElementName$ ToDoList�� �� Path$ SelectedItem��� 5J      $ ��Редактировать��6   $
��20 7q�5K      ��5L      �� *EditCommand+��5M      �� $ ToDoList��$ SelectedItem�5N      �� 5O      $��Удалить��6   $
��20 7q�5P      ��5Q      �� *RemoveCommand+��5R      �� $ ToDoList��$ SelectedItem�6\   �� 5S      $
��20 7q�6   $ 
Horizontal=�6'   .+�5T      �� -   5         ��Name$ 	SortParam��5T   I   $��0 0 5 0q�6    �� SelectedIndex$ 0��6,   $��80��6>   .@�5U      �� -   5        $��Created��5U   #   .��62   Создана5W      �� 5X      -   5        $��Theme��5X   #   .��60   
Тема5Z      �� 5[      -   5        $��Priority��5[   #   .��63   Приоритет5]      �� 5^      -   5        $
��Date��5^   #   .��6/   Дедлайн5`      �� 5a      -   5        $
��ToDo��5a   #   .��6/   Выполнение5c      5d      �� 5f      -   5         ��Name$
 Sort��5f      $��Сортировать��5g      ��5h      �� *SortCommand+��5i      `� 5j       `� 	Converter# [  6'   .1�5k      �� $ 	SortParam��6&   $ SelectedItem.Name�6>   �� 5l      $ ToDoList��6&   5m      5o      5q      5r      �� 5x   
   -	   5         ��Name$ ToDoList��5z      $��2��5x      $��1��5y      $��Disabled=�5{      ��.  ��5}      �� *	ListRow+��5~      �� 5      .R�5�      �� $ Vertical=�6!   $	��780��5�   !   $
��Left=�5�   !   ��5�   !   �� $ Color�.+�5�      �� $
��10 2q�6%   .+�5�      �� $��20(�6D   ��6(   �� $ Theme�6D   5�      �� 5�      $��
10,0,100,0q�6$   $
��Wrap=�68   �� 5�       �� Padding$	 5 2q�6(   $
��Wrap=�5�   !   $��13(�5�   !   ���#ecf0f1��5�   !   �� $ DateForUser� �� StringFormat$ Выполнить {0}����5�   !   �� $ BGcolorUserDate�5�   (   5�      �� 5�      $��
10 0 10 10q�6$   $
��Wrap=�5�   $   $��15(�5�   $   ��5�   $   �� $
 Text�5�   $   5�      5�      5�      5�      5�      