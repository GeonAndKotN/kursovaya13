using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class Lessons
    {
        public int Lessons_ID { get; set; }
        public string LessonsTitle { get; set; } = string.Empty;
        public int Teacher_IDL { get; set; }
    }
}
