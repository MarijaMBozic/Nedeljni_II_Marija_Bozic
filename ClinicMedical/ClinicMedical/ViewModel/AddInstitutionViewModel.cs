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
        public AddInstitutionViewModel(ClinicUser user, AddInstitutionView addInstitutionViewOpen)
        {
            addInstitutionView = addInstitutionViewOpen;
            Institution = new Institution();
            this.user = user;
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
            try
            {
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
        #endregion
    }
}
