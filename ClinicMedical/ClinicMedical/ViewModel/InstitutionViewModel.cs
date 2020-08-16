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
    public class InstitutionViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();
        InstitutionView institutionView;

        #region Constructor
        public InstitutionViewModel(ClinicUser user, InstitutionView institutionViewOpen)
        {
            this.user = user;
            institutionView = institutionViewOpen;
            ListOFInstitution = new ObservableCollection<Institution>(service.GetAllInstitution());
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

        private ObservableCollection<Institution> listOFInstitution;
        public ObservableCollection<Institution> ListOFInstitution
        {
            get
            {
                return listOFInstitution;
            }
            set
            {
                listOFInstitution = value;
                OnPropertyChanged("ListOFInstitution");
            }
        }

        private Institution selectedInstitution = new Institution();
        public Institution SelectedInstitution
        {
            get
            {
                return selectedInstitution;
            }
            set
            {
                selectedInstitution = value;
                OnPropertyChanged("SelectedInstitution");
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
                institutionView.Close();
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
                institutionView.Close();
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

        public void EditInstitution()
        {
            try
            {
                Institution institution = new Institution();
                institution.ClinicUserId = selectedInstitution.ClinicUserId;
                institution.Name = selectedInstitution.Name;
                institution.BuildDate = selectedInstitution.BuildDate;
                institution.Address = selectedInstitution.Address;
                institution.NumberOfFloors = selectedInstitution.NumberOfFloors;
                institution.NumberOfRoomsPerFloor = selectedInstitution.NumberOfRoomsPerFloor;
                institution.Backyard = selectedInstitution.Backyard;
                institution.Terrace = selectedInstitution.Terrace;
                institution.InstitutionId = selectedInstitution.InstitutionId;
                institution.Owner = selectedInstitution.Owner;
                institution.AccessPointsForAmbulances = selectedInstitution.AccessPointsForAmbulances;
                institution.AccessPointsForhandicaps = selectedInstitution.AccessPointsForhandicaps;

                AddInstitutionView addInstitutionView = new AddInstitutionView(User, institution, true);
                addInstitutionView.Show();
                institutionView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
