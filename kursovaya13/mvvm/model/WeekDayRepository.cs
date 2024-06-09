using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class WeekDayRepository
    {
        static WeekDayRepository instance;
        public static WeekDayRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new WeekDayRepository();
                return instance;
            }
        }

        internal IEnumerable<WeekDay> GetAllWeekDay(string sql)
        {
            var result = new List<WeekDay>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                int id;
                while (reader.Read())
                {
                    WeekDay weekDay = new WeekDay();
                    id = reader.GetInt32("id");
                    if (weekDay.id != id)
                    {
                        weekDay = new WeekDay();
                        result.Add(weekDay);
                        weekDay.id = id;
                        weekDay.Title = reader.GetString("Title");
                    }
                }
            }
            return result;
        }
    }
}
