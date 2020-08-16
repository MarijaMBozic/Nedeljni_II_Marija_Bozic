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
    public class AddPatientViewModel:ViewModelBase
    {
        AddPatientView addPatientView;
        ServiceCode service = new ServiceCode();

        #region Constructor
        public AddPatientViewModel(ClinicUser userAdmin, ClinicUser user, ClinicPatient patient, AddPatientView addPatientViewOpen, bool isForEdit)
        {
            this.userAdmin = userAdmin;
            this.user = user;
            this.userPatient = patient;            
            this.isForEdit = isForEdit;
            addPatientView = addPatientViewOpen;
            GenderList = new ObservableCollection<Gender>(service.GetAllGender());
            SelectedGender = GenderList.FirstOrDefault(p => p.GenderId == user.GenderId);
            DoctorList = new ObservableCollection<vwDoctor>(service.GetAllDoctors());
            SelectedDoctor = DoctorList.FirstOrDefault(p=>p.UniqueNumber==patient.UniqueDoctorNumber);
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


        private bool isForEdit;
        public bool IsForEdit
        {
            get
            {
                return isForEdit;
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

        private ObservableCollection<vwDoctor> doctorList;
        public ObservableCollection<vwDoctor> DoctorList
        {
            get
            {
                return doctorList;
            }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }

        private vwDoctor selectDoctor;
        public vwDoctor SelectedDoctor
        {
            get
            {
                return selectDoctor;
            }
            set
            {
                selectDoctor = value;
                OnPropertyChanged("SelectedDoctor");
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

        private ClinicPatient userPatient;
        public ClinicPatient UserPatient
        {
            get
            {
                return userPatient;
            }
            set
            {
                userPatient = value;
                OnPropertyChanged("UserPatient");
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
            User.RoleId = 5;
            try
            {
                bool uniqueInsuranceNumber = service.CheckInsuranceNumber(UserPatient.InsuranceNumber);
                int drUniqueNumber = service.GetDoctorUniqueNumberByDoctorId(selectDoctor.ClinicUserId);
                if (uniqueInsuranceNumber)
                {
                    int userId = service.AddClinicUser(User);
                    if (userId != 0)
                    {
                        UserPatient.ClinicUserId = userId;
                        UserPatient.UniqueDoctorNumber = drUniqueNumber;

                        if (service.AddNewPatient(UserPatient) != 0)
                        {
                            MessageBox.Show("You have successfully added new patient");
                            Logging.LoggAction("AddPatientViewModel", "Info", "Succesfull added new doctor");

                            PatientView patientView = new PatientView(UserAdmin);
                            patientView.Show();
                            addPatientView.Close();
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
                PatientView patientView = new PatientView(UserAdmin);
                patientView.Show();
                addPatientView.Close();
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
