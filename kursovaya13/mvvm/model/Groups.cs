using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya13.mvvm.model
{
    public class Groups
    {
        public int Group_ID { get; set; }
        public string GroupTitle { get; set; } = string.Empty;
        public int ID_Course { get; set; }
        public string CourseTitleG { get; set; } = string.Empty;
    }
}
