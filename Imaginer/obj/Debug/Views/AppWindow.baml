   M S B A M L   `   `   `  ����C  ?Imaginer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Imaginer.ViewsImaginer.Views  5   	   7 clr-namespace:Imaginer.ConverterImaginer.Converter  5	   	   7 clr-namespace:Imaginer.ViewModelImaginer.ViewModel  5
   	       Imaginer.Views.AppWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Imaginer.Views     5   	   6	converter clr-namespace:Imaginer.Converter     5	   	   3models clr-namespace:Imaginer.ViewModel     5
   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	     � Title$  Imaginer��5   	   $	��300��6   $	��300��6'    � WindowState$ 	Maximized=�5   	    � 	Resources 5      ?   9Imaginer.Converter.BooleanConfigurableVisibilityConverter    BooleanToVisibilityConverter�   �Imaginer.Converter.BooleanConverter`1[[System.Windows.Visibility, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   True   False ' #InverseBooleanToVisibilityConverter �� DataType&    Imaginer.ViewModel.HomeViewModel   Imaginer.Views.HomeView+   %Imaginer.ViewModel.MediaListViewModel"   Imaginer.Views.MediaListView��%  &	        &	 6     (�� l     5      *, +)(�� �     5      *, +)  5   
   $ Visible=�6`   $ 	Collapsed=�6o     5   
   $ 	Collapsed=�6g   $ Visible=�6x   �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   .+�5    
   � -   5        $��gridBusyIndicator��5       $��0��6(    K� ZIndex$	 999��5!      y�5    5   �� *ShowBusyIndicator+ �� 	Converter# [   5"      �� 5#      $��* �6    �� 5$      $
��Auto �6    �� 5%      $��* �6    	 � ColumnDefinitions	 5'      �� 5(      $��* �6#   �� 5)      $
��Auto �6#   �� 5*      $��* �6#   .+�5,      "  FontAwesome.WPF.ImageAwesome  $��3��6   $	 999��6/   $��3��6B   
   Icon$
 CircleOutlineNotch=�6V      
Foreground
 �����   Spin . $��48��5-   =   $��48��6I      SpinDuration$
 1.25Q�6T   5.      �� 5/   
   -   5         ��Name$ Pages��5/   &   $��0��6   ��65   �� *SelectedViewModel+y�50      �� *ShowBusyIndicator+# [ 51      52      