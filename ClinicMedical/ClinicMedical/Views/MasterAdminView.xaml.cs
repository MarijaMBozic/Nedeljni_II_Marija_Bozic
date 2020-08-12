
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
    /// Interaction logic for MasterAdminView.xaml
    /// </summary>
    public partial class MasterAdminView : Window
    {
        private bool isValidUsername;
        private bool isValidPassword;
        private bool isValidFullName;
        private bool isValidIDNumber;
        private bool isValidGender;
        private bool isValiDate;
        private bool isValidCitizenship;
        private bool isValidAdminUsername;
        private bool isValidAdminPassword;

        public MasterAdminView()
        {
            InitializeComponent();
            this.DataContext = new MasterAdminViewModel(this); 
        }

        private void IsChangeUsernamePasswordEnabeld()
        {
            if (isValidUsername &&
                isValidPassword)
            {
                btnSaveNewCredentials.IsEnabled = true;
            }
            else
            {
                btnSaveNewCredentials.IsEnabled = false;
            }
        }

        private void IsAddingNewClientUserEnabled()
        {
           if (isValidFullName &&
                isValidIDNumber &&
                isValidGender &&
                isValiDate &&
                isValidCitizenship &&
                isValidAdminUsername &&
                isValidAdminPassword)
            {
                btnSaveClinicAdmin.IsEnabled = true;
            }
            else
            {
                btnSaveClinicAdmin.IsEnabled = false;
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernameValidation(txtUser, lblValidationUsername);
            IsChangeUsernamePasswordEnabeld();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordValidation(txtPassword, lblValidationPassword);
            IsChangeUsernamePasswordEnabeld();
        }

        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGender.SelectedItem.ToString() == string.Empty)
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


        private void txtAdminUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernameValidation(txtAdminUsername, lblValidationAdminUsername);
            IsAddingNewClientUserEnabled();
        }


        private void PasswordValidation(PasswordBox txtPass, Label lblValidationPass)
        {
            if (txtPass.Focus())
            {
                lblValidationPass.Visibility = Visibility.Visible;
                lblValidationPass.FontSize = 16;
                lblValidationPass.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationPass.Content = "The password must contains at least:\n1 special characters.\n1 numbers.\n1 uppercase letter.\n1 one lowercase letter.\nMinimum length 8 characters!";
            }

            string patternPassword = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
            Match match = Regex.Match(txtPass.Password, patternPassword, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtPass.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPass.Foreground = new SolidColorBrush(Colors.Red);
                isValidPassword = false;
            }
            else
            {
                lblValidationPass.Visibility = Visibility.Hidden;
                txtPass.BorderBrush = new SolidColorBrush(Colors.Black);
                txtPass.Foreground = new SolidColorBrush(Colors.Black);
                isValidPassword = true;
            }
        }

        private void UsernameValidation(TextBox txtUser, Label lblValidationUsername)
        {
            if (txtUser.Focus())
            {
                lblValidationUsername.Visibility = Visibility.Visible;
                lblValidationUsername.FontSize = 16;
                lblValidationUsername.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationUsername.Content = "The username contains \nat least 5 characters!";
            }

            string patternUserName = @"^([a-zA-Z0-9]{5,100})$";
            Match match = Regex.Match(txtUser.Text, patternUserName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtUser.BorderBrush = new SolidColorBrush(Colors.Red);
                txtUser.Foreground = new SolidColorBrush(Colors.Red);
                isValidUsername = false;
            }
            else
            {
                lblValidationUsername.Visibility = Visibility.Hidden;
                txtUser.BorderBrush = new SolidColorBrush(Colors.Black);
                txtUser.Foreground = new SolidColorBrush(Colors.Black);
                isValidUsername = true;
            }
        }

        private void txtIDNumber_KeyUp(object sender, KeyEventArgs e)
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

        }

        private void txtAdminUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAdminUsername.Focus())
            {
                lblValidationAdminUsername.Visibility = Visibility.Visible;
                lblValidationAdminUsername.FontSize = 16;
                lblValidationAdminUsername.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationAdminUsername.Content = "The username contains \nat least 5 characters!";
            }

            string patternUserName = @"^([a-zA-Z0-9]{5,100})$";
            Match match = Regex.Match(txtAdminUsername.Text, patternUserName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtAdminUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                txtAdminUsername.Foreground = new SolidColorBrush(Colors.Red);
                isValidAdminUsername = false;
            }
            else
            {
                lblValidationAdminUsername.Visibility = Visibility.Hidden;
                txtAdminUsername.BorderBrush = new SolidColorBrush(Colors.Black);
                txtAdminUsername.Foreground = new SolidColorBrush(Colors.Black);
                isValidAdminUsername = true;
            }
            IsAddingNewClientUserEnabled();
        }

        private void txtCitizenship_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCitizenship.Focus())
            {
                lblValidationCitizenship.Visibility = Visibility.Visible;
                lblValidationCitizenship.FontSize = 16;
                lblValidationCitizenship.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationCitizenship.Content = "The citizenship must contain at least 5 caracters!";
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
        }

        private void cmbGender_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbGender.SelectedItem==null)
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
        }

        private void txtFullname_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFullname.Focus())
            {
                lblValidationFullname.Visibility = Visibility.Visible;
                lblValidationFullname.FontSize = 16;
                lblValidationFullname.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationFullname.Content = "The full name must contain at least 10 caracters!";
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
        }

        private void txtAdminPassword_KeyUp(object sender, KeyEventArgs e)
        {            
            if (txtAdminPassword.Focus())
            {
                lblValidationAdminPassword.Visibility = Visibility.Visible;
                lblValidationAdminPassword.FontSize = 16;
                lblValidationAdminPassword.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationAdminPassword.Content = "The password must contains at least:\n1 special characters.\n1 numbers.\n1 uppercase letter.\n1 one lowercase letter.\nMinimum length 8 characters!";
            }

            string patternPassword = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
            Match match = Regex.Match(txtAdminPassword.Password, patternPassword, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtAdminPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                txtAdminPassword.Foreground = new SolidColorBrush(Colors.Red);
                isValidAdminPassword = false;
            }
            else
            {
                lblValidationAdminPassword.Visibility = Visibility.Hidden;
                txtAdminPassword.BorderBrush = new SolidColorBrush(Colors.Black);
                txtAdminPassword.Foreground = new SolidColorBrush(Colors.Black);
                isValidAdminPassword = true;
            }
            IsAddingNewClientUserEnabled();
        }      
    }
}
