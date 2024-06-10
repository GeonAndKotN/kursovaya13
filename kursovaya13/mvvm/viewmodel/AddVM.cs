using kursovaya13.mvvm.model;
using kursovaya13.mvvm.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace kursovaya13.mvvm.viewmodel
{
    public class AddVM : BaseVM
    {
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

        public VmCommand Add {  get; set; }

        public AddVM()
        {
            string sqlcr = "SELECT * FROM course";
            Course = new ObservableCollection<Course>(CourseRepository.Instance.GetAllCourse(sqlcr));
            string sqll = "SELECT * FROM lessons";
            Lessons = new ObservableCollection<Lessons>(LessonsRepository.Instance.GetAllLessons(sqll));
            string sqlc = "SELECT * FROM cabinet WHERE Available != 'нет'";
            Cabinet = new ObservableCollection<Cabinet>(CabinetRepository.Instance.GetAllCabinet(sqlc));
            string sqlg = "select lessonsbykiprin.ggroups.Group_Id, lessonsbykiprin.ggroups.ID_Course, lessonsbykiprin.ggroups.Title AS TitleG, lessonsbykiprin.course.id, lessonsbykiprin.course.Title AS TitleCR FROM lessonsbykiprin.ggroups, lessonsbykiprin.course WHERE ID_Course = id ORDER BY Group_Id";
            Groups = new ObservableCollection<Groups>(GroupsRepository.Instance.GetAllGroups(sqlg));
            string sqlt = "SELECT * FROM teacher WHERE Absent != 'нет'";
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

            Add = new VmCommand(() =>
            {
                if (MessageBox.Show("Вы уверены, что хотите внести эти данные?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (SelectedGroups == null || SelectedLessons == null || SelectedCabinet == null ||
                SelectedPairNumber == null || SelectedWeekDay == null || SelectedTeacher == null)
                    {
                        MessageBox.Show("Не все данные выбраны", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        timeTable.ID_GROUP = SelectedGroups.Group_ID;
                        timeTable.ID_Week_Day = SelectedWeekDay.id;
                        timeTable.ID_LESSONS = SelectedLessons.Lessons_ID;
                        timeTable.ID_TEACHER = SelectedTeacher.Teacher_ID;
                        timeTable.ID_CABINET = SelectedCabinet.Cabinet_ID;
                        timeTable.ID_Pair_Number = SelectedPairNumber.id;
                        timeTable.ID_COURSE = SelectedGroups.ID_Course;

                        if (timeTable.id == 0)
                            TimeTableRepository.Instance.AddTimeTable(timeTable);
                        TimeTableRepository.Instance.GetAllTimeTable(sql);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Window MainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                        MainWindow?.Close();
                    }
                }
            });
        }

        public AddVM(ObservableCollection<TimeTable>? timeTables)
        {
            TimeTables = timeTables;
        }
        public ObservableCollection<TimeTable>? TimeTables { get; }
        public AddVM(ObservableCollection<Lessons>? lessons)
        {
            Lessons = lessons;
        }
        public ObservableCollection<Lessons>? Lessons { get; set; }

        public AddVM(ObservableCollection<Cabinet>? cabinets)
        {
            Cabinet = cabinets;
        }
        public ObservableCollection<Cabinet>? Cabinet { get; set; }

        public AddVM(ObservableCollection<Groups>? groups)
        {
            Groups = groups;
        }
        public ObservableCollection<Groups>? Groups { get; set; }

        public AddVM(ObservableCollection<Teacher>? teachers)
        {
                Teacher = teachers;
        }
        public ObservableCollection<Teacher>? Teacher { get; set; }

        public AddVM(ObservableCollection<PairNumber>? pairNumbers)
        {
            PairNumber = pairNumbers;
        }
        public ObservableCollection<PairNumber>? PairNumber { get; set; }

        public AddVM(ObservableCollection<WeekDay>? weekDays)
        {
            WeekDay = weekDays;
        }
        public ObservableCollection<WeekDay>? WeekDay { get; set; }

        public AddVM(ObservableCollection<Course>? courses)
        {
            Course = courses;
        }
        public ObservableCollection<Course>? Course { get; set; }
    }
}
