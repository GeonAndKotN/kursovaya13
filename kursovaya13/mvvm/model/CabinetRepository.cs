using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class CabinetRepository
    {
        static CabinetRepository instance;
        public static CabinetRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new CabinetRepository();
                return instance;
            }
        }

        internal IEnumerable<Cabinet> GetAllCabinet(string sql)
        {
            var result = new List<Cabinet>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Cabinet cabinet = new Cabinet();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("Cabinet_ID");
                    if (cabinet.Cabinet_ID != id)
                    {
                        cabinet = new Cabinet();
                        result.Add(cabinet);
                        cabinet.Cabinet_ID = id;
                        cabinet.CabinetTitle = reader.GetString("Title");
                        cabinet.Available = reader.GetString("Available");
                        cabinet.Appointment = reader.GetString("Appointment");
                    }
                }
            }
            return result;
        }

        internal void AddCabinet(Cabinet cabinet)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;
            int id = MySqlDB.Instance.GetAutoID("Cabinet_ID");
            string sql = "INSERT INTO cabinet VALUES (0, @Title, @Available, @Appointment)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("Title", cabinet.CabinetTitle));
                mc.Parameters.Add(new MySqlParameter("Available", cabinet.Available));
                mc.Parameters.Add(new MySqlParameter("Appointment", cabinet.Appointment));
                mc.ExecuteNonQuery();
            }
        }

        internal void Remove(Cabinet cabinet)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM cabinet WHERE Cabinet_ID = '" + cabinet.Cabinet_ID + "';";
            sql += "DELETE FROM cabinet WHERE Cabinet_ID = '" + cabinet.Cabinet_ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

        internal void UpdateCabinet(Cabinet cabinet)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE cabinet SET Title = @CabinetTitle, Available = @Available, Appointment = @Appointment WHERE Cabinet_ID = " + cabinet.Cabinet_ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("CabinetTitle", cabinet.CabinetTitle));
                mc.Parameters.Add(new MySqlParameter("Available", cabinet.Available));
                mc.Parameters.Add(new MySqlParameter("Appointment", cabinet.Appointment));
                mc.ExecuteNonQuery();
            }
        }
    }
}
