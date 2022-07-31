using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.Dto;
using vismaProject.models;

namespace vismaProject.services
{
    public interface IMeetingService
    {
        string CreateMeeting(NewMeetingDto dto);
        void SaveMeeting(Meeting meeting);
        List<Meeting> GetMeetings();
        List<Meeting> GetMeetingsByDescription(string desciption);
        List<Meeting> GetMeetingsByResponsiblePerson(string person);
        List<Meeting> GetMeetingsByCategory(string category);
        List<Meeting> GetMeetingsByAttendes(int attendees, string equal);
        List<Meeting> GetMeetingsByDates(DateTime startDate, DateTime endDate);
        List<Meeting> GetMeetingsByType(string type);
        string DeleteMeeting(string meetingName, string userName);
        string AddAnAttendee(string meetingName, string attendee);
        string RemoveAnAttendee(string meetingName, string name);


    }
}
