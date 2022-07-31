using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaProject.models;
using vismaProject.context;
using vismaProject.services;
using Microsoft.Extensions.DependencyInjection;
using vismaProject.Dto;
using vismaProject.enums;
using System.Data.Entity.Core.Metadata.Edm;

namespace vismaProject
{
    public class Program
    {


        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
            .AddSingleton<IMeetingService, MeetingService>()
            .AddSingleton<IMeetingContext, MeetingContext>()
            .BuildServiceProvider();

            var meetingServcice = serviceProvider.GetService<IMeetingService>();

            Console.WriteLine("Visma internal meetings app \nType in your name:");


            var userName = Console.ReadLine().ToUpper();

            displayMenu(meetingServcice, userName);

        }

        static void displayMenu(IMeetingService meetingServcice, string userName)
        {
            Console.WriteLine("choose what would you like to do:");
            Console.WriteLine(" 1)See the meetings \n 2)Add a new meeting \n 3)Delete a meeting \n 4)Add a person to a meeting \n 5)Remove a person from a meeting");

            var input = Console.ReadLine();
            string meetingName;
            string atendeeName;



            switch (input)
            {
                case "1":
                    {
                        filterBy(meetingServcice);

                    }
                    break;
                case "2":
                    Console.WriteLine("Type-in the Name of the new meeting: ");
                    var newMeetingName = Console.ReadLine().ToUpper();
                    Console.WriteLine("Type-in the Description of the new meeting: ");
                    var newMeetingDescription = Console.ReadLine().ToUpper();
                    Console.WriteLine("Type-in the ResponsiblePerson of the new meeting: ");
                    var newMeetingResponsiblePerson = Console.ReadLine().ToUpper();
                    Console.WriteLine("Type-in the Start date of the new meeting in format year-month-day: ");
                    var newMeetingStartDate = Console.ReadLine();
                    Console.WriteLine("Type-in the End date of the new meeting in format year - month - day Hours:Minutes : ");
                    var newMeetingEndDate = Console.ReadLine();
                    Console.WriteLine("What is the category of the meeting (CodeMonkey,Hub,Short,TeamBuilding):  ");
                    var newMeetingCategory = Console.ReadLine().ToUpper();
                    Console.WriteLine("What is the type of the meeting (Live,InPerson):  ");
                    var newMeetingType = Console.ReadLine().ToUpper();
                    Console.WriteLine("Who are the attendees of the meeting:  ");
                    var newMeetingAtendees = Console.ReadLine().ToUpper();
                    NewMeetingDto newmeetingDto = new NewMeetingDto
                    {
                        Name = newMeetingName,
                        Description = newMeetingDescription,
                        ResponsiblePerson = newMeetingResponsiblePerson,
                        StartDate = Convert.ToDateTime(newMeetingStartDate),
                        EndDate = Convert.ToDateTime(newMeetingEndDate),
                        Category = newMeetingCategory,
                        Type = newMeetingType,
                        Attendees = newMeetingAtendees.Split(',').ToList()
                    };
                    Console.WriteLine(meetingServcice.CreateMeeting(newmeetingDto));
                    break;
                case "3":
                    Console.WriteLine("Type-in the Name of the meeting you want to delete: ");
                    var deletedMeetingName = Console.ReadLine().ToUpper();
                    Console.WriteLine(meetingServcice.DeleteMeeting(deletedMeetingName, userName));
                    break;
                case "4":
                    Console.WriteLine("Type-in the Name of the meeting you want to add person to: ");
                    meetingName = Console.ReadLine().ToUpper();
                    Console.WriteLine("Type-in the Name of the person you want to add: ");
                    atendeeName = Console.ReadLine().ToUpper();
                    Console.WriteLine(meetingServcice.AddAnAttendee(meetingName, atendeeName));
                    break;
                case "5":
                    Console.WriteLine("Type-in the Name of the meeting you want to delete person from: ");
                    meetingName = Console.ReadLine().ToUpper();
                    Console.WriteLine("Type-in the Name of the person you want to delete: ");
                    atendeeName = Console.ReadLine().ToUpper();
                    Console.WriteLine(meetingServcice.RemoveAnAttendee(meetingName, atendeeName));
                    break;
                    


              
            }
            displayMenu(meetingServcice,userName);
        }

