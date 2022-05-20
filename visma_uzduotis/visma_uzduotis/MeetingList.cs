using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visma_uzduotis
{
    internal class MeetingList
    {
        string line = "--------------------------------" +
            "---------------------------------------------" +
            "-------------------------------------------";
        public MeetingList() { }

        public Boolean AllYourMeetings(List<Meeting> list, string userName)
        {
            List<Meeting> results = new List<Meeting>(list.FindAll(list=>list.ResponsiblePerson == userName));
            if(results != null)
            {
                Console.WriteLine("\nYour meetings:");
                foreach (var result in results)
                {
                    Console.WriteLine(result.Name);
                }
                return true;
            }
            else
            { 
                return false;
            }
        }
        public int YourMeeting(List<Meeting> list, string nameOfMeeting, string userName)
        {
            string? nameCheck;
            if (String.IsNullOrEmpty(nameOfMeeting))
            {
                Console.WriteLine("ERROR: given meeting name is empty");
                return 2;
            }
            var value = list.FirstOrDefault(c => c.Name == nameOfMeeting);
            if (value != null)
            {
                nameCheck = value.ResponsiblePerson;
            }
            else
            {
                Console.WriteLine("Meeting not found");
                return 3;
            }
            if(nameCheck != null)
            {
                if (nameCheck.CompareTo(userName) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            return 4;
        }

        public void printList(List<Meeting> list)
        {
            Console.WriteLine(line);
            Console.WriteLine("| Name           | Res. Person  | Description   " +
                                    "       | Category     | Type     | Start Date  " +
                                    "     | End Date         |");
            Console.WriteLine(line);
            foreach(Meeting a in list)
            {
                Console.WriteLine(a.ToString());
                Console.WriteLine(line);
            }
        }
        public void printParticipants(List<string> participants)
        {
            Console.WriteLine("\nParticipants:");
            foreach (string participant in participants)
            {
                Console.WriteLine(participant);
            }
        }

        public void AddPersonToMeeting( string meetingName, string personName, List<Meeting> list )
        {
            int indexOfMeeting = list.FindIndex(c => c.Name == meetingName);
            List<string> constParticipants = new List<string>();
            if(indexOfMeeting < 0)
            {
                Console.WriteLine("ERROR:there is no meeting with name: "+meetingName);
            }
            else
            {
                var constMeeting = list[indexOfMeeting];

                DateTime newStartDate = constMeeting.StartDate ?? new DateTime(1, 1, 1, 0, 0, 0);
                DateTime newEndDate = constMeeting.EndDate ?? new DateTime(1, 1, 1, 0, 0, 0);

                if (TimeIntersects(list, personName, newStartDate, newEndDate))
                {
                    if (constMeeting.Participants != null)
                    {
                        if (constMeeting.Participants.Contains(personName))
                        {
                            Console.WriteLine("Participant is already in this meeting");
                        }
                        else
                        {
                            constMeeting.Participants.Add(personName);
                            Console.WriteLine(String.Format("Participant {0}  is added to meeting at {1:yyyy/MM/dd HH:mm}", personName, DateTime.Now));
                        }
                    }
                    else
                    {
                        constMeeting.Participants = new List<string>();
                        constMeeting.Participants.Add(personName);
                    }
                    list[indexOfMeeting] = constMeeting;
                }
                else
                {
                    Console.WriteLine(String.Format("WARNING: {0} is already in other meeting which intersect with this meeting", personName));
                }
                

            }
        }

        static Boolean TimeIntersects(List<Meeting> list, string personName, DateTime startDate, DateTime endDate)
        {
            foreach (var result in list )
            {
                if(result.Participants != null)
                {
                    if (result.Participants.Contains(personName))
                    {
                        DateTime newStartDate = result.StartDate ?? new DateTime(1, 1, 1, 0, 0, 0);
                        DateTime newEndDate = result.EndDate ?? new DateTime(1, 1, 1, 0, 0, 0);

                        if ((DateTime.Compare(newStartDate, startDate) == 0) || (DateTime.Compare(newEndDate, startDate) == 0) || (DateTime.Compare(newStartDate, endDate) == 0) || (DateTime.Compare(newEndDate, endDate) == 0))
                        {
                            return false;
                        }
                        if (((DateTime.Compare(newStartDate,startDate) < 0) && (DateTime.Compare(newEndDate, startDate) > 0)) || ((DateTime.Compare(newStartDate, endDate) < 0) && (DateTime.Compare(newEndDate, endDate) > 0)))
                        {
                            return false;
                        }
                        else if (((DateTime.Compare(newStartDate, startDate) > 0) && (DateTime.Compare(newEndDate, startDate) < 0)) || ((DateTime.Compare(newStartDate, endDate) > 0) && (DateTime.Compare(newEndDate, endDate) < 0)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        public List<Meeting> ListByDescription(List<Meeting> list, string description)
        {
            var listOfDescription= new List<Meeting>();
            if (description.Equals(""))
            {
                listOfDescription = list.FindAll(c => c.Description.Equals(""));
            }
            else
            {
                listOfDescription = list.FindAll(c => c.Description.Contains(description));
            }

            return listOfDescription ?? new List<Meeting>();
        }
        public List<Meeting> ListByResponsiblePerson(List<Meeting> list, string userName)
        {
            var listOfResponsiblePerson = list.FindAll(c => c.ResponsiblePerson == userName);
            return listOfResponsiblePerson?? new List<Meeting>();
        }
        public List<Meeting> ListByCategory(List<Meeting> list, int id)
        {
            Meeting obj = new Meeting();
            var listOfCategory = list.FindAll(c => c.Category == obj.getCategories(id));
            return listOfCategory ?? new List<Meeting>();
        }
        public List<Meeting> ListByType(List<Meeting> list, int id)
        {
            Meeting obj = new Meeting();
            var listOfType = list.FindAll(c => c.Type == obj.getTypes(id));
            return listOfType ?? new List<Meeting>();
        }
        public List<Meeting> ListByStartDate(List<Meeting> list, DateTime startDate)
        {
            var listOfDate = list.FindAll(c => c.StartDate >= startDate);
            return listOfDate ?? new List<Meeting>();
        }
        public List<Meeting> ListByStartEndDate(List<Meeting> list, DateTime startDate, DateTime endDate)
        {
            var listOfDate = list.FindAll(c => c.StartDate >= startDate && c.StartDate<=endDate);
            return listOfDate ?? new List<Meeting>();
        }
        public List<Meeting> ListOfAttendees(List<Meeting> list, int nr)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Participants.Count);
            }
            List<Meeting>? listOfAttendees = list.FindAll(c => c.Participants.Count >= nr);
            return listOfAttendees ?? new List<Meeting>();
        }
    }
}
