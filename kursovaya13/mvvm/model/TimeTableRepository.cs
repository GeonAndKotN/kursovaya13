using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class TimeTableRepository
    {
        static TimeTableRepository instance;
        public static TimeTableRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new TimeTableRepository();
                return instance;
            }
        }
        //получение данных с таблицы в дб
        internal IEnumerable<TimeTable> GetAllTimeTable(string sql)
        {
            var result = new List<TimeTable>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                TimeTable timetable = new TimeTable();
                Groups groups = new Groups();
                Lessons lessons = new Lessons();
                Cabinet cabinet = new Cabinet();
                Teacher teacher = new Teacher();
                PairNumber pairnumber = new PairNumber();
                WeekDay weekday = new WeekDay();
                Course course = new Course();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("id");
                    if (timetable.id == id || timetable.id != id)
                    {
                        timetable = new TimeTable();
                        //timetable.id = id;
                        timetable.ID_GROUP = reader.GetInt32("ID_GROUP");
                        timetable.ID_CABINET = reader.GetInt32("ID_CABINET");
                        timetable.ID_LESSONS = reader.GetInt32("ID_LESSONS");
                        timetable.ID_Pair_Number = reader.GetInt32("ID_Pair_Number");
                        timetable.ID_Week_Day = reader.GetInt32("ID_Week_Day");
                        timetable.ID_TEACHER = reader.GetInt32("ID_TEACHER");
                        id = reader.GetInt32("Group_Id");
                        if (id == timetable.ID_GROUP)
                        {
                            groups = new Groups();
                            groups.Group_ID = id;
                            groups.GroupTitle = reader.GetString("TitleG");
                            groups.ID_Course = reader.GetInt32("ID_Course");
                            timetable.GROUP = groups.GroupTitle;
                            timetable.ID_COURSE = groups.ID_Course;
                            id = reader.GetInt32("Lessons_Id");
                            if (id == timetable.ID_LESSONS)
                            {
                                lessons = new Lessons();
                                lessons.Lessons_ID = id;
                                lessons.LessonsTitle = reader.GetString("TitleL");
                                lessons.Teacher_IDL = reader.GetInt32("Teacher_IDL");
                                timetable.LESSONS = lessons.LessonsTitle;
                                id = reader.GetInt32("Cabinet_ID");
                                if (id == timetable.ID_CABINET)
                                {
                                    cabinet = new Cabinet();
                                    cabinet.Cabinet_ID = id;
                                    cabinet.CabinetTitle = reader.GetString("TitleCab");
                                    cabinet.Available = reader.GetString("Available");
                                    cabinet.Appointment = reader.GetString("Appointment");
                                    timetable.CABINET = cabinet.CabinetTitle;
                                    id = reader.GetInt32("Teacher_Id");
                                    if (id == timetable.ID_TEACHER)
                                    {
                                        teacher = new Teacher();
                                        teacher.Teacher_ID = id;
                                        teacher.TeacherTitle = reader.GetString("TitleT");
                                        teacher.Absent = reader.GetString("Absent");

                                        timetable.TEACHER = teacher.TeacherTitle;
                                        id = reader.GetInt32("idPR");
                                        if (id == timetable.ID_Pair_Number)
                                        {
                                            pairnumber = new PairNumber();
                                            pairnumber.id = id;
                                            pairnumber.Title = reader.GetString("TitlePR");
                                            timetable.PAIRNUMBER = pairnumber.Title;
                                            id = reader.GetInt32("idWD");
                                            if (id == timetable.ID_Week_Day)
                                            {
                                                weekday = new WeekDay();
                                                weekday.id = id;
                                                weekday.Title = reader.GetString("TitleWD");
                                                timetable.WEEKDAY = weekday.Title;
                                                id = reader.GetInt32("idCourse");
                                                if (id == timetable.ID_COURSE)
                                                {
                                                    course = new Course();
                                                    course.id = id;
                                                    course.Title = reader.GetString("TitleCourse");
                                                    timetable.COURSE = course.Title;
                                                    result.Add(timetable);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        timetable.id = id;
                }
            }
            
            return result;
        }
        //добавления новых данных в таблицу бд
        internal void AddTimeTable(TimeTable timetable)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;
            int id = MySqlDB.Instance.GetAutoID("id");
            string sql = "INSERT INTO timetable VALUES (0, @ID_GROUP, @ID_CABINET, @ID_LESSONS, @ID_Pair_Number, @ID_Week_Day, @ID_TEACHER)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("ID_GROUP", timetable.ID_GROUP));
                mc.Parameters.Add(new MySqlParameter("ID_CABINET", timetable.ID_CABINET));
                mc.Parameters.Add(new MySqlParameter("ID_LESSONS", timetable.ID_LESSONS));
                mc.Parameters.Add(new MySqlParameter("ID_Pair_Number", timetable.ID_Pair_Number));
                mc.Parameters.Add(new MySqlParameter("ID_Week_Day", timetable.ID_Week_Day));
                mc.Parameters.Add(new MySqlParameter("ID_TEACHER", timetable.ID_TEACHER));
                mc.ExecuteNonQuery();

            }
        }
        //удаление данных с таблицы в бд
        internal void Remove(TimeTable timetable)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM timeTable WHERE id = '" + timetable.id + "';";
            sql += "DELETE FROM timeTable WHERE id = '" + timetable.id + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

        internal void RemoveAll(TimeTable timetable)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM timetable";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }
        //обновление данных в таблице бд
        internal void UpdateTimeTable(TimeTable timetable)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE timetable SET ID_GROUP = @ID_GROUP, ID_CABINET = @ID_CABINET, ID_LESSONS = @ID_LESSONS, ID_Pair_Number = @ID_Pair_Number, ID_Week_Day = @ID_Week_Day, ID_TEACHER = @ID_TEACHER WHERE id = " + timetable.id;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("ID_GROUP", timetable.ID_GROUP));
                mc.Parameters.Add(new MySqlParameter("ID_CABINET", timetable.ID_CABINET));
                mc.Parameters.Add(new MySqlParameter("ID_LESSONS", timetable.ID_LESSONS));
                mc.Parameters.Add(new MySqlParameter("ID_Pair_Number", timetable.ID_Pair_Number));
                mc.Parameters.Add(new MySqlParameter("ID_Week_Day", timetable.ID_Week_Day));
                mc.Parameters.Add(new MySqlParameter("ID_TEACHER", timetable.ID_TEACHER));
                mc.ExecuteNonQuery();
            }
        }

        //internal IEnumerable<TimeTable> Search(string searchText)
        //{
        //    string sql = "SELECT FROM Timetable WHERE AND";
        //    sql += " AND ( LIKE '%" + searchText + "%'";
        //    sql += " OR LIKE '%" + searchText + "%') order by";

        //    if (selectedTimeTable.id != 0)
        //    {
        //        var result = GetAllTimeTable(sql).Where(s => s..(s => s.ID == selectedTimeTable.id) != null);
        //        return result;
        //    }
        //    return GetAllTimeTable(sql);
        //}
    }
}
