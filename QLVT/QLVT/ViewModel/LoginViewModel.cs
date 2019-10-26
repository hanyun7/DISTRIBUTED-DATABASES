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
    public class LoginViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseCommand { get; set; }
        #endregion

        public bool IsLogin { get; set; }
        

        public LoginViewModel()
        {
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
        }

    }
}
