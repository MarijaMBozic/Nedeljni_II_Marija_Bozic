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
    public class AddDoctorViewModel:ViewModelBase
    {
        AddDoctorView addDoctorView;
        ServiceCode service = new ServiceCode();

        #region Constructor
        public AddDoctorViewModel(AddDoctorView addDoctorViewOpen)
        {
            addDoctorView = addDoctorViewOpen;
            GenderList = new ObservableCollection<Gender>(service.GetAllGender());
            DepartmentList = new ObservableCollection<Department>(service.GetAllDepartment());
            WorkShiftList= new ObservableCollection<Workshift>(service.GetAllWorkshift());
            ManagerList = new ObservableCollection<ClinicUser>(service.GetAllManager());
            User = new ClinicUser();
            UserDoctor = new ClinicDoctor();
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
       
        private ObservableCollection<Department> departmentList;
        public ObservableCollection<Department> DepartmentList
        {
            get
            {
                return departmentList;
            }
            set
            {
                departmentList = value;
                OnPropertyChanged("DepartmentList");
            }
        }

        private Department selectedDepartment;
        public Department SelectedDepartment
        {
            get
            {
                return selectedDepartment;
            }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }

        private ObservableCollection<Workshift> workShiftList;
        public ObservableCollection<Workshift> WorkShiftList
        {
            get
            {
                return workShiftList;
            }
            set
            {
                workShiftList = value;
                OnPropertyChanged("WorkShiftList");
            }
        }

        private Workshift selectedWorkShift;
        public Workshift SelectedWorkShift
        {
            get
            {
                return selectedWorkShift;
            }
            set
            {
                selectedWorkShift = value;
                OnPropertyChanged("SelectedWorkShift");
            }
        }

        private ObservableCollection<ClinicUser> managerList;
        public ObservableCollection<ClinicUser> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
            }
        }

        private ClinicUser selectedManager;
        public ClinicUser SelectedManager
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

        private ClinicDoctor userDoctor;
        public ClinicDoctor UserDoctor
        {
            get
            {
                return userDoctor;
            }
            set
            {
                userDoctor = value;
                OnPropertyChanged("UserDoctor");
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
            User.RoleId =4;
            try
            {
                bool uniqueNumber =service.CheckUniqueNumber(UserDoctor.UniqueNumber);
                bool uniqueBancAccount = service.CheckBancAccount(UserDoctor.BancAccount);
                if (uniqueNumber && uniqueBancAccount)
                {
                    int userId = service.AddClinicUser(User);
                    if (userId != 0)
                    {
                        UserDoctor.ClinicUserId = userId;
                        UserDoctor.DepartmentId = selectedDepartment.DepartmentId;
                        UserDoctor.WorkShiftId = selectedWorkShift.WorkShiftId;
                        UserDoctor.ClinicManagerId = selectedManager.ClinicUserId;

                        service.AddNewDoctor(UserDoctor);
                        MessageBox.Show("You have successfully added new doctor");
                        Logging.LoggAction("AddDoctorViewModel", "Info", "Succesfull added new doctor");
                    }
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Logging.LoggAction("AddDoctorViewModel", "Error", ex.ToString());
            }
        }
            #endregion
    }
}
