using ClinicMedical.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicMedical.Views
{
    /// <summary>
    /// Interaction logic for MaintainancView.xaml
    /// </summary>
    public partial class MaintainancView : Window
    {
        MaintainancViewModel maintainancViewModel;

        public MaintainancView(ClinicUser user)
        {
            InitializeComponent();
            MaintainancViewModel maintainancViewModel= new MaintainancViewModel(user, this);
            this.DataContext = maintainancViewModel;
            this.maintainancViewModel = maintainancViewModel;
        }

        private void btnDeleteMaintenance_Click(object sender, RoutedEventArgs e)
        {
            maintainancViewModel.DeleteMaintenanceExecute();
        }

        private void btnEditMaintenance_Click(object sender, RoutedEventArgs e)
        {
            maintainancViewModel.AddEditMaintenance();
        }
    }
}
