using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kursovaya13.mvvm.viewmodel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using kursovaya13.mvvm.model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace kursovaya13.mvvm.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        protected void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly EditorTimeTableVM editorTimeTableVM;
        public MainWindow()
        {
            InitializeComponent();
            editorTimeTableVM = new EditorTimeTableVM();
            DataContext = editorTimeTableVM;
            // Получаем представление данных
            ICollectionView view = CollectionViewSource.GetDefaultView(editorTimeTableVM.TimeTables);
            // Применяем фильтр к представлению
            view.Filter = Filter;
        }
        private bool Filter(object item)
        {
            TimeTable entry = item as TimeTable;
            // Проверяем, соответствует ли элемент фильтру
            bool matchesSelectedValues =
                (string.IsNullOrEmpty(CourseComboBox.SelectedItem?.ToString()) || entry.COURSE == CourseComboBox.SelectedItem?.ToString()) &&
                (string.IsNullOrEmpty(GroupsComboBox.SelectedItem?.ToString()) || entry.GROUP == GroupsComboBox.SelectedItem?.ToString()) &&
                (string.IsNullOrEmpty(LessonComboBox.SelectedItem?.ToString()) || entry.LESSONS == LessonComboBox.SelectedItem?.ToString()) &&
                (string.IsNullOrEmpty(TeacherComboBox.SelectedItem?.ToString()) || entry.TEACHER == TeacherComboBox.SelectedItem?.ToString()) &&
                (string.IsNullOrEmpty(CabinetComboBox.SelectedItem?.ToString()) || entry.CABINET == CabinetComboBox.SelectedItem?.ToString()) &&
                (string.IsNullOrEmpty(PairNumComboBox.SelectedItem?.ToString()) || entry.PAIRNUMBER == PairNumComboBox.SelectedItem?.ToString()) &&
                (string.IsNullOrEmpty(WeekdayComboBox.SelectedItem?.ToString()) || entry.WEEKDAY == WeekdayComboBox.SelectedItem?.ToString());

            // Показываем запись, если выбрано хотя бы одно значение
            if (!string.IsNullOrEmpty(CourseComboBox.SelectedItem?.ToString()) ||
                !string.IsNullOrEmpty(GroupsComboBox.SelectedItem?.ToString()) ||
                !string.IsNullOrEmpty(LessonComboBox.SelectedItem?.ToString()) ||
                !string.IsNullOrEmpty(TeacherComboBox.SelectedItem?.ToString()) ||
                !string.IsNullOrEmpty(CabinetComboBox.SelectedItem?.ToString()) ||
                !string.IsNullOrEmpty(PairNumComboBox.SelectedItem?.ToString()) ||
                !string.IsNullOrEmpty(WeekdayComboBox.SelectedItem?.ToString()))
            {
                return matchesSelectedValues;
            }
            // Показываем все записи, если ничего не выбрано
            else
            {
                return true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обновляем представление данных
            ICollectionView view = CollectionViewSource.GetDefaultView(editorTimeTableVM.TimeTables);
            view.Refresh();
            Signal();
        }

        private void ClearFiltr(object sender, RoutedEventArgs e)
        {
            if (CourseComboBox.SelectedItem != null ||
            GroupsComboBox.SelectedItem != null ||
            LessonComboBox.SelectedItem != null ||
            TeacherComboBox.SelectedItem != null ||
            CabinetComboBox.SelectedItem != null ||
            PairNumComboBox.SelectedItem != null ||
            WeekdayComboBox.SelectedItem != null)
            {
                CourseComboBox.SelectedItem = null;
                GroupsComboBox.SelectedItem = null;
                LessonComboBox.SelectedItem = null;
                TeacherComboBox.SelectedItem = null;
                CabinetComboBox.SelectedItem = null;
                PairNumComboBox.SelectedItem = null;
                WeekdayComboBox.SelectedItem = null;
            }
        }
    }
}