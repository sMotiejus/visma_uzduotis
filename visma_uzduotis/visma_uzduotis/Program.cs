using Newtonsoft.Json;
using visma_uzduotis;

string json_file = "../../../meetings.json";
Json json = new Json(json_file);

var meetings = new List<Meeting>();
meetings = json.ReadJson();
MeetingList listCommands = new MeetingList();

string line = "-----------------------------------" +
    "-----------------------------------------------" +
    "--------------------------------------";
string? userName;

Console.WriteLine("VISMA .NET6 Developer Task");
Console.WriteLine(line);
Console.WriteLine("Tell your name:");

// Doing while, until userName is given
while (true)
{
    userName = Console.ReadLine();
    if (String.IsNullOrEmpty(userName))
    {
        Console.WriteLine("You have not written anything. Retry:");
        continue;
    }
    break;
}
Console.WriteLine(line);

// COMMAND LINES MENIU LOOP
while (true)
{
    Console.WriteLine("\nHello, " + userName + "!!!");
    Console.WriteLine("Type a command ID:");
    Console.WriteLine("1. Create a new meeting\n2. Delete a meeting\n3. " +
                      "Add a person to the meeting\n4. Remove a person" +
                      " from the meeting\n5. List all the meetings\n" +
                      "(Type (q or quit) to quit the console)");

    Boolean programRunning = true;
    int intCommand = 0;
    while (programRunning)
    {
        string? commandId = Console.ReadLine();
        if (string.IsNullOrEmpty(commandId))
        {
            Console.WriteLine("You didn't write anything. Try again:");
            continue;
        }

        if (commandId.Equals("q") || commandId.Equals("quit"))
        {
            Console.WriteLine("Good Bye");
            return;
        }
        bool isNumeric = int.TryParse(commandId, out intCommand);
        if (isNumeric == false)
        {
            Console.WriteLine("You writed not number. Try again:");
            continue;
        }
        if (intCommand < 1 || intCommand > 5)
        {
            Console.WriteLine("Wrong Command ID number. Try again:");
            continue;
        }
        break;
    }

//------------------------------------------------------------------------------
//---------COMMAND LINE 1-------------------------------------------------------
//------------------------------------------------------------------------------
    if (intCommand == 1)
    {
        int meetingCategory = -1;
        int meetingType = -1;

        DateTime meetingStartDate = new DateTime(1, 1, 1, 0, 0, 0);
        DateTime meetingEndDate = new DateTime(1, 1, 1, 0, 0, 0);

        int[] startDateArray = new int[5];
        int[] endDateArray = new int[5];

        Console.WriteLine(line);
        Console.WriteLine("Creating a new meeting");
        Console.WriteLine(line);

        Console.WriteLine("Input Name of Meeting:");
        string? meetingName;
        while (true)
        {
            meetingName = Console.ReadLine();
            if (String.IsNullOrEmpty(meetingName))
            {
                Console.WriteLine("You have not written meeting name. Retry:");
                continue;
            }
            break;
        }

        Console.WriteLine("Input Description of Meeting:");
        string? meetingDescription = Console.ReadLine();

        Console.WriteLine("Input number of Category:\n( 0 - CodeMonkey, 1 - Hub, 2 - Short, 3 - TeamBuilding )\n(Not writing anything - 0)");
        string? meetingCategoryString = Console.ReadLine();
        if (!string.IsNullOrEmpty(meetingCategoryString))
        {
            meetingCategory = int.Parse(meetingCategoryString);
        }

        Console.WriteLine("Input number of Type:\n( 0 - Live, 1 - InPerson )\n(Not writing anything - 0)");
        string? meetingTypeString = Console.ReadLine();
        if (!string.IsNullOrEmpty(meetingTypeString))
        {
            meetingType = int.Parse(meetingTypeString);
        }

        Console.WriteLine("Meeting Start DateTime:\nType: YEAR, MONTH, DAY, HOUR, MINUTES - separated by space");
        string? meetingStartDateTime = Console.ReadLine();
        if (!string.IsNullOrEmpty(meetingStartDateTime))
        {
            int year = 1,
                month = 1,
                day = 1,
                hour = 0,
                minutes = 0;

            startDateArray = meetingStartDateTime.Split(' ').Select(Int32.Parse).ToArray();
            int gotSize = startDateArray.Length;
            if (startDateArray[0] > 0)
            {
                year = startDateArray[0];
            }
            if (gotSize > 1)
            {
                if (startDateArray[1] > 0 && startDateArray[1] <= 12)
                {
                    month = startDateArray[1];
                }
            }
            if (gotSize > 2)
            {
                if (startDateArray[2] > 0 && startDateArray[2] < 31)
                {
                    day = startDateArray[2];
                }
            }
            if (gotSize > 3)
            {
                if (startDateArray[3] > 0 && startDateArray[3] < 24)
                {
                    hour = startDateArray[3];
                }
            }
            if (gotSize > 4)
            {
                if (startDateArray[4] > 0 && startDateArray[4] < 60)
                {
                    minutes = startDateArray[4];
                }
            }

            meetingStartDate = new DateTime(year, month, day,
                                            hour, minutes, 0);
        }

        Console.WriteLine("Meeting End DateTime:\nType: YEAR, MONTH, DAY, HOUR, MINUTES - separated by space");
        string? meetingEndDateTime = Console.ReadLine();
        if (!string.IsNullOrEmpty(meetingEndDateTime))
        {
            int year = 1,
                month = 1,
                day = 1,
                hour = 0,
                minutes = 0;

            endDateArray = meetingEndDateTime.Split(' ').Select(Int32.Parse).ToArray();
            int gotSize = endDateArray.Length;

            if (endDateArray[0] > 0)
            {
                year = endDateArray[0];
            }
            if (gotSize > 1)
            {
                if (endDateArray[1] > 0 && endDateArray[1] <= 12)
                {
                    month = endDateArray[1];
                }
            }
            if (gotSize > 2)
            {
                if (endDateArray[2] > 0 && endDateArray[2] < 31)
                {
                    day = endDateArray[2];
                }
            }
            if (gotSize > 3)
            {
                if (endDateArray[3] >= 0 && endDateArray[3] < 24)
                {
                    hour = endDateArray[3];
                }
            }
            if (gotSize > 4)
            {
                if (endDateArray[4] >= 0 && endDateArray[4] < 60)
                {
                    minutes = endDateArray[4];
                }
            }

            meetingEndDate = new DateTime(year, month, day,
                                            hour, minutes, 0);
        }

        Meeting constMeeting = new Meeting(meetingName,
                                           userName,
                                           meetingDescription,
                                           meetingCategory,
                                           meetingType,
                                           meetingStartDate,
                                           meetingEndDate);
        meetings = json.ReadJson();
        meetings.Add(constMeeting);
        json.WriteJson(meetings);
    }
//------------------------------------------------------------------------------
//---------COMMAND LINE 2-------------------------------------------------------
//------------------------------------------------------------------------------
    if (intCommand == 2)
    {
        Console.WriteLine(line);
        Console.WriteLine("Delete meeting");
        Console.WriteLine(line);
        listCommands.AllYourMeetings(meetings, userName);

        Console.WriteLine("Type meeting name of meeting you want to delete:");
        string? nameOfMeeting = Console.ReadLine();
        if(nameOfMeeting != null)
        {
            int returnCode = listCommands.YourMeeting(meetings, nameOfMeeting, userName);
            if (returnCode==0)
            {
                var value = meetings.FirstOrDefault(c => c.Name == nameOfMeeting);
                if(value != null)
                {
                    meetings.Remove(value);
                    json.WriteJson(meetings);
                    meetings = json.ReadJson();
                    Console.WriteLine("Meeting was removed successfuly");
                }
            }
            else if(returnCode == 1)
            {
                Console.WriteLine("Can't delete. Not your meeting");
            }
        }
    }

//------------------------------------------------------------------------------
//---------COMMAND LINE 3-------------------------------------------------------
//------------------------------------------------------------------------------
    if (intCommand == 3)
    {
        meetings = json.ReadJson();
        Console.WriteLine(line);
        Console.WriteLine("Add person to meeting");
        Console.WriteLine(line);
        if (listCommands.AllYourMeetings(meetings, userName))
        {
            Console.WriteLine("\nType meeting name to where you want to add person:");
            string? nameOfMeeting = Console.ReadLine();
            if (nameOfMeeting != null)
            {
                if(listCommands.YourMeeting(meetings, nameOfMeeting, userName)==0)
                {
                    var meeting = meetings.Find(meetings => meetings.Name == nameOfMeeting);
                    if (meeting != null)
                    {
                        if (meeting.Participants != null)
                        {
                            listCommands.printParticipants(meeting.Participants);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You dont have any meetings");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("You dont have any meetings");
                    continue;
                }
                Console.WriteLine("\nType person name you want to add:");
                string? nameOfPerson = Console.ReadLine();
                if (nameOfPerson != null)
                {
                    listCommands.AddPersonToMeeting(nameOfMeeting, nameOfPerson, meetings);

                    json.WriteJson(meetings);
                }
                else
                {
                    Console.WriteLine("You have not typed anything");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("You have not typed anything");
                continue;
            }
        }
        else
        {
            Console.WriteLine("You don't have meeting to add person.");
        }
    }

    //------------------------------------------------------------------------------
    //---------COMMAND LINE 4-------------------------------------------------------
    //------------------------------------------------------------------------------
    if (intCommand == 4)
    {
        Console.WriteLine(line);
        Console.WriteLine("Remove person from meeting");
        Console.WriteLine(line);
        if (listCommands.AllYourMeetings(meetings, userName))
        {
            Console.WriteLine("\nType meeting name from where you want to remove person:");
            string? nameOfMeeting = Console.ReadLine();
            if (nameOfMeeting != null)
            {
                int returnCode = listCommands.YourMeeting(meetings, nameOfMeeting, userName);
                if (returnCode == 0)
                {
                    var vIndex = meetings.FindIndex(c => c.Name == nameOfMeeting);
                    var value = meetings.FirstOrDefault(c => c.Name == nameOfMeeting);
                    if (value != null)
                    {
                        if (value.Participants != null)
                        {
                            listCommands.printParticipants(value.Participants);

                            Console.WriteLine("Type participant name you want to remove:");
                            string? participantName = Console.ReadLine();
                            if (!String.IsNullOrEmpty(participantName))
                            {
                                if(value.ResponsiblePerson != null)
                                {
                                    if (value.Participants.Contains(participantName))
                                    {
                                        if (!value.ResponsiblePerson.Equals(participantName))
                                        {
                                            value.Participants.Remove(participantName);
                                            meetings[vIndex].Participants = value.Participants;
                                            json.WriteJson(meetings);
                                            Console.WriteLine(participantName + " was removed from meeting");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cant remove hoster from meeting");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("There in no participant in meeting: " + nameOfMeeting + " ,with name: " + participantName);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("You have not typed anything");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is not participants in this meeting");
                        }

                        //if(value.Participants.Contains())
                        //meetings.Remove(value);
                        //json.WriteJson(meetings);
                        //meetings = json.ReadJson();
                        //Console.WriteLine("Meeting was removed successfuly");
                    }
                }
                else if (returnCode == 1)
                {
                    Console.WriteLine("Not your meeting");
                    continue;
                }
            }
        }
        else
        {
            Console.WriteLine("You have no meetings");
            continue;
        }
        
    }
//------------------------------------------------------------------------------
//---------COMMAND LINE 5-------------------------------------------------------
//------------------------------------------------------------------------------
    if (intCommand == 5)
    {
        meetings = json.ReadJson();
        Console.WriteLine(line);
        Console.WriteLine("Meetings List");
        Console.WriteLine(line);
        Console.WriteLine("Filters:\n0 - No filter\n1 - Filer by description\n" +
                          "2 - Filter by responsible person\n3 - Filter by category\n" +
                          "4 - Filter by type\n5 - Filter by dates\n6 - Filter by number of attendees\n" +
                          "(Type (q or quit) to quit the console)");

        int filterIntId = 0;
        Boolean filterAsk = true;
        while (filterAsk)
        {
            string? filterId = Console.ReadLine();
            if (string.IsNullOrEmpty(filterId))
            {
                Console.WriteLine("You didn't write anything. Try again:");
                continue;
            }

            if (filterId.Equals("q") || filterId.Equals("quit"))
            {
                Console.WriteLine("Good Bye");
                return;
            }
            bool isNumeric = int.TryParse(filterId, out filterIntId);
            if (isNumeric == false)
            {
                Console.WriteLine("You writed not number. Try again:");
                continue;
            }
            if (filterIntId < 0 || filterIntId > 6)
            {
                Console.WriteLine("Wrong Command ID number. Try again:");
                continue;
            }
            break;
        }
        if (filterIntId==0)
        {
            Console.WriteLine("List printed with: No filter");
            listCommands.printList(meetings);
        }
        if (filterIntId == 1)
        {
            Console.WriteLine("Type description:");
            string? description = Console.ReadLine();
            if (description == null)
            {
                description = "";
            }

            var listOfDescription = listCommands.ListByDescription(meetings,description);
            Console.WriteLine("List printed with: Filtered by description");
            listCommands.printList(listOfDescription);
        }
        if (filterIntId == 2)
        {
            Console.WriteLine("Input Responsible Person name:");
            string? responsiblePerson = Console.ReadLine();
            if(responsiblePerson != null)
            {
                var resposiblePersonList = listCommands.ListByResponsiblePerson(meetings, responsiblePerson);
                Console.WriteLine("List printed with: Filtered by responsible person");
                listCommands.printList(resposiblePersonList);
            }
        }
        if (filterIntId == 3)
        {
            int meetingCategory = 0;
            Console.WriteLine("Input number of Category:\n( 0 - CodeMonkey, 1 - Hub, 2 - Short, 3 - TeamBuilding )\n(Not writing anything - 0)");
            string? meetingCategoryString = Console.ReadLine();
            if (!string.IsNullOrEmpty(meetingCategoryString))
            {
                meetingCategory = int.Parse(meetingCategoryString);
            }
            else
            {
                meetingCategory = 0;
            }
            var categoryList = listCommands.ListByCategory(meetings, meetingCategory);
            Console.WriteLine("List printed with: Filtered by category");
            listCommands.printList(categoryList);
        }
        if (filterIntId == 4)
        {
            int meetingType = 0;
            Console.WriteLine("Input number of Type:\n( 0 - Live, 1 - InPerson )\n(Not writing anything - 0)");
            string? meetingTypeString = Console.ReadLine();
            if (!string.IsNullOrEmpty(meetingTypeString))
            {
                meetingType = int.Parse(meetingTypeString);
            }
            else
            {
                meetingType = 0;
            }
            var typeList = listCommands.ListByType(meetings, meetingType);
            Console.WriteLine("List printed with: Filtered by type");
            listCommands.printList(typeList);
        }
        if (filterIntId == 5)
        {
            DateTime meetingStartDate = new DateTime(1, 1, 1, 0, 0, 0);
            DateTime meetingEndDate = new DateTime(1, 1, 1, 0, 0, 0);

            int[] startDateArray = new int[5];
            int[] endDateArray = new int[5];
            Console.WriteLine("Type Start DateTime:\nType: YEAR, MONTH, DAY, HOUR, MINUTES - separated by space");
            string? meetingStartDateTime = Console.ReadLine();
            if (!string.IsNullOrEmpty(meetingStartDateTime))
            {
                int year = 1,
                    month = 1,
                    day = 1,
                    hour = 0,
                    minutes = 0;

                startDateArray = meetingStartDateTime.Split(' ').Select(Int32.Parse).ToArray();
                int gotSize = startDateArray.Length;
                if (startDateArray[0] > 0)
                {
                    year = startDateArray[0];
                }
                if (gotSize > 1)
                {
                    if (startDateArray[1] > 0 && startDateArray[1] <= 12)
                    {
                        month = startDateArray[1];
                    }
                }
                if (gotSize > 2)
                {
                    if (startDateArray[2] > 0 && startDateArray[2] < 31)
                    {
                        day = startDateArray[2];
                    }
                }
                if (gotSize > 3)
                {
                    if (startDateArray[3] > 0 && startDateArray[3] < 24)
                    {
                        hour = startDateArray[3];
                    }
                }
                if (gotSize > 4)
                {
                    if (startDateArray[4] > 0 && startDateArray[4] < 60)
                    {
                        minutes = startDateArray[4];
                    }
                }

                meetingStartDate = new DateTime(year, month, day,
                                                hour, minutes, 0);
            }
            Console.WriteLine("Will you write End date? Type: y/n");
            string? endEnable = Console.ReadLine();
            var dateList = new List<Meeting>();
            if (endEnable != null)
            {
                if (endEnable.CompareTo("y") == 0)
                {
                    Console.WriteLine("Meeting End DateTime:\nType: YEAR, MONTH, DAY, HOUR, MINUTES - separated by space");
                    string? meetingEndDateTime = Console.ReadLine();
                    if (!string.IsNullOrEmpty(meetingEndDateTime))
                    {
                        int year = 1,
                            month = 1,
                            day = 1,
                            hour = 0,
                            minutes = 0;

                        endDateArray = meetingEndDateTime.Split(' ').Select(Int32.Parse).ToArray();
                        int gotSize = endDateArray.Length;

                        if (endDateArray[0] > 0)
                        {
                            year = endDateArray[0];
                        }
                        if (gotSize > 1)
                        {
                            if (endDateArray[1] > 0 && endDateArray[1] <= 12)
                            {
                                month = endDateArray[1];
                            }
                        }
                        if (gotSize > 2)
                        {
                            if (endDateArray[2] > 0 && endDateArray[2] < 31)
                            {
                                day = endDateArray[2];
                            }
                        }
                        if (gotSize > 3)
                        {
                            if (endDateArray[3] >= 0 && endDateArray[3] < 24)
                            {
                                hour = endDateArray[3];
                            }
                        }
                        if (gotSize > 4)
                        {
                            if (endDateArray[4] >= 0 && endDateArray[4] < 60)
                            {
                                minutes = endDateArray[4];
                            }
                        }

                        meetingEndDate = new DateTime(year, month, day,
                                                        hour, minutes, 0);
                    }
                     dateList = listCommands.ListByStartEndDate(meetings,meetingStartDate,meetingEndDate);
                }
                else
                {
                    dateList = listCommands.ListByStartDate(meetings, meetingStartDate);
                }
            }
            else
            {
                dateList = listCommands.ListByStartDate(meetings, meetingStartDate);
            }

            Console.WriteLine("List printed with: Filtered by dates");

            listCommands.printList(dateList);
        }
        if (filterIntId == 6)
        {
            int numberOfAttendees = 0;
            Console.WriteLine("Type lowest number of attendees:");
            string? nrOfAttendees = Console.ReadLine();
            if (string.IsNullOrEmpty(nrOfAttendees))
            {
                numberOfAttendees = 0;
            }
            else
            {
                int.TryParse(nrOfAttendees, out numberOfAttendees);
            }
            var attendeesList = listCommands.ListOfAttendees(meetings, numberOfAttendees);
            Console.WriteLine("List printed with: Filtered by number of attendees");
            listCommands.printList(attendeesList);
        }

    }
}