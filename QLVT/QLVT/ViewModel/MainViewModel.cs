using QLVT.DBConnection;
using QuanLyKho.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLVT.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;

        #region commands
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand NhanVienWindowCommand { get; set; }
        public ICommand VatTuWindowCommand { get; set; }
        public ICommand KhoWindowCommand { get; set; }
        public ICommand NhapXuatVatTuWindowCommand { get; set; }
        public ICommand PhieuThuChiWindowCommand { get; set; }
        public ICommand LichSuNXVTWindowCommand { get; set; }
        public ICommand LoadedReportNhanVienCommand { get; set; }
        public ICommand LoadedReportVatTuCommand { get; set; }
        public ICommand LoadedReportChiTietNXCommand { get; set; }
        public ICommand LoadedReportTHNXCommand { get; set; }
        #endregion
        // mọi thứ xử lý sẽ nằm trong này
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                //Isloaded = true;
                if (p == null)
                    return;
                //p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                //if (loginWindow.DataContext == null)
                //    return; 

                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                }

            });

            NhanVienWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {              
                NhanVien nhanvienWindow = new NhanVien();
                nhanvienWindow.ShowDialog();
            });

            VatTuWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                VatTu vattuWindow = new VatTu();
                vattuWindow.ShowDialog();
            });

            KhoWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Kho khoWindow = new Kho();
                khoWindow.ShowDialog();
            });

            NhapXuatVatTuWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                NhapXuatKho nhapxuatvattuWindow = new NhapXuatKho();
                nhapxuatvattuWindow.ShowDialog();
            });

            PhieuThuChiWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                PhieuThuChi phieuthuchiWindow = new PhieuThuChi();
                phieuthuchiWindow.ShowDialog();
            });

            LichSuNXVTWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LichSuNXVT lichsunxvtWindow = new LichSuNXVT();
                lichsunxvtWindow.ShowDialog();
            });

            LoadedReportNhanVienCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                ReportNhanVien reportnhanvienwindow = new ReportNhanVien();
                reportnhanvienwindow.ShowDialog();
            });

            LoadedReportVatTuCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                ReportVatTu reportvattuwindow = new ReportVatTu();
                reportvattuwindow.ShowDialog();
            });

            LoadedReportChiTietNXCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                ReportChiTietNX reportchitietnxwindow = new ReportChiTietNX();
                reportchitietnxwindow.ShowDialog();
            });

            LoadedReportTHNXCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                ReportTHNX reportthnxwindow = new ReportTHNX();
                reportthnxwindow.ShowDialog();
            });

        }
    }
}
