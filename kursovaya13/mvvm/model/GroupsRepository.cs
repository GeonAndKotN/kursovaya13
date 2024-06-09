using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kursovaya13.mvvm.model
{
    public class GroupsRepository
    {
        static GroupsRepository instance;
        public static GroupsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new GroupsRepository();
                return instance;
            }
        }
        internal IEnumerable<Groups> GetAllGroups(string sql)
        {
            var result = new List<Groups>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Groups groups = new Groups();
                Course course = new Course();
                int idr;
                while (reader.Read())
                {
                    idr = reader.GetInt32("Group_Id");
                    if (groups.Group_ID != idr)
                    {
                        groups = new Groups();
                        result.Add(groups);
                        groups.Group_ID = idr;
                        groups.GroupTitle = reader.GetString("TitleG");
                        groups.ID_Course = reader.GetInt32("ID_Course");
                        course = new Course
                        {
                            id = reader.GetInt32("id"),
                            Title = reader.GetString("TitleCR"),
                        };
                        if (groups.ID_Course == course.id)
                            groups.CourseTitleG = course.Title;
                    }
                }
            }
            return result;
        }

        internal void AddGroups(Groups groups)
        {
            Course course = new Course();
            int idc;
            var connectc = MySqlDB.Instance.GetConnection();
            if (connectc == null)
                return;
            string sqlc = "SELECT lessonsbykiprin.course.id, lessonsbykiprin.course.Title AS TitleCR FROM lessonsbykiprin.course; ";
            using (var mc = new MySqlCommand(sqlc, connectc))
            using (var reader = mc.ExecuteReader())
            {
                if (groups.CourseTitleG == "4" || groups.CourseTitleG == "1" || groups.CourseTitleG == "2" || groups.CourseTitleG == "3")
                {
                    while (reader.Read())
                    {
                        idc = reader.GetInt32("id");
                        if (course.id != idc)
                        {
                            course = new Course();
                            course.id = idc;
                            course.Title = reader.GetString("TitleCR");

                        }
                        if (groups.CourseTitleG == course.Title)
                        {
                            connectc.Close();
                            var connect = MySqlDB.Instance.GetConnection();
                            if (connect == null)
                                return;
                            int id = MySqlDB.Instance.GetAutoID("Group_Id");
                            string sql = "INSERT INTO ggroups VALUES (0, @Title, @ID_Course)";
                            using (var mcc = new MySqlCommand(sql, connect))
                            {
                                groups.ID_Course = course.id;
                                mcc.Parameters.Add(new MySqlParameter("Title", groups.GroupTitle));
                                mcc.Parameters.Add(new MySqlParameter("ID_Course", groups.ID_Course));
                                mcc.ExecuteNonQuery();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные!", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }
        

        internal void Remove(Groups groups)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM ggroups WHERE Group_Id = '" + groups.Group_ID + "';";
            sql += "DELETE FROM ggroups WHERE Group_Id = '" + groups.Group_ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

        internal void UpdateGroups(Groups groups)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE ggroups SET Title = @GroupTitle, ID_Course = @ID_Course WHERE Group_Id = " + groups.Group_ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("GroupTitle", groups.GroupTitle));
                mc.Parameters.Add(new MySqlParameter("ID_Course", groups.ID_Course));
                mc.ExecuteNonQuery();
            }
        }
    }
}
