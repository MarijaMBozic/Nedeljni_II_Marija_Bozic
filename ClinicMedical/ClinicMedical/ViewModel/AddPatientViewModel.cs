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
        public AddPatientViewModel(AddPatientView addPatientViewOpen)
        {
            addPatientView = addPatientViewOpen;
            GenderList = new ObservableCollection<Gender>(service.GetAllGender());          
            DoctorList = new ObservableCollection<ClinicUser>(service.GetAllDoctors());
            User = new ClinicUser();
            UserPatient = new ClinicPatient();
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

        private ObservableCollection<ClinicUser> doctorList;
        public ObservableCollection<ClinicUser> DoctorList
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

        private ClinicUser selectDoctor;
        public ClinicUser SelectedDoctor
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

                        service.AddNewPatient(UserPatient);
                        MessageBox.Show("You have successfully added new patient");
                        Logging.LoggAction("AddPatientViewModel", "Info", "Succesfull added new doctor");
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
