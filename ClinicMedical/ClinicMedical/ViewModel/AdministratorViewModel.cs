using ClinicMedical.Commands;
using ClinicMedical.Service;
using ClinicMedical.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClinicMedical.ViewModel
{
    public class AdministratorViewModel:ViewModelBase
    {
        AdministratorView administratorView;
        ServiceCode service = new ServiceCode();

        #region Constructor
        public AdministratorViewModel(ClinicUser user, AdministratorView administratorViewOpen)
        {
            this.user = user;
            administratorView = administratorViewOpen;

        }
        #endregion
        #region
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
                administratorView.Close();
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

        private ICommand goToInstitution;

        public ICommand GoToInstitution
        {
            get
            {
                if (goToInstitution == null)
                {
                    goToInstitution = new RelayCommand(param => GoToInstitutionExecute(), param => CanGoToInstitutionExecute());
                }
                return goToInstitution;
            }
        }

        public void GoToInstitutionExecute()
        {
            try
            {
                InstitutionView main = new InstitutionView();
                main.Show();
                administratorView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanGoToInstitutionExecute()
        {
            return true;
        }
               
        private ICommand goToMaintainanc;

        public ICommand GoToMaintainanc
        {
            get
            {
                if (goToMaintainanc == null)
                {
                    goToMaintainanc = new RelayCommand(param => GoToMaintainancExecute(), param => CanGoToMaintainancExecute());
                }
                return goToMaintainanc;
            }
        }

        public void GoToMaintainancExecute()
        {
            try
            {
                MaintainancView main = new MaintainancView(user);
                main.Show();
                administratorView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanGoToMaintainancExecute()
        {
            return true;
        }

        private ICommand goToManager;

        public ICommand GoToManager
        {
            get
            {
                if (goToManager == null)
                {
                    goToManager = new RelayCommand(param => GoToManagerExecute(), param => CanGoToManagerExecute());
                }
                return goToManager;
            }
        }

        public void GoToManagerExecute()
        {
            try
            {
                ManagerView main = new ManagerView(user);
                main.Show();
                administratorView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanGoToManagerExecute()
        {
            return true;
        }

        private ICommand goToDoctor;

        public ICommand GoToDoctor
        {
            get
            {
                if (goToDoctor == null)
                {
                    goToDoctor = new RelayCommand(param => GoToDoctorExecute(), param => CanGoToDoctorExecute());
                }
                return goToDoctor;
            }
        }

        public void GoToDoctorExecute()
        {
            try
            {
                DoctorView main = new DoctorView(user);
                main.Show();
                administratorView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanGoToDoctorExecute()
        {
            return true;
        }        

        private ICommand goToPatient;

        public ICommand GoToPatient
        {
            get
            {
                if (goToPatient == null)
                {
                    goToPatient = new RelayCommand(param => GoToPatientExecute(), param => CanGoToPatientExecute());
                }
                return goToPatient;
            }
        }

        public void GoToPatientExecute()
        {
            try
            {
                PatientView main = new PatientView();
                main.Show();
                administratorView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanGoToPatientExecute()
        {
            return true;
        }
        #endregion
    }
}
