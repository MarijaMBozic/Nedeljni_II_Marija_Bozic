using ClinicMedical.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddInstitutionView.xaml
    /// </summary>
    public partial class AddInstitutionView : Window
    {
        public bool isValidDate;
        public AddInstitutionView(ClinicUser user, Institution institution, bool isForEdit)
        {
            InitializeComponent();
            this.DataContext = new AddInstitutionViewModel(user, institution, this, isForEdit);
        }

        private void IsSaveEnabled()
        {
            if (isValidDate)

            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }

        private void dpBuildDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (!dpBuildDate.SelectedDate.HasValue)
            {
                dpBuildDate.BorderBrush = new SolidColorBrush(Colors.Red);
                dpBuildDate.Foreground = new SolidColorBrush(Colors.Red);
                isValidDate = false;
            }
            else if (dpBuildDate.SelectedDate > DateTime.Now)
            {
                dpBuildDate.BorderBrush = new SolidColorBrush(Colors.Red);
                dpBuildDate.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationBuildDate.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationBuildDate.Content = "Build date must be \nbefore current date!";
                isValidDate = false;
            }
            else
            {
                lblValidationBuildDate.Visibility = Visibility.Hidden;
                dpBuildDate.BorderBrush = new SolidColorBrush(Colors.Black);
                dpBuildDate.Foreground = new SolidColorBrush(Colors.Black);
                isValidDate = true;
            }
            IsSaveEnabled();
        }
    }
}
