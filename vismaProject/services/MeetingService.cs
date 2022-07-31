using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.context;
using vismaProject.Dto;
using vismaProject.enums;
using vismaProject.models;

namespace vismaProject.services
{
    public class MeetingService : IMeetingService
    {

        private readonly IMeetingContext _meetingContext;

        public MeetingService(IMeetingContext meetingContext)
        {
            _meetingContext = meetingContext;
        }

        public string CreateMeeting(NewMeetingDto dto)
        {
            var meeting = new Meeting
            {
                Name = dto.Name,
                Description = dto.Description,
                ResponsiblePerson = dto.ResponsiblePerson,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Category = (MeetingCategoryTypeEnum)Enum.Parse(typeof(MeetingCategoryTypeEnum),dto.Category),
                Type = (MeetingTypeEnum)Enum.Parse(typeof(MeetingTypeEnum), dto.Type),
                Attendees = dto.Attendees


            };
            if(!meeting.Attendees.Contains(meeting.ResponsiblePerson))
            {
                meeting.Attendees.Add(meeting.ResponsiblePerson);
            }

            List<Meeting> meetings = _meetingContext.GetAllMeetings();
            if (_meetingContext.GetAllMeetings().Any(p => p.Name == meeting.Name))
                return "\nthere is already a meeting with this name\n";

            SaveMeeting(meeting);
            return "meeting " + meeting.Name + " was created\n";
        }
        public void SaveMeeting(Meeting meeting)
        {
            _meetingContext.SaveMeeting(meeting);

        }

        public List<Meeting> GetMeetings()
        {
            return _meetingContext.GetAllMeetings();
        }


        public List<Meeting> GetMeetingsByDescription(string description)
        {
            var meetings= _meetingContext.GetAllMeetings();

            var results = meetings.FindAll(m => m.Description.Contains(description));

            return results;
        }

        public List<Meeting> GetMeetingsByResponsiblePerson(string person)
        {
            var meetings = _meetingContext.GetAllMeetings();

            var results = meetings.FindAll(m => m.ResponsiblePerson == person);

            return results;
        }

        public List<Meeting> GetMeetingsByCategory(string category)
        {
            var meetings = _meetingContext.GetAllMeetings();

            var results = meetings.FindAll(m => m.Category == (MeetingCategoryTypeEnum)Enum.Parse(typeof(MeetingCategoryTypeEnum),category));

            return results;
        }

        public List<Meeting> GetMeetingsByType(string type)
        {
            var meetings = _meetingContext.GetAllMeetings();

            var results = meetings.FindAll(m => m.Type == (MeetingTypeEnum)Enum.Parse(typeof(MeetingTypeEnum), type));

            return results;
        }

        public List<Meeting> GetMeetingsByAttendes(int attendees, string equal)
        {
            var meetings = _meetingContext.GetAllMeetings();

            if(equal=="1")
            {
                return meetings.FindAll(m => m.Attendees.Count <= attendees);
            }else if(equal == "2")
            {
                return meetings.FindAll(m => m.Attendees.Count == attendees);
            }else
                return meetings.FindAll(m => m.Attendees.Count >= attendees);

        }

        public List<Meeting> GetMeetingsByDates(DateTime startDate, DateTime endDate)
        {
            var meetings = _meetingContext.GetAllMeetings();

                return meetings.FindAll(m => m.StartDate >= startDate && m.EndDate <= endDate);

        }

        public string DeleteMeeting(string meetingName, string userName)
        {
            List<Meeting> meetings = _meetingContext.GetAllMeetings();
            var itemToRemove = meetings.Single(r => r.Name == meetingName);
            if (itemToRemove.ResponsiblePerson != userName)
                return "you do not have persmision to delete this meeting \n";
            else
                _meetingContext.DeleteMeeting(itemToRemove, meetings);
            return " meeting "+ meetingName + " was deleted \n";
            
        }

        public string AddAnAttendee(string meetingName, string attendee)
        {
            List<Meeting> meetings = _meetingContext.GetAllMeetings();
            var meeting = meetings.Single(r => r.Name == meetingName);
            string message = "";

            foreach (var i in meeting.Attendees)
            {
                if (i == attendee)
                    return "This person is already in the meeting \n";
            }
            
            foreach (var i in meetings)
            {
                if (meeting != i)
                {
                    if(meeting.EndDate>i.StartDate && meeting.StartDate<i.EndDate)
                    {
                        if (i.Attendees.Contains(attendee))
                          message = "this person is already participating in another meeting at this time" + "\n";
                    }
                }
            }

            meetings.Single(r => r == meeting).Attendees.Add(attendee);
            _meetingContext.SaveMeetings(meetings);


            return message+ "\n" + attendee + " was added to a meeting at " + DateTime.Now.ToString("h:mm:ss tt") + "\n";
        }

        public string RemoveAnAttendee(string meetingName, string name)
        {

            List<Meeting> meetings = _meetingContext.GetAllMeetings();
            var meeting = meetings.Single(r => r.Name == meetingName);
            if(!meeting.Attendees.Contains(name))
                return name+ " is not participating in the meeting " + meetingName+"\n";
            if (meeting.ResponsiblePerson==name)
            {
                return "Responsible person can not be removed from the meeting\n";
            }
            meetings.Single(r => r == meeting).Attendees.Remove(name);
            _meetingContext.SaveMeetings(meetings);
            return name+ " is removed from the meeting\n";
        }


    }
}

