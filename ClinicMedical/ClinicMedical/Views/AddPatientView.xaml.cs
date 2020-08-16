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
    /// Interaction logic for AddPatientView.xaml
    /// </summary>
    public partial class AddPatientView : Window
    {
        public bool isValidDate;
        public bool isValidDateInsurance;
        public AddPatientView(ClinicUser userAdmin, ClinicUser user, ClinicPatient patient, bool isForEdit)
        {
            InitializeComponent();
            this.DataContext =new AddPatientViewModel(userAdmin, user, patient, this, isForEdit);
        }

        private void IsSaveEnabled()
        {
            if (isValidDate && isValidDateInsurance)

            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }

        private void IsEditEnabled()
        {
            if (isValidDate && isValidDateInsurance)

            {
                btnEdit.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
            }
        }

        private void dpDateOfBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                lblValidationDateOfBirth.Visibility = Visibility.Visible;
                dpDateOfBirth.BorderBrush = new SolidColorBrush(Colors.Red);
                dpDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                isValidDate = false;
            }
            else if (dpDateOfBirth.SelectedDate > DateTime.Now)
            {                
                dpDateOfBirth.BorderBrush = new SolidColorBrush(Colors.Red);
                dpDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationDateOfBirth.Visibility = Visibility.Visible;
                lblValidationDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationDateOfBirth.Content = "Date of birth must be \nbefore current date!";
                isValidDate = false;
            }
            else
            {
                lblValidationDateOfBirth.Visibility = Visibility.Hidden;
                dpDateOfBirth.BorderBrush = new SolidColorBrush(Colors.Black);
                dpDateOfBirth.Foreground = new SolidColorBrush(Colors.Black);
                isValidDate = true;
            }
            IsSaveEnabled();
            IsEditEnabled();
        }

        private void dpInsuranceExpirationDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (!dpInsuranceExpirationDate.SelectedDate.HasValue)
            {
                lblValidationInsuranceExpirationDate.Visibility = Visibility.Visible;
                dpInsuranceExpirationDate.BorderBrush = new SolidColorBrush(Colors.Red);
                dpInsuranceExpirationDate.Foreground = new SolidColorBrush(Colors.Red);
                isValidDateInsurance = false;
            }
            else if (dpInsuranceExpirationDate.SelectedDate < DateTime.Now)
            {
                dpInsuranceExpirationDate.BorderBrush = new SolidColorBrush(Colors.Red);
                dpInsuranceExpirationDate.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationInsuranceExpirationDate.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationInsuranceExpirationDate.Visibility = Visibility.Visible;
                lblValidationInsuranceExpirationDate.Content = "The expiration date must be \nafter the current date!";
                isValidDateInsurance = false;
            }
            else
            {
                lblValidationInsuranceExpirationDate.Visibility = Visibility.Hidden;
                dpInsuranceExpirationDate.BorderBrush = new SolidColorBrush(Colors.Black);
                dpInsuranceExpirationDate.Foreground = new SolidColorBrush(Colors.Black);
                isValidDateInsurance = true;
            }
            IsSaveEnabled();
            IsEditEnabled();
        }
    }
}
