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
         
        //    GenderList = new ObservableCollection<Gender>(service.GetAllGender());
        //    SelectedGender = GenderList.FirstOrDefault(p => p.GenderId == user.GenderId);
        //    FloorList = new ObservableCollection<int>(service.ListOfFreeFloors());
        //    SelectedFloor = FloorList.FirstOrDefault(p => p.Equals(userManager.ClinicFloor));
   
        #region Constructor
        public AddDoctorViewModel(ClinicUser userAdmin, ClinicUser user, ClinicDoctor clinicDoctor,  AddDoctorView addDoctorViewOpen)
        {
            this.userAdmin = userAdmin;
            this.userDoctor = clinicDoctor;
            this.user = user;
            addDoctorView = addDoctorViewOpen;
            GenderList = new ObservableCollection<Gender>(service.GetAllGender());
            SelectedGender = GenderList.FirstOrDefault(p => p.GenderId == user.GenderId);
            DepartmentList = new ObservableCollection<Department>(service.GetAllDepartment());
            SelectedDepartment = DepartmentList.FirstOrDefault(p => p.DepartmentId == userDoctor.DepartmentId);
            WorkShiftList = new ObservableCollection<Workshift>(service.GetAllWorkshift());
            SelectedWorkShift = WorkShiftList.FirstOrDefault(p => p.WorkShiftId == userDoctor.WorkShiftId);
            ManagerList = new ObservableCollection<vwManager>(service.GetAllManager());
           
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

        private ObservableCollection<vwManager> managerList;
        public ObservableCollection<vwManager> ManagerList
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

        private vwManager selectedManager;
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
                if (User.ClinicUserId == 0)
                {
                    bool uniqueNumber = service.CheckUniqueNumber(UserDoctor.UniqueNumber);
                    bool uniqueBancAccount = service.CheckBancAccount(UserDoctor.BancAccount);
                    if (uniqueNumber && uniqueBancAccount)
                    {
                        int userId = service.AddClinicUser(User);
                        if (userId != 0)
                        {
                            UserDoctor.ClinicUserId = userId;
                            UserDoctor.DepartmentId = selectedDepartment.DepartmentId;
                            UserDoctor.WorkShiftId = selectedWorkShift.WorkShiftId;
                            UserDoctor.ClinicManagerId = selectedManager.ClinicManagerId;

                            if (service.AddNewDoctor(UserDoctor) != 0)
                            {
                                MessageBox.Show("You have successfully added new doctor");
                                Logging.LoggAction("AddDoctorViewModel", "Info", "Succesfull added new doctor");

                                DoctorView doctorView = new DoctorView(UserAdmin);
                                doctorView.Show();
                                addDoctorView.Close();
                            }
                        }
                    }
                }
                else { 
                    int userId = service.AddClinicUser(User);
                    if (userId != 0)
                    {
                        UserDoctor.ClinicUserId = userId;
                        UserDoctor.DepartmentId = selectedDepartment.DepartmentId;
                        UserDoctor.WorkShiftId = selectedWorkShift.WorkShiftId;
                        UserDoctor.ClinicManagerId = selectedManager.ClinicManagerId;

                        if (service.AddNewDoctor(UserDoctor) != 0)
                        {
                            MessageBox.Show("You have successfully added new doctor");
                            Logging.LoggAction("AddDoctorViewModel", "Info", "Succesfull added new doctor");

                            DoctorView doctorView = new DoctorView(UserAdmin);
                            doctorView.Show();
                            addDoctorView.Close();
                        }
                    }
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
                DoctorView doctorView = new DoctorView(UserAdmin);
                doctorView.Show();
                addDoctorView.Close();
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
