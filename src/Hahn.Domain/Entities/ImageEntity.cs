namespace Hahn.Domain.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public int ArtObjectId { get; set; }
        public ArtObjectEntity? ArtObject { get; set; }
        public string? Url { get; set; }
    }
}
