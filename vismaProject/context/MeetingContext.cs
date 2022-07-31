using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace vismaProject.context
{
    public class MeetingContext : IMeetingContext
    {
        public List<Meeting> GetAllMeetings()
        {
            List<Meeting> source = new List<Meeting>();

            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());


            using (StreamReader r = new StreamReader("C:/Users/glide/Desktop/LaikinasGintares/visma/data.txt"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Meeting>>(json,options);
            }
            return source; 
        }

        public void SaveMeeting(Meeting newMeeting)
        {
            List<Meeting> meetings = GetAllMeetings();
            meetings.Add(newMeeting);

            SaveMeetings(meetings);
        }

        public void DeleteMeeting(Meeting meetingToRemove, List<Meeting> meetings)
        {

            meetings.Remove(meetingToRemove);
            SaveMeetings(meetings);


        }

        public void SaveMeetings(List<Meeting> meetings)
        {

            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());

            string jsonString = JsonSerializer.Serialize(meetings, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("C:/Users/glide/Desktop/LaikinasGintares/visma/data.txt"))
            {
                outputFile.WriteLine(jsonString);
            }
        }
    }
}
