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
        public AddManagerViewModel(AddManagerView addManagerViewOpen)
        {
            addManagerView = addManagerViewOpen;
            GenderList = new ObservableCollection<Gender>(service.GetAllGender());
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
            User.GenderId = selectedGender.GenderId;
            User.RoleId = 3;
            try
            {
                int userId = service.AddClinicUser(User);
                if (userId != 0)
                {
                    UserManager.ClinicUserId = userId;                    
                    service.AddNewManager(UserManager);
                    MessageBox.Show("You have successfully added new manager");
                    Logging.LoggAction("AddManagerViewModel", "Info", "Succesfull added new manager");
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Logging.LoggAction("AddManagerViewModel", "Error", ex.ToString());
            }
        }
        #endregion
    }
}
