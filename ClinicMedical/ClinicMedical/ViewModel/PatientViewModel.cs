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
    public class PatientViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();

        PatientView patientView;
        #region Constructor

        public PatientViewModel(ClinicUser user, PatientView patientViewOpen)
        {
            this.user = user;
            patientView = patientViewOpen;
            ListOfPatients = new ObservableCollection<vwPatient>(service.GetAllvwPatientsList());
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

        private ObservableCollection<vwPatient> listOfPatients;
        public ObservableCollection<vwPatient> ListOfPatients
        {
            get
            {
               return listOfPatients;
            }
            set
            {
                listOfPatients = value;
                OnPropertyChanged("ListOfPatients");
            }
        }

        private vwPatient selectedPatient = new vwPatient();
        public vwPatient SelectedPatient
        {
            get
            {
                return selectedPatient;
            }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
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
                patientView.Close();
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
                patientView.Close();
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

        private ICommand addNewPatient;

        public ICommand AddNewMenager
        {
            get
            {
                if (addNewPatient == null)
                {
                    addNewPatient = new RelayCommand(param => AddNewMenagerExecute(), param => CanAddNewMenagerExecute());
                }
                return addNewPatient;
            }
        }

        public void AddNewMenagerExecute()
        {
            try
            {
                AddPatientView addManagerView = new AddPatientView(User, new ClinicUser(), new ClinicPatient(), false);
                addManagerView.Show();
                patientView.Close();
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
                if(service.DeleteUser(selectedPatient.ClinicUserId)==true)
                {
                    Logging.LoggAction("PatientViewModel", "Info", "Succesfull deleted patient");
                }
                ListOfPatients = new ObservableCollection<vwPatient>(service.GetAllvwPatientsList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void EditPatient()
        {
            try
            {
                ClinicUser clinicUser = new ClinicUser();
                clinicUser.ClinicUserId = selectedPatient.ClinicUserId;
                clinicUser.FullName = selectedPatient.FullName;
                clinicUser.DateOfBirth = selectedPatient.DateOfBirth;
                clinicUser.IDNumber = selectedPatient.IDNumber;
                clinicUser.GenderId = selectedPatient.GenderId;
                clinicUser.Citizenship = selectedPatient.Citizenship;
                clinicUser.Username = selectedPatient.Username;
                clinicUser.Password = selectedPatient.Password;

                ClinicPatient clinicPatient = new ClinicPatient();
                clinicPatient.InsuranceNumber = selectedPatient.InsuranceNumber;
                clinicPatient.InsuranceExpirationDate = selectedPatient.InsuranceExpirationDate;
                clinicPatient.UniqueDoctorNumber = selectedPatient.UniqueDoctorNumber;

                AddPatientView addPatientView = new AddPatientView(User, clinicUser, clinicPatient, true);
                addPatientView.Show();
                patientView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
