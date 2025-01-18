namespace Hahn.Application.DTOs
{
    public class MeasurementDto
    {
        public string? ElementName { get; set; }
        public string? ElementDescription { get; set; }
        public ElementMeasurementsDto ElementMeasurements { get; set; }
    }
}