        static void filterBy(IMeetingService meetingServcice)
        {
            Console.WriteLine("How would you like to filter the meetings \n1)by description \n2)by responsible person \n3)by category \n4)by type \n5)by number of attendees \n6)by dates \n7)See all meetings");
            var input = Console.ReadLine();
            List<Meeting> meetings;
            switch (input)
            {
                case "1":
                    Console.WriteLine("Type-in the search word/sentence:");
                    var description = Console.ReadLine().ToUpper();
                    meetings = meetingServcice.GetMeetingsByDescription(description);
                    if(meetings.Count==0)
                    {
                        Console.WriteLine("there is no meeting that contains this description\n");
                        break;
                    }
                    else
                        Console.WriteLine("meetings that contains" + description + "in the description:\n");
                    PrintMeetings(meetings);
                    break;
                case "2":
                    Console.WriteLine("Type-in the responsible person:");
                    var person = Console.ReadLine().ToUpper();
                    meetings = meetingServcice.GetMeetingsByResponsiblePerson(person);
                    if (meetings.Count == 0)
                    {
                        Console.WriteLine("there is no meeting that this person is responsible for\n");
                        break;
                    }else
                        Console.WriteLine("meetings that " + person + "is responsible for:\n");
                    PrintMeetings(meetings);
                    break;
                case "3":
                    Console.WriteLine("Type-in the category you are interested in (CodeMonkey,Hub,Short,TeamBuilding):");
                    var category = Console.ReadLine().ToUpper();
                    meetings = meetingServcice.GetMeetingsByCategory(category);
                    if (meetings.Count == 0)
                    {
                        Console.WriteLine("there is no meeting that has this category \n");
                        break;
                    }
                    else
                        Console.WriteLine("meetings that has category " + category +":\n");
                    PrintMeetings(meetings);
                    break;
                case "4":
                    Console.WriteLine("Type-in the type you are interested in (Live,InPerson):");
                    var type = Console.ReadLine().ToUpper();
                    meetings = meetingServcice.GetMeetingsByType(type);
                    if (meetings.Count == 0)
                    {
                        Console.WriteLine("there is no meeting that has this type \n");
                        break;
                    }
                    else
                        Console.WriteLine("meetings that has type " + type + ":\n");
                    PrintMeetings(meetings);
                    break;
                case "5":
                    Console.WriteLine("Type-in the number of attendees you are interested in");
                    var attendees = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("do you want to see meetings that has "+attendees+"\n1)equal and less \n2)equal \n3)equal and more  \nattendees");
                    var equal= Console.ReadLine();
                    meetings = meetingServcice.GetMeetingsByAttendes(attendees, equal);
                    if (meetings.Count == 0)
                    {
                        Console.WriteLine("there is no meeting that has this amount of attendees \n");
                        break;
                    }
                    else
                        Console.WriteLine("meetings that has this amount of attendees:\n");
                    PrintMeetings(meetings);
                    break;
                case "6":
                    Console.WriteLine("Type-in the start date in the time range");
                    var startDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Type-in the end date in the time range");
                    var endDate = DateTime.Parse(Console.ReadLine());
                    meetings = meetingServcice.GetMeetingsByDates(startDate, endDate);
                    if (meetings.Count == 0)
                    {
                        Console.WriteLine("there is no meeting that happens in between these dates \n");
                        break;
                    }
                    else
                        Console.WriteLine("meetings that happends from "+ startDate +" to "+ endDate+ ":\n");
                    PrintMeetings(meetings);
                    break;
                case "7":
                    PrintMeetings(meetingServcice.GetMeetings());
                    break;

            }
        }
        static void PrintMeetings(List<Meeting> meetings)
        {
            foreach (Meeting v in meetings)
            {
                Console.WriteLine("name of the meeting : " + v.Name);
                Console.WriteLine("Responsible person of the meeting : " + v.ResponsiblePerson);
                Console.WriteLine("Description of the meeting : " + v.Description);
                Console.WriteLine("Date of the meeting " + v.StartDate + " - " + v.EndDate);
                Console.WriteLine("Category of the meeting : " + v.Category);
                Console.WriteLine("Type of the meeting : " + v.Type);
                Console.WriteLine("Attendees of the meeting : ");
                v.Attendees.ForEach(Console.WriteLine);
                Console.WriteLine("\n \n");

            }

        }
    }
}
