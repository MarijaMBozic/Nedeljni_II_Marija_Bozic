using ClinicMedical.Service;
using ClinicMedical.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.ViewModel
{
    public class ManagerViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();

        ManagerView managerView;
        #region Constructor

        public ManagerViewModel(ManagerView managerViewOpend)
        {
            managerView = managerViewOpend;
            ListOFManagers = new ObservableCollection<vwManager>(service.GetAllvwDoctorsList());
        }
        #endregion

        #region Properties
        private ObservableCollection<vwManager> listOFManagers;
        public ObservableCollection<vwManager> ListOFManagers
        {
            get
            {
                return listOFManagers;
            }
            set
            {
                listOFManagers = value;
                OnPropertyChanged("ListOFManagers");
            }
        }
        #endregion
    }
}
