   M S B A M L   `   `   `  ����C  ?Imaginer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Imaginer.ViewsImaginer.Views  5      7 clr-namespace:Imaginer.ConverterImaginer.Converter  5      "    Imaginer.Views.MediaListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Imaginer.Views     5      6	converter clr-namespace:Imaginer.Converter     5      -   5        $��ucMediaListView��5        � 	Resources  5       �� MergedDictionaries �� Source'   !Imaginer.Converter.LabelConverter   LabelConverter  MediaListViewItemTemplate � Cursor J� InputBindings d� MouseAction �� Command �� ElementName �� CommandParameter	 � RowDefinitions
 �� IsAsync  NormalTextFontStyle �� TextAlignment�� 5   
    5      �� 5      $J Dpack://application:,,,/Imaginer;component/Styles/ScrollbarStyle.xaml?�6%   �� 5      $J Dpack://application:,,,/Imaginer;component/Styles/TextblockStyle.xaml?�6%   �� 5      $H Bpack://application:,,,/Imaginer;component/Styles/TextboxStyle.xaml?�6%   �� 5      $G Apack://application:,,,/Imaginer;component/Styles/ButtonStyle.xaml?�6%   %�  &	        &	      7    5      6'   �� 5      .R�5      �� ���#E8E8E8$��1q�6/   ._�5      � $��	gridImage��6   $
 Hand��6,    5      d� 5      $ 	LeftClicke�6+    6C   �� *&$DataContext.OpenSelectedImageCommand+$ ucMediaListView�� 6�   �� 	 5      �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   .+�5       �� $��0��6$   ���#E8E8E8$	��250��6@   $	��150��6L   �� 5!      $��0��6    $	��150��6Z   $	��250��6g   ��6-   �� *thumbnailUrl+
 . 6g   �� 5"      $��1��6$   8��  $��WrapWithOverflow=�5#   ;   $	��250��6[   $ Center=�6j   ��6$   �� *title+6j   5&      5'      5(      5)      .��5,      � 
�������	 5-   
   �� 5.      $
��Auto �6   �� 5/      $��* �6    � ColumnDefinitions 51   
   �� 52      $��50 �6   �� 53      $��* �6   �� 54      $��50 �6   .+�57   
   �� -   5        $��BackBtn��57      $��0��6    $��0��6-   $
��Left=�6=     NoBorderButton#��[ 6X   $��10 30 10 30q�6�   ��58      �� *BackButtonCommand+.��59      �� $B��<pack://application:,,,/Imaginer;component/Resources/Back.png��6   $��20��6Z   $��30��6f   5:      � 5;   
   -   5        $��
gridSearch��5;      $��0��6!   $��1��6.   $��Stretch=�6>   $��30q�6\    5<      �� 5=      $��* �6#   �� 5>      $��60 �6#   .+�5@      �� -   5        $��tbSearchBox��5@      $��0��6)     MyWaterMarkStyle#��[ 69   $��Stretch=�5A      $��14(�6{   
 � Tag 5@   c   ��  �� Path$ .� �� 	Converter# [   �� ConverterParameter$ 3����5A   5   �� *
SearchText+ �� Mode$ TwoWay=�6{   �� 5B      -   5        $��	btnSearch��5B      $��1��6&     BlueButtonWithHover#��[ 66   $��14(�5C   o   $��Right=�6}   ��5B   c   �� *SearchTextCommand+��5C      �� $ .�# [  $ 2��6}   5E      � 5G   
   -   5         �Name$ PhotoLargePopup��5G   !    � 	StaysOpen . $��Center=�5H   =   $��LeftToRight=�6P   ��5G   ;   �� $ SelectedImage.isOpened�$ TwoWay=� � PlacementTarget 5H      �� $ ucMediaListView��.��5J      ]� .+�5K      �� -   5        $��
CloseImage��5K      $��Right=�6+   $	��Top=�6G   #��[ 5L      ��5K   \   �� *CloseOpenImageCommand+.��5M      �� $C��=pack://application:,,,/Imaginer;component/Resources/Close.png��6   $��20��6c   $��20��6o   5N      �� 5O      -   5         ��Name$
 Zoom��5O      $
��Left=�6'    � Value$ 1Q�6=    � Minimum$	 0.1Q�6G    � Maximum$ 5Q�6U   $��25��6a    �� Orientation$ Vertical=�6l   (� 5P      $	��150��6   $��5 5 5 5q�6&   .��5Q      �� -	   5         ��Name$
 Canv��5Q      ��6+   �� $	 Img��$ ActualWidth���6_   �� $	 Img��$ ActualHeight�.+�5R      �� -
   5        ��5R       �� $	 Img��$ Source�6P   �� 5S      -   5         ��Name$ HRect��5S   $   ���	#75ffffff
���   �6D   5T      5U      �� 5V      -   5         ��Name$ SV��5V   g   $
��Auto=�6   $
��Auto=�6D   .��5W      �� -   5         ��Name$	 Img��5W   b   $
��None=�6S   $	��500��6o   $	��500��6|   ��6   �� $ SelectedImage.url�
 .  � LayoutTransform 5X      �� 5Y       �� ScaleX 6-   �� $
 Zoom��$ Value�  �� ScaleY  5Z   )   �� $
 Zoom��$ Value�5\      5]      5^      5_      � 5b   
   -   5        $��gridMediaList��5b      $��1��6$   $��1��61   $��Stretch=�6A   	 5c      �� 5d      $��* �6    �� 5e      $��40 �6    .+�5g      �� -   5        $ ListBoxScroll��5g   )   $��0��6   $
��Auto=�5h   2   .��5i      �� -   5        $��mediaListView��5i      $��Stretch=�61   $��Disabled=�5j      $
��Auto=�6M   #��[ 5k   G   ��6   �� *PhotoMetadataList.photo+��5m      �� 5n      ! �� 
TargetType"! ��6    .�5o      �� " �� Property" � ��# �� Value# 5p   "   �� 5q   &   $ �� 
TargetType"$ ��66   .R�5r   *   �� 
������ $��0q�5s   1   
�������._�5t   .   �� 5u   +   5v   '   5x      �� 5y      " � ��$# 5q�68   �� 5z      " � ��$# 8q�67   5{      ��5}      �� 5~      .R�5      � % K� IsItemsHost% . & � Orientation$& 
Horizontal=�6;   6W   5�      5�      5�      � 5�      $��1��6   .+�5�      �� #��[ 6   ���#808080�� 5�      �6   �� $ .�# [  $ 4�� �� 5�      �6   �� *PhotoMetadataList.page+ �� 5�      �6   �� $ .�# [  $ 5�� �� 5�      �6   �� *PhotoMetadataList.pages+5�      5�      5�      5�      5�      