   M S B A M L   `   `   `  ����?  ;QLVT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:QLVTQLVT  5   	   ;"clr-namespace:QLVT.UserControlQLVTQLVT.UserControlQLVT  5
   	       QLVT.LichSuNXVT   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:QLVT     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   +uc"clr-namespace:QLVT.UserControlQLVT  5
   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��PhieuNhapXuat��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � Title$% PHIẾU NHẬP XUẤT VẬT TƯ��5   	   $	��600��6(   $
��1000��65    � 	Resources 5         QLVT.QLVTDataSet1  qLVTDataSet1  pHATSINHViewSource �� Source �� Source  cT_PHATSINHViewSource��%�   &	       &	      7  &	 A     7    5   
   6   �� 5   
    6:   �� *
PHATSINH+8   �� 5   
    6=   �� *CT_PHATSINH+8   5      .  (System.Windows.Interactivity.Interaction	   Triggers	 5      /  )System.Windows.Interactivity.EventTrigger  5    
   
   	EventName$
 Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5!      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedThuChiCommand+   CommandParameter 6L   ��  �� ElementName$ PhieuThuChi��5"      .��5%      �  � RowDefinitions 5&   
   �� 5'      $
��auto �6   6,   �� 5(      6   .+�5*   
   � $��0��6   .+�5+      '   !QLVT.UserControlQLVT.ControlBarUC  
 � Tag 6   �� *Title+$ PhieuThuChi��6O   5,      � 5-   
   -   5        $��1��5-       5.      �� 5/      $
��auto �6    60   �� 50      $
��auto �6    60   �� 51      6"   .+�53      � $��0��6    � ColumnDefinitions 54      �� 55      $��3* �6'   64   �� 56      6)   .+�58      � $��0��6   .+�59      �� $��	15 5 15 5q�6    $��	ExtraBold"�63   $��Center=�6J   
���� ��$��Tahoma)�6|   $)��#LỊCH SỬ NHẬP/XUẤT VẬT TƯ��6�   $��30(�6�   $��Center=�6�   6�   5:      � 5;      $��1��6   .+�5<      �� -   5         ��Name$ 
comboBoxCN��5<      $
��Left=�63   $��Center=�6N   $	��180��6i   $��38��6u   6�   5=      5>      � 5?      $��1��6   .+�5@      #  MaterialDesignThemes.Wpf.Card  $��15,10.6,14.8,0q�6&   $��Stretch=�6>   .��5A      � $��2��6   .+�5B      ��  �� Orientation$ 
Horizontal=�6%    5C       �� 
TargetType �� Property �� Value��%�   '��       �� 5D   "   " ��6(   .�5E   &   ��  � ��$ 15(�6A   6N   �� 5F   &    � ��$
 Bold"�6C   6R   �� 5G   &    � ��$ 40��6?   6L   �� 5H   &    � ��$ 15q�6?   6L   5I   #   5J      .+�5K      �� -   5         � ToolTip$  Xem tất cả lịch sử��5K   %   ���#FF8A00$��Xem Tất Cả��6W   $	��120��6l   6�   �� 5L      -   5        $  Xóa Phiếu Nhập/Xuất��5L   %   ���#FF1A1A$��Xóa PHIẾU��6X   $	��110��6l   6�   �� 5M      -   5        $+ %Xóa Chi Tiết Phiếu Nhập/Xuất��5M   %   ���#89ffaa$��Xóa CT_PHIẾU��6a   $	��120��6x   6�   �� 5N      -   5        $+ %Thêm phiếu Nhập/Xuất vật tư��5N   %   ���#d5ea83$��PHIẾU NHẬP/XUẤT��6`   $	��150��6z   6�   �� 5O      -   5        $ Thêm phiếu Thu/Chi��5O   %   ���#5389b2$��PHIẾU THU/CHI��6W   $	��150��6o   6�   �� 5P      -	   5        ���#aa68e8$��IN DANH SÁCH��5P   ]   $	��140��6t   ��6%   �� *INDSNVCommand+6�   5Q      5R      5S      5T      � 5U      $��2��6    � DataContext# [ 6     5V      �� 5W      $��5* �6'   64   �� 5X      $
��auto �6'   66   .+�5Z      � $��0��6   # [ 6'   .+�5[        $" Thông tin nhập/xuất kho��6*   $��
15 15 7 15q�6L   $��Stretch=�6`   .��5\      �� $��Disabled=�6'   .��5]      &	   System.Windows.Controls.DataGrid	  -
   5         	 Name$ pHATSINHDataGrid��5]   '    	  AutoGenerateColumns .   	  EnableRowVirtualization .  	  RowDetailsVisibilityMode$ VisibleWhenSelected=�6�   ��6|   ��  	  Columns 5^   "   4
  .System.Windows.Controls.DataGridTemplateColumn
  5_   &   -   5        pHIEUColumn��,  &System.Windows.Controls.DataGridColumn   Header$ PHIEU��5_   R      Width5  /System.Windows.Controls.DataGridLengthConverter$ 90 6a     
  CellTemplate  5`   *   �� 5a   .   .R�5b   2   �� ��6<   �� *PHIEU+5c   /   5e   '   
  5f   &   -   5        
nGAYColumn��$ NGÀY��5f   Q   $	 105 6_     5g   *   �� 5h   .   .R�5i   2   (  "System.Windows.Controls.DatePicker  !   SelectedDate! 6=   �� *NGAY+" �� Mode$" TwoWay=�# �� NotifyOnValidationError# . $ �� ValidatesOnExceptions$ . 5j   /   5l   '   0  *System.Windows.Controls.DataGridTextColumn  5m   &   -   5        
lOAIColumn��$ LOẠI��5m   f   $ 60 6t   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6M   �� *LOAI+6t     5n   &   -   5        hOTENKHColumn��$ KHÁCH HÀNG��5n   l   $	 130 6�   % 6P   �� *	HOTENKH+6�     5o   &   -   5        tHANHTIENColumn��$ THÀNH TIỀN��5o   p   $	 105 6�   % 6R   �� *	THANHTIEN+6�     5p   &   -   5        
mANVColumn��$
 MANV��5p   f   $ 70 6t   % 6M   �� *MANV+6t     5q   &   -   5        mAKHOColumn��$ MAKHO��5q   h   $	 100 6w   % 6N   �� *MAKHO+6w   5s      5t      5u      5v      � 5x      $��1��6   # [ 6'   .+�5y        $��
7 15 15 15q�6*   $��Stretch=�6>   .��5z      �� $��Disabled=�6'   .��5{      	  -   5        $ cT_PHATSINHDataGrid��5{   '    .   . $ VisibleWhenSelected=�6�   ��6   ��  5|   "     5}   &   -   5        
mAVTColumn��$
 MAVT��5}   f   $	 100 6t   % 6M   �� *MAVT+6t     5~   &   -   5        sOLUONGColumn��$ SỐ LƯỢNG��5~   l   $	 100 6~   % 6P   �� *	SOLUONG+6~     5   &   -   5        dONGIAColumn��$ 
ĐƠN GIÁ��5   j   $	 100 6{   % 6O   �� *DONGIA+6{   5�      5�      5�      5�      5�      5�      5�      5�      