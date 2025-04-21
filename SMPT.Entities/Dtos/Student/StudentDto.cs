using SMPT.Entities.DbSet;
using SMPT.Entities.Dtos.User;

namespace SMPT.Entities.Dtos.Student
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public Guid CycleId { get; set; }
        public Cycle Cycle { get; set; }
        public Guid StateId { get; set; }
        public StudentState State { get; set; }
        public Guid CareerId { get; set; }
        public Career Career { get; set; }
        public ICollection<Evidence> Evidences { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
