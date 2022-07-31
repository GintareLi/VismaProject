using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.enums;

namespace vismaProject.models
{
    public class Meeting
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ResponsiblePerson {get; set;}

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MeetingCategoryTypeEnum Category { get; set; }

        public MeetingTypeEnum Type { get; set; }

        public List<string> Attendees { get; set; }

    }
}
