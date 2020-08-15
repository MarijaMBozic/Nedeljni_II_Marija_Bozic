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
    /// Interaction logic for AddMaintainanceView.xaml
    /// </summary>
    public partial class AddMaintainanceView : Window
    {
        public AddMaintainanceView(ClinicUser adminUser, ClinicUser user, ClinicMaintenance maintenance)
        {
            InitializeComponent();
            this.DataContext = new AddMaintainancViewModel(adminUser, user, maintenance, this);
        }
    }
}
