using SMPT.Entities.DbSet;

namespace SMPT.Entities.Dtos.Student
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public Guid CycleId { get; set; }
        public Cycle Cycle { get; set; }
        public Guid StateId { get; set; }
        public StudentState State { get; set; }
        public ICollection<Career> Careers { get; set; }
        public ICollection<Evidence> Evidences { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
