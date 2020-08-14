﻿using ClinicMedical.ViewModel;
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
    /// Interaction logic for AddInstitutionView.xaml
    /// </summary>
    public partial class AddInstitutionView : Window
    {
        public AddInstitutionView(ClinicUser user)
        {
            InitializeComponent();
            this.DataContext = new AddInstitutionViewModel(user, this);
        }
    }
}
