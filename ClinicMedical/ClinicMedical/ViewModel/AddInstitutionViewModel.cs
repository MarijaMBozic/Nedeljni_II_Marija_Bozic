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
    public class AddInstitutionViewModel:ViewModelBase
    {
        AddInstitutionView addInstitutionView;
        ServiceCode service = new ServiceCode();

        #region Constructor
        public AddInstitutionViewModel(ClinicUser user, Institution institution, AddInstitutionView addInstitutionViewOpen, bool isForEdit)
        {
            addInstitutionView = addInstitutionViewOpen;
            this.institution = institution;
            this.user = user;
            this.isForEdit = isForEdit;
        }
        #endregion
        #region Properties
        private Institution institution;
        public Institution Institution
        {
            get
            {
                return institution;
            }
            set
            {
                institution = value;
                OnPropertyChanged("Institution");
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

        private bool isForEdit;
        public bool IsForEdit
        {
            get
            {
                return isForEdit;
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
            int currentAccessPointAmbulance = service.GetAccesPointAmbulance();
            int currentAccesspointHandicaps = service.GetAccesPointHandicaps();

            if(currentAccessPointAmbulance> Institution.AccessPointsForAmbulances ||
                currentAccesspointHandicaps>Institution.AccessPointsForhandicaps)
            {
                MessageBox.Show("The number of access points cannot be less than the current one");
            }
            else
            {
                try
                {
                    Institution.ClinicUserId = user.ClinicUserId;

                    if (service.AddInstitution(Institution) != 0)
                    {
                        MessageBox.Show("You have successfully added Institution");
                        Logging.LoggAction("AddInstitutionViewModel", "Info", "Succesfull added new Institution");
                        AdministratorView adminView = new AdministratorView(user);
                        adminView.Show();
                        addInstitutionView.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    Logging.LoggAction("AddInstitutionViewModel", "Error", ex.ToString());
                }
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
                MainWindow main = new MainWindow();
                main.Show();
                addInstitutionView.Close();
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
