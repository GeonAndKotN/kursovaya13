using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class Cabinet
    {
        public int Cabinet_ID { get; set; }
        public string CabinetTitle { get; set; } = string.Empty;
        public string Available { get; set; }
        public string Appointment { get; set; }
    }
}
