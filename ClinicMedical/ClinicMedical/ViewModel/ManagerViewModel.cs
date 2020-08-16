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
using System.Windows.Input;

namespace ClinicMedical.ViewModel
{
    public class ManagerViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();

        ManagerView managerView;
        #region Constructor

        public ManagerViewModel(ClinicUser user, ManagerView managerViewOpend)
        {
            this.user = user;
            managerView = managerViewOpend;
            ListOFManagers = new ObservableCollection<vwManager>(service.GetAllvwManagersList());
        }
        #endregion

        #region Properties

        private ClinicUser user;
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

        private ObservableCollection<vwManager> listOFManagers;
        public ObservableCollection<vwManager> ListOFManagers
        {
            get
            {
                return listOFManagers;
            }
            set
            {
                listOFManagers = value;
                OnPropertyChanged("ListOFManagers");
            }
        }

        private vwManager selectedManager = new vwManager();
        public vwManager SelectedManager
        {
            get
            {
                return selectedManager;
            }
            set
            {
                selectedManager = value;
                OnPropertyChanged("SelectedManager");
            }
        }

        #endregion
        #region Commands        

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
                managerView.Close();
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

        private ICommand backToAdminView;

        public ICommand BackToAdminView
        {
            get
            {
                if (backToAdminView == null)
                {
                    backToAdminView = new RelayCommand(param => BackToAdminViewExecute(), param => CanBackToAdminViewExecute());
                }
                return backToAdminView;
            }
        }

        public void BackToAdminViewExecute()
        {
            try
            {
                AdministratorView adminView = new AdministratorView(user);
                adminView.Show();
                managerView.Close();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanBackToAdminViewExecute()
        {
            return true;
        }

        private ICommand addNewMenager;

        public ICommand AddNewMenager
        {
            get
            {
                if (addNewMenager == null)
                {
                    addNewMenager = new RelayCommand(param => AddNewMenagerExecute(), param => CanAddNewMenagerExecute());
                }
                return addNewMenager;
            }
        }

        public void AddNewMenagerExecute()
        {
            try
            {               
                AddManagerView addManagerView = new AddManagerView(User, new ClinicUser(), new ClinicManager(), false);
                addManagerView.Show();
                managerView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewMenagerExecute()
        {
            return true;
        }

        public void DeleteManagerExecute()
        {
            try
            {
               if(service.DeleteUser(selectedManager.ClinicUserId)==true)
                {
                    Logging.LoggAction("ManagerViewModel", "Info", "Succesfull deleted manager");
                }
                ListOFManagers = new ObservableCollection<vwManager>(service.GetAllvwManagersList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void EditManager()
        {
            try
            {
                ClinicUser clinicUser = new ClinicUser();
                clinicUser.ClinicUserId = selectedManager.ClinicUserId;
                clinicUser.FullName = selectedManager.FullName;
                clinicUser.DateOfBirth = selectedManager.DateOfBirth;
                clinicUser.IDNumber = selectedManager.IDNumber;
                clinicUser.GenderId = selectedManager.GenderId;
                clinicUser.Citizenship = selectedManager.Citizenship;
                clinicUser.Username = selectedManager.Username;
                clinicUser.Password = selectedManager.Password;

                ClinicManager clinicManager = new ClinicManager();
                clinicManager.ClinicManagerId = selectedManager.ClinicManagerId;
                clinicManager.ClinicUserId = selectedManager.ClinicUserId;
                clinicManager.ClinicFloor = selectedManager.ClinicFloor;
                clinicManager.MaxNumOfDoctorsSupervised = selectedManager.MaxNumOfDoctorsSupervised;
                clinicManager.MinNumOfRoomSupervised = selectedManager.MinNumOfRoomSupervised;
                clinicManager.NumberOfMistake = selectedManager.NumberOfMistake;

                AddManagerView addManagerView = new AddManagerView(User, clinicUser, clinicManager, true);
                addManagerView.Show();
                managerView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
