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
    public class AddMaintainancViewModel:ViewModelBase
    {
        AddMaintainanceView addMaintainanceView;
        ServiceCode service = new ServiceCode();

        #region Constructor
        public AddMaintainancViewModel(ClinicUser adminUser, ClinicUser user, ClinicMaintenance clinicMaintenance, AddMaintainanceView addMaintainanceViewOpen)
        {
            this.adminUser = adminUser;
            this.userMaintainance=clinicMaintenance;
            addMaintainanceView = addMaintainanceViewOpen;
            GenderList = new ObservableCollection<Gender>(service.GetAllGender());
            SelectedGender = GenderList.FirstOrDefault(p => p.GenderId == user.GenderId);
            this.user = user;            
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

        private ClinicUser adminUser;
        public ClinicUser AdminUser
        {
            get
            {
                return adminUser;
            }
            set
            {
                adminUser = value;
                OnPropertyChanged("AdminUser");
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

        private ClinicMaintenance userMaintainance;
        public ClinicMaintenance UserMaintainance
        {
            get
            {
                return userMaintainance;
            }
            set
            {
                userMaintainance = value;
                OnPropertyChanged("UserMaintainance");
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
            User.RoleId = 2;
            try
            {  
                int userId = service.AddClinicUser(User);
                if (userId != 0)
                {
                    UserMaintainance.ClinicUserId = userId;
                    if(service.AddNewMaintainance(UserMaintainance)!=0)
                    {
                        service.ChackNumberOfMaintainanc();
                    }
                    MessageBox.Show("You have successfully added new maintainance");
                    Logging.LoggAction("AddMaintainanceViewModel", "Info", "Succesfull added new doctor");
                    MaintainancView maintainanceView = new MaintainancView(AdminUser);
                    maintainanceView.Show();
                    addMaintainanceView.Close();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Logging.LoggAction("AddDoctorViewModel", "Error", ex.ToString());
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
                MaintainancView maintainancView = new MaintainancView(AdminUser);
                maintainancView.Show();
                addMaintainanceView.Close();
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
