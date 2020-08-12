using ClinicMedical.Commands;
using ClinicMedical.Service;
using ClinicMedical.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClinicMedical.ViewModel
{
    public class MasterAdminViewModel:ViewModelBase
    {
        MasterAdminView masterAdminView;
        ServiceCode service = new ServiceCode();

        #region Constructor
        public MasterAdminViewModel(MasterAdminView masterAdminViewOpen)
        {
            masterAdminView = masterAdminViewOpen;
            GenderList=new ObservableCollection<Gender>(service.GetAllGender());
            User = new ClinicUser();
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

        private ObservableCollection<Gender> genderList;
        public ObservableCollection<Gender> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }

        private Gender selectedGender;
        public Gender SelectedGender
        {
            get
            {
                return selectedGender;
            }
            set
            {
                selectedGender = value;
                OnPropertyChanged("SelectedGender");
            }
        }

        private  ClinicUser user;
        public ClinicUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        #endregion

        #region Commands

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(param));
                }
                return save;
            }
        }

        public void SaveExecute(object parametar)
        {
            var passwordBox = parametar as PasswordBox;
            var password = passwordBox.Password;
            User.Password = password;
            User.GenderId=selectedGender.GenderId;
            User.RoleId = 1;
            try
            {
                service.AddClinicUser(User);                          
                MessageBox.Show("You have successfully added new Clinic administrator");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            User = new ClinicUser();
            masterAdminView.txtAdminPassword.Password = "";
            User.DateOfBirth = DateTime.Now;
        }

        private ICommand logOut;

        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
            }
        }

        public void LogOutExecute()
        {
            try
            {
                MainWindow main = new MainWindow();
                main.Show();
                masterAdminView.Close();
                MessageBox.Show("You have successfully logged out");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogOutExecute()
        {
            return true;
        }

        private ICommand changeCredentialsOpens;

        public ICommand ChangeCredentialsOpens
        {
            get
            {
                if (changeCredentialsOpens == null)
                {
                    changeCredentialsOpens = new RelayCommand(param => ChangeCredentialsOpensExecute(), param => CanChangeCredentialsOpensExecute());
                }
                return changeCredentialsOpens;
            }
        }

        public void ChangeCredentialsOpensExecute()
        {
            try
            {
                OpenChangePassAndUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanChangeCredentialsOpensExecute()
        {
            return true;
        }       

        private ICommand saveNewCredentials;
        public ICommand SaveNewCredentials
        {
            get
            {
                if (saveNewCredentials == null)
                {
                    saveNewCredentials = new RelayCommand(param => SaveNewCredentialsExecute(param));
                }
                return saveNewCredentials;
            }
        }

        private void SaveNewCredentialsExecute(object parametar)
        {
            try
            {
                var passwordBox = parametar as PasswordBox;
                var password = passwordBox.Password;

                if (MasterLogin.ChangeCredentials(username, password) == true)
                {
                    MessageBox.Show("Successful Changed Credentials");
                    CloseChangePassAndUser();
                }
                else
                {
                    MessageBox.Show("Invalid new credentials!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand quitNewCredentials;

        public ICommand QuitNewCredentials
        {
            get
            {
                if (quitNewCredentials == null)
                {
                    quitNewCredentials = new RelayCommand(param => QuitNewCredentialsExecute(), param => CanQuitNewCredentialsExecute());
                }
                return quitNewCredentials;
            }
        }

        public void QuitNewCredentialsExecute()
        {
            try
            {
                CloseChangePassAndUser();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanQuitNewCredentialsExecute()
        {
            return true;
        }

        private void OpenChangePassAndUser()
        {
            masterAdminView.btnChangeCredentials.Visibility = Visibility.Hidden;
            masterAdminView.lblUsername.Visibility = Visibility.Visible;
            masterAdminView.lblPassword.Visibility = Visibility.Visible;
            masterAdminView.txtUser.Visibility = Visibility.Visible;
            masterAdminView.txtPassword.Visibility = Visibility.Visible;
            masterAdminView.btnQuit.Visibility = Visibility.Visible;
            masterAdminView.btnSaveNewCredentials.Visibility = Visibility.Visible;
        }

        private void CloseChangePassAndUser()
        {
            masterAdminView.btnChangeCredentials.Visibility = Visibility.Visible;
            masterAdminView.lblUsername.Visibility = Visibility.Hidden;
            masterAdminView.lblPassword.Visibility = Visibility.Hidden;
            masterAdminView.txtUser.Visibility = Visibility.Hidden;
            masterAdminView.txtPassword.Visibility = Visibility.Hidden;
            masterAdminView.lblValidationUsername.Visibility = Visibility.Hidden;
            masterAdminView.lblValidationPassword.Visibility = Visibility.Hidden;
            masterAdminView.btnQuit.Visibility = Visibility.Hidden;
            masterAdminView.btnSaveNewCredentials.Visibility = Visibility.Hidden;
            masterAdminView.txtUser.Text = " ";
            masterAdminView.txtPassword.Password = "";
        }

        #endregion

    }
}
