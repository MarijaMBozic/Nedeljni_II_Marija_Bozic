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
    public class DoctorViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();
        DoctorView doctorView;

        #region Constructor
        public DoctorViewModel(ClinicUser user, DoctorView doctorViewOpen)
        {
            this.user = user;
            doctorView = doctorViewOpen;
            ListOfDoctors = new ObservableCollection<vwDoctor>(service.GetAllvwDoctorsList());
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

        private ObservableCollection<vwDoctor> listOfDoctors;
        public ObservableCollection<vwDoctor> ListOfDoctors
        {
            get
            {
                return listOfDoctors;
            }
            set
            {
                listOfDoctors = value;
                OnPropertyChanged("ListOfDoctors");
            }
        }

        private vwDoctor selectedDoctor = new vwDoctor();
        public vwDoctor SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
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
                doctorView.Close();
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
                doctorView.Close();
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

        private ICommand addNewDoctor;

        public ICommand AddNewDoctor
        {
            get
            {
                if (addNewDoctor == null)
                {
                    addNewDoctor = new RelayCommand(param => AddNewDoctorExecute(), param => CanAddNewDoctorExecute());
                }
                return addNewDoctor;
            }
        }

        public void AddNewDoctorExecute()
        {
            try
            {
                AddDoctorView addDoctorView = new AddDoctorView(User, new ClinicUser(), new ClinicDoctor(), false);
                addDoctorView.Show();
                doctorView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewDoctorExecute()
        {
            return true;
        }

        public void EditDoctor()
        {
            try
            {
                ClinicUser clinicUser = new ClinicUser();
                clinicUser.ClinicUserId = selectedDoctor.ClinicUserId;
                clinicUser.FullName = selectedDoctor.FullName;
                clinicUser.DateOfBirth = selectedDoctor.DateOfBirth;
                clinicUser.IDNumber = selectedDoctor.IDNumber;
                clinicUser.GenderId = selectedDoctor.GenderId;
                clinicUser.Citizenship = selectedDoctor.Citizenship;
                clinicUser.Username = selectedDoctor.Username;
                clinicUser.Password = selectedDoctor.Password;

                ClinicDoctor clinicDoctor = new ClinicDoctor();
                clinicDoctor.ClinicDoctorId = selectedDoctor.ClinicDoctorId;
                clinicDoctor.ClinicUserId = selectedDoctor.ClinicUserId;
                clinicDoctor.UniqueNumber = selectedDoctor.UniqueNumber;
                clinicDoctor.BancAccount = selectedDoctor.BancAccount;
                clinicDoctor.DepartmentId = selectedDoctor.DepartmentId;
                clinicDoctor.WorkShiftId = selectedDoctor.WorkShiftId;
                clinicDoctor.InChargeOfAdmission = selectedDoctor.InChargeOfAdmission;
                clinicDoctor.ClinicManagerId = selectedDoctor.ClinicManagerId;

                AddDoctorView addDoctorView = new AddDoctorView(User, clinicUser, clinicDoctor, true);
                addDoctorView.Show();
                doctorView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DeleteDoctorExecute()
        {
            try
            {
                if(service.DeleteUser(selectedDoctor.ClinicUserId)==true)
                {
                    Logging.LoggAction("DoctorViewModel", "Info", "Succesfull deleted doctor");
                }
                ListOfDoctors = new ObservableCollection<vwDoctor>(service.GetAllvwDoctorsList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

    }
}
