namespace Hahn.Domain.Entities
{
    public class DepartmentEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public required string DisplayName { get; set; }
    }

}
