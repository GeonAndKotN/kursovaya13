using kursovaya13.mvvm.model;
using kursovaya13.mvvm.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace kursovaya13.mvvm.viewmodel
{
    public class DBVM : BaseVM
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
        //команды окна бд
        public VmCommand Save { get; set; }
        public VmCommand Add { get; set; }
        public VmCommand AddTeacher { get; set; }
        public VmCommand AddCabinet { get; set; }
        public VmCommand AddGroup { get; set; }
        public VmCommand AddLessons { get; set; }
        public VmCommand Remove { get; set; }
        public DBVM()
        {
            //запрос в бд для вывода данных с таблиц
            string sqll = "SELECT * FROM lessons";
            Lessons = new ObservableCollection<Lessons>(LessonsRepository.Instance.GetAllLessons(sqll));
            string sqlcr = "SELECT * FROM course";
            Course = new ObservableCollection<Course>(CourseRepository.Instance.GetAllCourse(sqlcr));
            string sqlc = "SELECT * FROM cabinet";
            Cabinet = new ObservableCollection<Cabinet>(CabinetRepository.Instance.GetAllCabinet(sqlc));
            string sqlg = "select lessonsbykiprin.ggroups.Group_Id, lessonsbykiprin.ggroups.ID_Course, lessonsbykiprin.ggroups.Title AS TitleG, lessonsbykiprin.course.id, lessonsbykiprin.course.Title AS TitleCR FROM lessonsbykiprin.ggroups, lessonsbykiprin.course WHERE ID_Course = id ORDER BY Group_Id";
            Groups = new ObservableCollection<Groups>(GroupsRepository.Instance.GetAllGroups(sqlg));
            string sqlt = "SELECT * FROM teacher";
            Teacher = new ObservableCollection<Teacher>(TeacherRepository.Instance.GetAllTeacher(sqlt));

            Save = new VmCommand(() =>
            {
                LessonsRepository.Instance.UpdateLessons(lessons);

                TeacherRepository.Instance.UpdateTeacher(teacher);

                CabinetRepository.Instance.UpdateCabinet(cabinet);

                GroupsRepository.Instance.UpdateGroups(groups);
            });
            AddTeacher = new VmCommand(() =>
            {
                if (teacher.Teacher_ID == 0)
                    TeacherRepository.Instance.AddTeacher(SelectedTeacher);
            });
            AddCabinet = new VmCommand(() =>
            {
                if (cabinet.Cabinet_ID == 0)
                    CabinetRepository.Instance.AddCabinet(cabinet);
            });
            AddGroup = new VmCommand(() =>
            {
                if (groups.Group_ID == 0)
                    GroupsRepository.Instance.AddGroups(groups);
            });
            AddLessons = new VmCommand(() =>
            {
                if (lessons.Lessons_ID == 0)
                    LessonsRepository.Instance.AddLessons(lessons);
            });
            Remove = new VmCommand(() =>
            {
                if (MessageBox.Show("Удаление! Вы уверены, что хотите удалить выбраный элемент?", "Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    LessonsRepository.Instance.Remove(SelectedLessons);
                    Lessons.Remove(SelectedLessons);

                    TeacherRepository.Instance.Remove(SelectedTeacher);
                    Teacher.Remove(SelectedTeacher);

                    CabinetRepository.Instance.Remove(SelectedCabinet);
                    Cabinet.Remove(SelectedCabinet);

                    GroupsRepository.Instance.Remove(SelectedGroups);
                    Groups.Remove(SelectedGroups);
                }
            });
        }

        public DBVM(ObservableCollection<Lessons>? lessons)
        {
            Lessons = lessons;
        }
        public ObservableCollection<Lessons>? Lessons { get; set; }

        public DBVM(ObservableCollection<Cabinet>? cabinets)
        {
            Cabinet = cabinets;
        }
        public ObservableCollection<Cabinet>? Cabinet { get; set; }

        public DBVM(ObservableCollection<Groups>? groups)
        {
            Groups = groups;
        }
        public ObservableCollection<Groups>? Groups { get; set; }

        public DBVM(ObservableCollection<Teacher>? teachers)
        {
            Teacher = teachers;
        }
        public ObservableCollection<Teacher>? Teacher { get; set; }

        public DBVM(ObservableCollection<Course>? courses)
        {
            Course = courses;
        }
        public ObservableCollection<Course>? Course { get; set; }
    }
}
