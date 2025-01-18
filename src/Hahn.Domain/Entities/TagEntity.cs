namespace Hahn.Domain.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public int ArtObjectId { get; set; }
        public ArtObjectEntity? ArtObject { get; set; }
        public string? Term { get; set; }
        public string? WikidataURL { get; set; }
        public string? AATURL { get; set; }
    }
}
