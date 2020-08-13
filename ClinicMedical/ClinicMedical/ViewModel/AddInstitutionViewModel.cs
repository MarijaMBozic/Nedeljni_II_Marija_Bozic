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
        public AddInstitutionViewModel(AddInstitutionView addInstitutionViewOpen)
        {
            addInstitutionView = addInstitutionViewOpen;
            Institution = new Institution();
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
