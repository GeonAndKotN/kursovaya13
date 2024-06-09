using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kursovaya13.mvvm.model
{
    public class TeacherRepository
    {
        static TeacherRepository instance;
        public static TeacherRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new TeacherRepository();
                return instance;
            }
        }

        internal IEnumerable<Teacher> GetAllTeacher(string sql)
        {
            var result = new List<Teacher>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Teacher teacher = new Teacher();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("Teacher_id");
                    if (teacher.Teacher_ID != id)
                    {
                            teacher = new Teacher();
                            result.Add(teacher);
                            teacher.Teacher_ID = id;
                            teacher.TeacherTitle = reader.GetString("Title");
                            teacher.Absent = reader.GetString("Absent");
                    }
                }
            }
            return result;
        }

        internal void AddTeacher(Teacher teacher)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;
            int id = MySqlDB.Instance.GetAutoID("Teacher_Id");
            string Absent = MySqlDB.Instance.GetAutoYes("Absent");
            string sql = "INSERT INTO teacher VALUES (0, @Title, @Absent)";
            Lessons lessons = new Lessons();
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("Title", teacher.TeacherTitle));
                mc.Parameters.Add(new MySqlParameter("Absent", teacher.Absent));
                mc.ExecuteNonQuery();
            }
        }

        internal void Remove(Teacher teacher)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM teacher WHERE Teacher_Id = '" + teacher.Teacher_ID + "';";
            sql += "DELETE FROM teacher WHERE Teacher_Id = '" + teacher.Teacher_ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

        internal void UpdateTeacher(Teacher teacher)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE teacher SET Title = @TeacherTitle, Absent = @Absent WHERE Teacher_Id = " + teacher.Teacher_ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("TeacherTitle", teacher.TeacherTitle));
                mc.Parameters.Add(new MySqlParameter("Absent", teacher.Absent));
                mc.ExecuteNonQuery();
            }
        }
    }
}
