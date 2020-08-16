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
    public class MaintainancViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();
        MaintainancView maintainancView;

        #region Constructor
        public MaintainancViewModel(ClinicUser user, MaintainancView maintainancViewOpen)
        {
            this.user = user;
            maintainancView = maintainancViewOpen;
            ListOFMaintenance = new ObservableCollection<vwMaintenance>(service.GetAllvwMaintainancList());
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

        private ObservableCollection<vwMaintenance> listOFMaintenance;
        public ObservableCollection<vwMaintenance> ListOFMaintenance
        {
            get
            {
                return listOFMaintenance;
            }
            set
            {
                listOFMaintenance = value;
                OnPropertyChanged("ListOFMaintenance");
            }
        }

        private vwMaintenance selectedMaintenance = new vwMaintenance();
        public vwMaintenance SelectedMaintenance
        {
            get
            {
                return selectedMaintenance;
            }
            set
            {
                selectedMaintenance = value;
                OnPropertyChanged("SelectedMaintenance");
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
                maintainancView.Close();
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
                maintainancView.Close();
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

        private ICommand addNewMaintenance;

        public ICommand AddNewMaintenance
        {
            get
            {
                if (addNewMaintenance == null)
                {
                    addNewMaintenance = new RelayCommand(param => AddNewMaintenanceExecute(), param => CanAddNewMaintenanceExecute());
                }
                return addNewMaintenance;
            }
        }

        public void AddNewMaintenanceExecute()
        {
            try
            {
                AddMaintainanceView addMaintainanceView = new AddMaintainanceView(User, new ClinicUser(), new ClinicMaintenance(), false);
                addMaintainanceView.Show();
                maintainancView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void AddEditMaintenance()
        {
            try
            {                
                ClinicUser clinicUser = new ClinicUser();
                clinicUser.ClinicUserId = selectedMaintenance.ClinicUserId;
                clinicUser.FullName = selectedMaintenance.FullName;
                clinicUser.DateOfBirth = selectedMaintenance.DateOfBirth;
                clinicUser.IDNumber = selectedMaintenance.IDNumber;
                clinicUser.GenderId = selectedMaintenance.GenderId;
                clinicUser.Citizenship = selectedMaintenance.Citizenship;
                clinicUser.Username = selectedMaintenance.Username;
                clinicUser.Password = selectedMaintenance.Password;

                ClinicMaintenance clinicMaintenance = new ClinicMaintenance();
                clinicMaintenance.ClinicMaintenanceId = selectedMaintenance.ClinicMaintenanceId;
                clinicMaintenance.ClinicUserId = selectedMaintenance.ClinicUserId;
                clinicMaintenance.PermissionToExpandClinic = selectedMaintenance.PermissionToExpandClinic;
                clinicMaintenance.ResponsibleForAccessOfHandicaps = selectedMaintenance.ResponsibleForAccessOfHandicaps;
                clinicMaintenance.ResponsibleForVehicleAccessibility = selectedMaintenance.ResponsibleForVehicleAccessibility;

                AddMaintainanceView addMaintainanceView = new AddMaintainanceView(User,clinicUser, clinicMaintenance, true);
                addMaintainanceView.Show();
                maintainancView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewMaintenanceExecute()
        {
            return true;
        }

        public void DeleteMaintenanceExecute()
        {
            try
            {
                service.DeleteUser(selectedMaintenance.ClinicUserId);
                ListOFMaintenance = new ObservableCollection<vwMaintenance>(service.GetAllvwMaintainancList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
