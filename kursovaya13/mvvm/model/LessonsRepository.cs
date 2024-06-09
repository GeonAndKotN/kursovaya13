using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace kursovaya13.mvvm.model
{
    public class LessonsRepository
    {
        static LessonsRepository instance;
        public static LessonsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new LessonsRepository();
                return instance;
            }
        }

        internal IEnumerable<Lessons> GetAllLessons(string sql)
        {
            var result = new List<Lessons>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Lessons lessons = new Lessons();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("Lessons_id");
                    if (lessons.Lessons_ID != id)
                    {
                        lessons = new Lessons();
                        result.Add(lessons);
                        lessons.Lessons_ID = id;
                        lessons.LessonsTitle = reader.GetString("Title");
                        lessons.Teacher_IDL = reader.GetInt32("Teacher_IDL");
                    }
                }
            }
            return result;
        }

        internal void AddLessons(Lessons lessons)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;
            int id = MySqlDB.Instance.GetAutoID("Lessons_Id");
            string sql = "INSERT INTO lessons VALUES (0, @title, @Teacher_IDL)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", lessons.LessonsTitle));
                mc.Parameters.Add(new MySqlParameter("Teacher_IDL", lessons.Teacher_IDL));
                mc.ExecuteNonQuery();
            }
        }

        internal void Remove(Lessons lessons)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM lessons WHERE Lessons_Id = '" + lessons.Lessons_ID + "';";
            sql += "DELETE FROM lessons WHERE Lessons_Id = '" + lessons.Lessons_ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

        internal void UpdateLessons(Lessons lessons)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE lessons SET Title = @LessonsTitle, Teacher_IDL = @Teacher_IDL WHERE Lessons_Id = " + lessons.Lessons_ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("LessonsTitle", lessons.LessonsTitle));
                mc.Parameters.Add(new MySqlParameter("Teacher_IDL", lessons.Teacher_IDL));
                mc.ExecuteNonQuery();
            }
        }
    }
}
