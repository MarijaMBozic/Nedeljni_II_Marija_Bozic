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
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        DoctorViewModel doctorViewModel;
        public DoctorView(ClinicUser user)
        {
            InitializeComponent();

            DoctorViewModel doctorViewModel = new DoctorViewModel(user, this);
            this.DataContext = doctorViewModel;
            this.doctorViewModel = doctorViewModel;
        }

        private void btnEditDoctor_Click(object sender, RoutedEventArgs e)
        {
            doctorViewModel.EditDoctor();
        }

        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            doctorViewModel.DeleteDoctorExecute();
        }
    }
}
