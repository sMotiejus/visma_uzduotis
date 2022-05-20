using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visma_uzduotis
{
    [Serializable]
    internal class Meeting
    {
        public string? Name { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public string? Category { get; set; }
        public string? Type { get; set; }
        public List<string> Participants { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Meeting(string? name, string? responsiblePerson,string? description, int? category, int? type, DateTime? startDate, DateTime? endDate) {
            Name = name;
            ResponsiblePerson = responsiblePerson;
            if(description != null)
            {
                Description = description;
            }
            else
            {
                Description = "";
            }

            if (category >= 0 && category < 4) 
            {
                Category = Enum.GetName(typeof(Categories), category);
            }
            else
            {
                Category = Enum.GetName(typeof(Categories), 0);
            }
            if (type >= 0 && type<2)
            {
                Type = Enum.GetName(typeof(Types), type);
            }
            else
            {
                Type = Enum.GetName(typeof(Types), 0);
            }
            Participants = new List<string>();
            if(responsiblePerson != null)
            {
                Participants.Add(responsiblePerson);
            }
            StartDate = startDate;
            EndDate = endDate;
        }
        public Meeting() { Participants = new List<string>(); Description = ""; }

        public string getCategories(int id)
        {
            string CName="";
            if (id >= 0 && id < 4)
            {
                CName = Enum.GetName(typeof(Categories), id)??"";
            }
            return CName;
        }
        public string getTypes(int id)
        {
            string TName = "";
            if (id >= 0 && id < 2)
            {
                TName = Enum.GetName(typeof(Types), id) ?? "";
            }
            return TName;
        }
        public enum Categories
        {
            CodeMonkey=0,
            Hub=1,
            Short=2,
            TeamBuilding=3
        }

        public enum Types
        {
            Live,
            InPerson
        }

        public override string ToString()
        {
            return String.Format("| {0,-14} | {1,-12} | {2,-20} | {3,-12} | {4,-8} | {5:yyyy/MM/dd HH:mm} | {6:yyyy/MM/dd HH:mm} |",
                                 Name,
                                 ResponsiblePerson,
                                 Description,
                                 Category,
                                 Type,
                                 StartDate,
                                 EndDate);
        }
    }
}
