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
    public class AddManagerViewModel:ViewModelBase
    {
        AddManagerView addManagerView;
        ServiceCode service = new ServiceCode();

        #region Constructor
        public AddManagerViewModel(ClinicUser userAdmin, AddManagerView addManagerViewOpen)
        {
            this.userAdmin = userAdmin;
            addManagerView = addManagerViewOpen;
            GenderList = new ObservableCollection<Gender>(service.GetAllGender());
            FloorList = new ObservableCollection<int>(service.ListOfFreeFloors());
            User = new ClinicUser();
            UserManager = new ClinicManager();
        }
        #endregion
        #region Properties
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
        private ObservableCollection<int> floorList;
        public ObservableCollection<int> FloorList
        {
            get
            {
                return floorList;
            }
            set
            {
                floorList = value;
                OnPropertyChanged("FloorList");
            }
        }

        private int selectedFloor;
        public int SelectedFloor
        {
            get
            {
                return selectedFloor;
            }
            set
            {
                selectedFloor = value;
                OnPropertyChanged("SelectedFloor");
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

        private ClinicUser userAdmin;
        public ClinicUser UserAdmin
        {
            get
            {
                return userAdmin;
            }
            set
            {
                userAdmin = value;
                OnPropertyChanged("UserAdmin");
            }
        }

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

        private ClinicManager userManager;
        public ClinicManager UserManager
        {
            get
            {
                return userManager;
            }
            set
            {
                userManager = value;
                OnPropertyChanged("UserManager");
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
            User.GenderId = SelectedGender.GenderId;
            User.RoleId = 3;
            try
            {
                int userId = service.AddClinicUser(User);
                if (userId != 0)
                {
                    UserManager.ClinicUserId = userId;
                    UserManager.ClinicFloor = SelectedFloor;
                    if (service.AddNewManager(UserManager) != 0)
                    {
                        MessageBox.Show("You have successfully added new manager");
                        Logging.LoggAction("AddManagerViewModel", "Info", "Succesfull added new manager");
                        ManagerView managerView = new ManagerView(user);
                        managerView.Show();
                        addManagerView.Close();                        
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Logging.LoggAction("AddManagerViewModel", "Error", ex.ToString());
            }
        }

        

        private ICommand quit;

        public ICommand Quit
        {
            get
            {
                if (quit == null)
                {
                    quit = new RelayCommand(param => QuitExecute(), param => CanQuitExecute());
                }
                return quit;
            }
        }

        public void QuitExecute()
        {
            try
            {
                ManagerView managerView = new ManagerView(user);
                managerView.Show();
                addManagerView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanQuitExecute()
        {
            return true;
        }
        #endregion
    }
}
