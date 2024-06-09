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

namespace kursovaya13.mvvm.view
{
    /// <summary>
    /// Логика взаимодействия для BDWindow.xaml
    /// </summary>
    public partial class BDWindow : Window
    {
        public BDWindow()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Весь несохранённый прогресс будет потерян!", "Выход!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
