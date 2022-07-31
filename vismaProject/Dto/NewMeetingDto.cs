using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.enums;


namespace vismaProject.Dto
{
    public class NewMeetingDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ResponsiblePerson { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String Category { get; set; }

        public String Type { get; set; }

        public List<string> Attendees { get; set; }
    }
}
