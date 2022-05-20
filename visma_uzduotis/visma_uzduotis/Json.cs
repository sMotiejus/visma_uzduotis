using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visma_uzduotis
{
    internal class Json
    {
        public string FileName { get; set; }
        public Json(string fileName) { FileName = fileName; }

        public List<Meeting> ReadJson()
        {
            List<Meeting>? meetingList = new List<Meeting>();
            string readText = File.ReadAllText(FileName);
            meetingList = JsonConvert.DeserializeObject<List<Meeting>>(readText);
            if(meetingList == null)
            {
                meetingList = new List<Meeting>();
            }
            return meetingList;
        }
        public void WriteJson(List<Meeting> meetings)
        {
            string jsonListString = JsonConvert.SerializeObject(meetings);
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                writer.Write(jsonListString);
            }
        }
    }
}
