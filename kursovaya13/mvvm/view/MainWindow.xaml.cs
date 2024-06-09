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


namespace kursovaya13.mvvm.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EditorTimeTableVM editorTimeTableVM;
        public SearchTimeTable searchTimeTable = new SearchTimeTable();
        public MainWindow()
        {
            InitializeComponent();
            editorTimeTableVM = new EditorTimeTableVM();
            DataContext = editorTimeTableVM;
            FilteredTimetableEntries = editorTimeTableVM.TimeTables;
        }
        public ObservableCollection<TimeTable> FilteredTimetableEntries { get; set; }
        public void SearchTimetable(string selectedCourse, string selectedGroups, string selectedLesson, string selectedTeacher, string selectedCabinet, string selectedPairNum, string selectedWeekday)
        {
            var filteredEntries = editorTimeTableVM.TimeTables.Where(entry =>
                (string.IsNullOrEmpty(selectedCourse) || entry.COURSE == selectedCourse) &&
                (string.IsNullOrEmpty(selectedGroups) || entry.GROUP == selectedGroups) &&
                (string.IsNullOrEmpty(selectedLesson) || entry.LESSONS == selectedLesson) &&
                (string.IsNullOrEmpty(selectedTeacher) || entry.TEACHER == selectedTeacher) &&
                (string.IsNullOrEmpty(selectedCabinet) || entry.CABINET == selectedCabinet) &&
                (string.IsNullOrEmpty(selectedPairNum) || entry.PAIRNUMBER == selectedPairNum) &&
                (string.IsNullOrEmpty(selectedWeekday) || entry.WEEKDAY == selectedWeekday)).ToList();

            FilteredTimetableEntries.Clear();
            foreach (var entry in filteredEntries)
            {
                FilteredTimetableEntries.Add(entry);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTimetable(
                CourseComboBox.SelectedItem?.ToString(),
                GroupsComboBox.SelectedItem?.ToString(),
                LessonComboBox.SelectedItem?.ToString(),
                TeacherComboBox.SelectedItem?.ToString(),
                CabinetComboBox.SelectedItem?.ToString(),
                PairNumComboBox.SelectedItem?.ToString(),
                WeekdayComboBox.SelectedItem?.ToString());
        }
    }
}