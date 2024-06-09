using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class CourseRepository
    {
        static CourseRepository instance;
        public static CourseRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new CourseRepository();
                return instance;
            }
        }

        internal IEnumerable<Course> GetAllCourse(string sql)
        {
            var result = new List<Course>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Course course = new Course();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("id");
                    if (course.id != id)
                    {
                        course = new Course();
                        result.Add(course);
                        course.id = id;
                        course.Title = reader.GetString("Title");
                    }
                }
            }
            return result;
        }
    }
}
