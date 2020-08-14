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
        ServiceCode service = new ServiceCode();
        
        #region Constructor
        public MainWindowViewModel(MainWindow mainOpen)
        {
            this.main = mainOpen;
            Institution = service.GetAllInstitution();
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

        private List<Institution> institution;
        public List<Institution> Institution
        {
            get
            {
                return institution;
            }
            set
            {
                institution = value;
                OnPropertyChanged("Institution");
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
            var passwordBox = parametar as PasswordBox;
            var password = passwordBox.Password;
            try
            {
                if (MasterLogin.Login(username, password) == true)
                {
                  
                        MessageBox.Show("Successful login");
                        MasterAdminView window = new MasterAdminView();
                        window.Show();
                        main.Close();
                                     
                }
                else if(MasterLogin.Login(username, password) == false)
                {
                    ClinicUser user = service.LoginUser(username, password);
                    if(user != null)
                    {
                        if(user.RoleId==1)
                        {
                            if (Institution.Count == 0)
                            {
                                MessageBox.Show("Successful login");
                                AddInstitutionView window = new AddInstitutionView(user);
                                window.Show();
                                main.Close();
                            }
                            else
                            {
                                MessageBox.Show("Successful login");
                                AdministratorView window = new AdministratorView(user);
                                window.Show();
                                main.Close();
                            }
                        }
                        else if(user.RoleId == 2)
                        {
                            MessageBox.Show("Successful login maintainanc");
                        }
                        else if (user.RoleId == 3)
                        {
                            MessageBox.Show("Successful login manager");

                        }
                        else if (user.RoleId == 4)
                        {
                            MessageBox.Show("Successful login doctor");
                        }
                        else if (user.RoleId == 5)
                        {
                            MessageBox.Show("Successful login patient");
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("Wrong user or password credentials");
                    }
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
