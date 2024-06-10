using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class SearchTimeTable
    {
        public string GROUP { get; set; } = string.Empty;
        public string TEACHER { get; set; } = string.Empty;
        public string CABINET { get; set; } = string.Empty;
        public string LESSONS { get; set; } = string.Empty;
        public string COURSE { get; set; } = string.Empty;
        public string PAIRNUMBER { get; set; } = string.Empty;
        public string WEEKDAY { get; set; } = string.Empty;
    }
}
