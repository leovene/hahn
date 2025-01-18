namespace Hahn.Domain.Entities
{
    public class ConstituentEntity
    {
        public int Id { get; set; }
        public int ConstituentId { get; set; }
        public int ArtObjectId { get; set; }
        public ArtObjectEntity? ArtObject { get; set; }
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? WikidataURL { get; set; }
        public string? ULANURL { get; set; }
    }
}
