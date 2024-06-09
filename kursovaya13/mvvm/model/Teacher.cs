using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class Teacher
    {
        public int Teacher_ID { get; set; }
        public string TeacherTitle { get; set; } = string.Empty;
        public string Absent { get; set; }
    }
}
