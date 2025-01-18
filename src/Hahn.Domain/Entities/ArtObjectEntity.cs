namespace Hahn.Domain.Entities
{
    public class ArtObjectEntity
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public int DepartmentId { get; set; }
        public required string Title { get; set; }
        public required string Department { get; set; }
        public required string ArtistDisplayName { get; set; }
        public string? Culture { get; set; }
        public string? Period { get; set; }
        public string? Medium { get; set; }
        public string? Dimensions { get; set; }
        public string? ObjectDate { get; set; }
        public string? ObjectURL { get; set; }
        public bool IsPublicDomain { get; set; }
        public DateTime MetadataDate { get; set; }
        public ICollection<ConstituentEntity> Constituents { get; set; } = new List<ConstituentEntity>();
        public ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();
        public ICollection<ImageEntity> Images { get; set; } = new List<ImageEntity>();
    }
}
