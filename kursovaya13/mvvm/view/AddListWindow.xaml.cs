using kursovaya13.mvvm.model;
using kursovaya13.mvvm.viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace kursovaya13.mvvm.view
{
    /// <summary>
    /// Логика взаимодействия для AddListWindow.xaml
    /// </summary>
    public partial class AddListWindow : Window
    {
        private readonly AddVM viewModel;
        public AddListWindow()
        {
            InitializeComponent();
            viewModel = new AddVM();
            DataContext = viewModel;
        }
    }
}
