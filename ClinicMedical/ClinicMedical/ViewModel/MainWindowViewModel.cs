using ClinicMedical.Commands;
using ClinicMedical.Service;
using ClinicMedical.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClinicMedical.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        MainWindow main;
        
        #region Constructor

        public MainWindowViewModel(MainWindow mainOpen)
        {
            this.main = mainOpen;
        }
        #endregion

        #region Properties
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        #endregion

        #region Command
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(param));
                }
                return login;
            }
        }

        private void LoginExecute(object parametar)
        {
            try
            {
                var passwordBox = parametar as PasswordBox;
                var password = passwordBox.Password;

                if (MasterLogin.Login(username, password) == true)
                {
                    MessageBox.Show("Successful login");
                    MasterAdminView window = new MasterAdminView();
                    window.Show();
                    main.Close();
                }
                //else if()
                else
                {
                    MessageBox.Show("Wrong user or password credentials");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        
    }
}
