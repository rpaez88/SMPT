namespace SMPT.Entities.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
