using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.models;

namespace vismaProject.context
{
    public interface IMeetingContext
    {
        List<Meeting> GetAllMeetings();
        void SaveMeeting(Meeting newMeeting);
        void DeleteMeeting(Meeting meetingToRemove, List<Meeting> meetings);
        void SaveMeetings(List<Meeting> meetings);


    }
}
