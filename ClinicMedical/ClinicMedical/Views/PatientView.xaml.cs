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
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    { 
        PatientViewModel patientViewModel;
        public PatientView(ClinicUser user)
        {
            InitializeComponent();
            PatientViewModel patientViewModel = new PatientViewModel(user, this);
            this.DataContext = patientViewModel;
            this.patientViewModel = patientViewModel;
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            patientViewModel.DeleteManagerExecute();
        }

        private void btnEditPatient_Click(object sender, RoutedEventArgs e)
        {
            patientViewModel.EditPatient();
        }
    }
}

