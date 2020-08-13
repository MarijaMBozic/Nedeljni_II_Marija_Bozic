using ClinicMedical.Service;
using ClinicMedical.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.ViewModel
{
    public class MaintainancViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();
        MaintainancView maintainancView;

        #region Constructor
        public MaintainancViewModel(MaintainancView maintainancViewOpen)
        {
            maintainancView = maintainancViewOpen;
        }
        #endregion
    }
}
