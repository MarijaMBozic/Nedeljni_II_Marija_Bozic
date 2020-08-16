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
    /// Interaction logic for InstitutionView.xaml
    /// </summary>
    public partial class InstitutionView : Window
    {
        InstitutionViewModel institutionViewModel;
        public InstitutionView(ClinicUser user)
        {
            InitializeComponent();
            InstitutionViewModel institutionViewModel = new InstitutionViewModel(user, this);
            this.DataContext = institutionViewModel;
            this.institutionViewModel = institutionViewModel;
        }

        private void btnEditInstitution_Click(object sender, RoutedEventArgs e)
        {
            institutionViewModel.EditInstitution();
        }
    }
}

