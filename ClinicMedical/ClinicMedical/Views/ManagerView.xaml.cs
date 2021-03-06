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
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        ManagerViewModel managerViewModel;
        public ManagerView(ClinicUser user)
        {
            InitializeComponent();
            ManagerViewModel managerViewModel = new ManagerViewModel(user, this);
            this.DataContext = managerViewModel;
            this.managerViewModel = managerViewModel;
        }
        private void btnDeleteManager_Click(object sender, RoutedEventArgs e)
        {
            managerViewModel.DeleteManagerExecute();
        }

        private void btnEditManager_Click(object sender, RoutedEventArgs e)
        {
            managerViewModel.EditManager();
        }
    }
}
