using kursovaya13.mvvm.model;
using kursovaya13.mvvm.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace kursovaya13.mvvm.viewmodel
{
    public class EditorTimeTableVM : BaseVM
    {
        private SearchTimeTable searchTimeTable = new();
        public SearchTimeTable SelectedSearchTimeTable
        {
            get => searchTimeTable;
            set
            {
                searchTimeTable = value;
                Signal();
            }
        }

        private TimeTable timeTable = new();
        public TimeTable SelectedTimeTable
        {
            get => timeTable;
            set
            {
                timeTable = value;
                Signal();
            }
        }

        private Lessons lessons = new();
        public Lessons SelectedLessons
        {
            get => lessons;
            set
            {
                lessons = value;
                Signal();
            }
        }
        private Cabinet cabinet = new();
        public Cabinet SelectedCabinet
        {
            get => cabinet;
            set
            {
                cabinet = value;
                Signal();
            }
        }
        private Groups groups = new();
        public Groups SelectedGroups
        {
            get => groups;
            set
            {
                groups = value;
                Signal();
            }
        }
        private Teacher teacher = new();
        public Teacher SelectedTeacher
        {
            get => teacher;
            set
            {
                    teacher = value;
                    Signal();
            }
        }
        private PairNumber pairNumber = new();
        public PairNumber SelectedPairNumber
        {
            get => pairNumber;
            set
            {
                pairNumber = value;
                Signal();
            }
        }
        private WeekDay weekDay = new();
        public WeekDay SelectedWeekDay
        {
            get => weekDay;
            set
            {
                weekDay = value;
                Signal();
            }
        }
        private Course course = new();
        public Course SelectedCourse
        {
            get => course;
            set
            {
                course = value;
                Signal();
            }
        }

        //команды окна редактирования расписания
        public VmCommand Add { get; set; }
        public VmCommand Remove { get; set; }
        public VmCommand DeleteList { get; set; }
        public VmCommand OpenBD { get; set; }
        public EditorTimeTableVM()
        {
            //запрос в бд для вывода данных с таблицы
            string sqlcr = "SELECT * FROM course";
            Course = new ObservableCollection<Course>(CourseRepository.Instance.GetAllCourse(sqlcr));
            string sqll = "SELECT * FROM lessons";
            Lessons = new ObservableCollection<Lessons>(LessonsRepository.Instance.GetAllLessons(sqll));
            string sqlc = "SELECT * FROM cabinet";
            Cabinet = new ObservableCollection<Cabinet>(CabinetRepository.Instance.GetAllCabinet(sqlc));
            string sqlg = "select lessonsbykiprin.ggroups.Group_Id, lessonsbykiprin.ggroups.ID_Course, lessonsbykiprin.ggroups.Title AS TitleG, lessonsbykiprin.course.id, lessonsbykiprin.course.Title AS TitleCR FROM lessonsbykiprin.ggroups, lessonsbykiprin.course WHERE ID_Course = id ORDER BY Group_Id";
            Groups = new ObservableCollection<Groups>(GroupsRepository.Instance.GetAllGroups(sqlg));
            string sqlt = "SELECT * FROM teacher";
            Teacher = new ObservableCollection<Teacher>(TeacherRepository.Instance.GetAllTeacher(sqlt));
            string sqlp = "SELECT * FROM pairnumber";
            PairNumber = new ObservableCollection<PairNumber>(PairNumberRepository.Instance.GetAllPair(sqlp));
            string sqlw = "SELECT * FROM weekday";
            WeekDay = new ObservableCollection<WeekDay>(WeekDayRepository.Instance.GetAllWeekDay(sqlw));
            string sql = "SELECT timetable.id, timetable.ID_GROUP, timetable.ID_CABINET, timetable.ID_LESSONS, timetable.ID_Pair_Number, timetable.ID_Week_Day, timetable.ID_TEACHER, " +
                "ggroups.Group_Id, ggroups.Title AS TitleG, ggroups.ID_Course, " +
                "lessons.Lessons_Id, lessons.Title AS TitleL, lessons.Teacher_IDL, " +
                "cabinet.Cabinet_ID, cabinet.Title AS TitleCab, cabinet.Available, cabinet.Appointment, " +
                "teacher.Teacher_Id, teacher.Title AS TitleT, teacher.Absent, " +
                "pairnumber.id AS idPR, pairnumber.Title AS TitlePR, " +
                "weekday.id AS idWD, weekday.Title AS TitleWD, " +
                "course.id AS idCourse, course.Title AS TitleCourse " +
                "FROM timetable, ggroups, lessons, cabinet, teacher, pairnumber, weekday, course";
            TimeTables = new ObservableCollection<TimeTable>(TimeTableRepository.Instance.GetAllTimeTable(sql));
            SearchTimeTable = new ObservableCollection<SearchTimeTable>(SearchTimeTableRepository.Instance.GetAllTimeTable(sql));
            Add = new VmCommand(() =>
            {
                AddListWindow addListWindow = new AddListWindow();
                addListWindow.ShowDialog();
            });
            Remove = new VmCommand(() =>
            {
                if (SelectedTimeTable == null)
                    return;
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?", "Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    TimeTableRepository.Instance.Remove(SelectedTimeTable);
                    TimeTables.Remove(SelectedTimeTable);
                }
            });
            DeleteList = new VmCommand(() =>
            {
                if (SelectedTimeTable == null)
                    return;
                if (MessageBox.Show("Удалить расписание?", "Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    TimeTableRepository.Instance.RemoveAll(SelectedTimeTable);
                    TimeTables.Remove(SelectedTimeTable);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Window MainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    MainWindow?.Close();
                }
            });
            OpenBD = new VmCommand(() =>
            {
                BDWindow bdWindow = new BDWindow();
                bdWindow.ShowDialog();
            });
        }

        public EditorTimeTableVM(ObservableCollection<SearchTimeTable>? searchTimeTable)
        {
            SearchTimeTable = searchTimeTable;
        }
        public ObservableCollection<SearchTimeTable>? SearchTimeTable { get; set; }

        public EditorTimeTableVM(ObservableCollection<TimeTable>? timeTables)
        {
            TimeTables = timeTables;
        }
        public ObservableCollection<TimeTable>? TimeTables { get; set; }

        public EditorTimeTableVM(ObservableCollection<Lessons>? lessons)
        {
            Lessons = lessons;
        }
        public ObservableCollection<Lessons>? Lessons { get; set; }

        public EditorTimeTableVM(ObservableCollection<Cabinet>? cabinets)
        {
            Cabinet = cabinets;
        }
        public ObservableCollection<Cabinet>? Cabinet { get; set; }

        public EditorTimeTableVM(ObservableCollection<Groups>? groups)
        {
            Groups = groups;
        }
        public ObservableCollection<Groups>? Groups { get; set; }

        public EditorTimeTableVM(ObservableCollection<Teacher>? teachers)
        {
            Teacher = teachers;
        }
        public ObservableCollection<Teacher>? Teacher { get; set; }

        public EditorTimeTableVM(ObservableCollection<PairNumber>? pairNumbers)
        {
            PairNumber = pairNumbers;
        }
        public ObservableCollection<PairNumber>? PairNumber { get; set; }

        public EditorTimeTableVM(ObservableCollection<WeekDay>? weekDays)
        {
            WeekDay = weekDays;
        }
        public ObservableCollection<WeekDay>? WeekDay { get; set; }

        public EditorTimeTableVM(ObservableCollection<Course>? courses)
        {
            Course = courses;
        }
        public ObservableCollection<Course>? Course { get; set; }

    }
}