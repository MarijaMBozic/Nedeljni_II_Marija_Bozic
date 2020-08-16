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
    /// Interaction logic for AddDoctorView.xaml
    /// </summary>
    public partial class AddDoctorView : Window
    {
        private bool isValidFullName;
        private bool isValidIDNumber;
        private bool isValidGender;
        private bool isValiDate;
        private bool isValidCitizenship;
        private bool isValidUsername;
        private bool isValidPassword;
        private bool isValidUniqueNumber;
        private bool isValidBancAccount;
        private bool isValidDepartment;
        private bool isValidWorkShift;
        private bool isValidManager;

        public AddDoctorView(ClinicUser adminUser, ClinicUser user, ClinicDoctor doctor, bool isForEdit)
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModel(adminUser, user, doctor, this, isForEdit);
        }
        private void IsAddingNewClientUserEnabled()
        {
            if (isValidFullName &&
            isValidIDNumber &&
            isValidGender &&
            isValiDate &&
            isValidCitizenship &&
            isValidUsername &&
            isValidPassword &&
            isValidUniqueNumber &&
            isValidBancAccount &&
            isValidDepartment &&
            isValidWorkShift &&
            isValidManager)
            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }

        private void IsEditingUserEnabled()
        {
            if (isValidFullName &&
            isValidIDNumber &&
            isValidGender &&
            isValiDate &&
            isValidCitizenship &&
            isValidUsername &&
            isValidUniqueNumber &&
            isValidBancAccount &&
            isValidDepartment &&
            isValidWorkShift &&
            isValidManager)
            {
                btnEdit.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
            }
        }

        private void txtFullname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFullname.Focus())
            {
                lblValidationFullname.Visibility = Visibility.Visible;
                lblValidationFullname.FontSize = 16;
                lblValidationFullname.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationFullname.Content = "The full name must contain \nat least 10 caracters!";
            }

            string patternFullName = @"^([a-zA-Z ]{10,100})$";
            Match match = Regex.Match(txtFullname.Text, patternFullName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtFullname.BorderBrush = new SolidColorBrush(Colors.Red);
                txtFullname.Foreground = new SolidColorBrush(Colors.Red);
                isValidFullName = false;
            }
            else
            {
                lblValidationFullname.Visibility = Visibility.Hidden;
                txtFullname.BorderBrush = new SolidColorBrush(Colors.Black);
                txtFullname.Foreground = new SolidColorBrush(Colors.Black);
                isValidFullName = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void txtUniqueNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUniqueNumber.Focus())
            {
                lblValidationUniqueNumber.Visibility = Visibility.Visible;
                lblValidationUniqueNumber.FontSize = 16;
                lblValidationUniqueNumber.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationUniqueNumber.Content = "The Unique Number must \ncontains exact 7 digits!";
            }

            string patternIDNumber = @"^([0-9]{7})$";
            Match match = Regex.Match(txtUniqueNumber.Text, patternIDNumber, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtUniqueNumber.BorderBrush = new SolidColorBrush(Colors.Red);
                txtUniqueNumber.Foreground = new SolidColorBrush(Colors.Red);
                isValidUniqueNumber = false;
            }
            else
            {
                lblValidationUniqueNumber.Visibility = Visibility.Hidden;
                txtUniqueNumber.BorderBrush = new SolidColorBrush(Colors.Black);
                txtUniqueNumber.Foreground = new SolidColorBrush(Colors.Black);
                isValidUniqueNumber = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void txtIDNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtIDNumber.Focus())
            {
                lblValidationIDNumber.Visibility = Visibility.Visible;
                lblValidationIDNumber.FontSize = 16;
                lblValidationIDNumber.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationIDNumber.Content = "The number ID must contains exact 7 digits!";
            }

            string patternIDNumber = @"^([0-9]{7})$";
            Match match = Regex.Match(txtIDNumber.Text, patternIDNumber, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtIDNumber.BorderBrush = new SolidColorBrush(Colors.Red);
                txtIDNumber.Foreground = new SolidColorBrush(Colors.Red);
                isValidIDNumber = false;
            }
            else
            {
                lblValidationIDNumber.Visibility = Visibility.Hidden;
                txtIDNumber.BorderBrush = new SolidColorBrush(Colors.Black);
                txtIDNumber.Foreground = new SolidColorBrush(Colors.Black);
                isValidIDNumber = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void txtBancAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBancAccount.Focus())
            {
                lblValidationBankAccount.Visibility = Visibility.Visible;
                lblValidationBankAccount.FontSize = 16;
                lblValidationBankAccount.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationBankAccount.Content = "The banc account must contains \nexact 10 digits!";
            }

            string patternIDNumber = @"^([0-9]{10})$";
            Match match = Regex.Match(txtBancAccount.Text, patternIDNumber, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtBancAccount.BorderBrush = new SolidColorBrush(Colors.Red);
                txtBancAccount.Foreground = new SolidColorBrush(Colors.Red);
                isValidBancAccount = false;
            }
            else
            {
                lblValidationBankAccount.Visibility = Visibility.Hidden;
                txtBancAccount.BorderBrush = new SolidColorBrush(Colors.Black);
                txtBancAccount.Foreground = new SolidColorBrush(Colors.Black);
                isValidBancAccount = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void dpDateOfBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            int choosenDate = 0;
            if (dpDateOfBirth.SelectedDate != null)
            {
                choosenDate = dpDateOfBirth.SelectedDate.Value.Year;
            }

            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                dpDateOfBirth.BorderBrush = new SolidColorBrush(Colors.Red);
                dpDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                isValiDate = false;
            }
            else if ((currentYear - choosenDate) < 18)
            {
                dpDateOfBirth.BorderBrush = new SolidColorBrush(Colors.Red);
                dpDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationDateOfBirth.Content = "The person must be of legal age!";
                isValiDate = false;
            }
            else if ((currentYear - choosenDate) > 80)
            {
                dpDateOfBirth.BorderBrush = new SolidColorBrush(Colors.Red);
                dpDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationDateOfBirth.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationDateOfBirth.Content = "Invalid year!";
                isValiDate = false;
            }
            else
            {
                lblValidationDateOfBirth.Visibility = Visibility.Hidden;
                dpDateOfBirth.BorderBrush = new SolidColorBrush(Colors.Black);
                dpDateOfBirth.Foreground = new SolidColorBrush(Colors.Black);
                isValiDate = true;
            }
            IsAddingNewClientUserEnabled();
        }

        private void txtCitizenship_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCitizenship.Focus())
            {
                lblValidationCitizenship.Visibility = Visibility.Visible;
                lblValidationCitizenship.FontSize = 16;
                lblValidationCitizenship.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationCitizenship.Content = "The citizenship must contain \nat least 5 caracters!";
            }

            string patternCitizenship = @"^([a-zA-Z ]{5,100})$";
            Match match = Regex.Match(txtCitizenship.Text, patternCitizenship, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtCitizenship.BorderBrush = new SolidColorBrush(Colors.Red);
                txtCitizenship.Foreground = new SolidColorBrush(Colors.Red);
                isValidCitizenship = false;
            }
            else
            {
                lblValidationCitizenship.Visibility = Visibility.Hidden;
                txtCitizenship.BorderBrush = new SolidColorBrush(Colors.Black);
                txtCitizenship.Foreground = new SolidColorBrush(Colors.Black);
                isValidCitizenship = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUsername.Focus())
            {
                lblValidationUsername.Visibility = Visibility.Visible;
                lblValidationUsername.FontSize = 16;
                lblValidationUsername.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationUsername.Content = "The username contains \nat least 5 characters!";
            }

            string patternUserName = @"^([a-zA-Z0-9]{5,100})$";
            Match match = Regex.Match(txtUsername.Text, patternUserName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                txtUsername.Foreground = new SolidColorBrush(Colors.Red);
                isValidUsername = false;
            }
            else
            {
                lblValidationUsername.Visibility = Visibility.Hidden;
                txtUsername.BorderBrush = new SolidColorBrush(Colors.Black);
                txtUsername.Foreground = new SolidColorBrush(Colors.Black);
                isValidUsername = true;
            }
            IsAddingNewClientUserEnabled();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Focus())
            {
                lblValidationAdminPassword.Visibility = Visibility.Visible;
                lblValidationAdminPassword.FontSize = 16;
                lblValidationAdminPassword.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationAdminPassword.Content = "The password must contains at least:\n1 special characters.\n1 numbers.\n1 uppercase letter.\n1 one lowercase letter.\nMinimum length 8 characters!";
            }

            string patternPassword = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
            Match match = Regex.Match(txtPassword.Password, patternPassword, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPassword.Foreground = new SolidColorBrush(Colors.Red);
                isValidPassword = false;
            }
            else
            {
                lblValidationAdminPassword.Visibility = Visibility.Hidden;
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Black);
                txtPassword.Foreground = new SolidColorBrush(Colors.Black);
                isValidPassword = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGender.SelectedItem == null)
            {
                cmbGender.BorderBrush = new SolidColorBrush(Colors.Red);
                cmbGender.Foreground = new SolidColorBrush(Colors.Red);
                isValidGender = false;
            }
            else
            {
                cmbGender.BorderBrush = new SolidColorBrush(Colors.Black);
                cmbGender.Foreground = new SolidColorBrush(Colors.Black);
                isValidGender = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDepartment.SelectedItem == null)
            {
                cmbDepartment.BorderBrush = new SolidColorBrush(Colors.Red);
                cmbDepartment.Foreground = new SolidColorBrush(Colors.Red);
                isValidDepartment = false;
            }
            else
            {
                cmbDepartment.BorderBrush = new SolidColorBrush(Colors.Black);
                cmbDepartment.Foreground = new SolidColorBrush(Colors.Black);
                isValidDepartment = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void cmbWorkShift_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbWorkShift.SelectedItem == null)
            {
                cmbWorkShift.BorderBrush = new SolidColorBrush(Colors.Red);
                cmbWorkShift.Foreground = new SolidColorBrush(Colors.Red);
                isValidWorkShift = false;
            }
            else
            {
                cmbWorkShift.BorderBrush = new SolidColorBrush(Colors.Black);
                cmbWorkShift.Foreground = new SolidColorBrush(Colors.Black);
                isValidWorkShift = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }

        private void cmbManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbManager.SelectedItem == null)
            {
                cmbManager.BorderBrush = new SolidColorBrush(Colors.Red);
                cmbManager.Foreground = new SolidColorBrush(Colors.Red);
                isValidManager = false;
            }
            else
            {
                cmbManager.BorderBrush = new SolidColorBrush(Colors.Black);
                cmbManager.Foreground = new SolidColorBrush(Colors.Black);
                isValidManager = true;
            }
            IsAddingNewClientUserEnabled();
            IsEditingUserEnabled();
        }
    }
}



     

        

        

        

