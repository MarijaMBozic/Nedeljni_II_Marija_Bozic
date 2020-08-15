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
    /// Interaction logic for AddManagerView.xaml
    /// </summary>
    public partial class AddManagerView : Window
    {
        public AddManagerView(ClinicUser adminUser, ClinicUser user, ClinicManager manager)
        {
            InitializeComponent();
            this.DataContext = new AddManagerViewModel(adminUser, user, manager, this);
        }
    }
}
