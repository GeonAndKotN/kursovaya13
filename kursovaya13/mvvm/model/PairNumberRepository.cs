using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class PairNumberRepository
    {
        static PairNumberRepository instance;
        public static PairNumberRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new PairNumberRepository();
                return instance;
            }
        }

        internal IEnumerable<PairNumber> GetAllPair(string sql)
        {
            var result = new List<PairNumber>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                PairNumber pair = new PairNumber();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("id");
                    if (pair.id != id)
                    {
                        pair = new PairNumber();
                        result.Add(pair);
                        pair.id = id;
                        pair.Title = reader.GetString("Title");
                    }
                }
            }
            return result;
        }
    }
}
