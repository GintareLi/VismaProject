using Microsoft.VisualStudio.TestTools.UnitTesting;
using vismaProject.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.Dto;
using NSubstitute;
using NUnit.Framework;

namespace vismaProject.services.Tests//not working
{
    [TestClass()]
    public class MeetingServiceTests
    {
        private IMeetingService _meetingService;

        [SetUp]
        public void TestInitializer()
        {
            _meetingService = Substitute.For<IMeetingService>();
        }

        [Test]
        public void CreateMeetingTest()
        {
            var meeting = new NewMeetingDto()
            {
                Name = "name",
                Description = "description",
                ResponsiblePerson = "responsiblePerson",
                StartDate = new DateTime(2000 - 10 - 10),
                EndDate = new DateTime(2000 - 10 - 11),
                Category = "Short",
                Type = "Live",
                Attendees = new List<string> { "lukas", "matas", "jonas" }
            };
            var msg = _meetingService.CreateMeeting(meeting);

            NUnit.Framework.Assert.AreEqual("meeting " + meeting.Name + " was created\n", msg);

        }
    }
}
