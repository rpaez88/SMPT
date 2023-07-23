using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SMPT.Client.Models
{
    public class Student
    {
        public string? State { get; set; }
        public string? Course { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public bool Active { get; set; }
        public int Code { get; set; }

        public Student(int Code, string Name, string LastName, string Course, string State, bool Active)
        {
            this.Code = Code;
            this.Name = Name;
            this.LastName = LastName;
            this.Course = Course;
            this.State = State;
            this.Active = Active;
        }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}