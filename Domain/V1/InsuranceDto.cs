using System.Text.Json.Serialization;

namespace Domain.V1
{
    /// <summary>
    /// Insurance DTO
    /// </summary>
    public class InsuranceDto
    {
        public int ProductId { get; set; }
        public float InsuranceValue { get; set; }
        [JsonIgnore]
        public string ProductTypeName { get; set; }
        [JsonIgnore]
        public bool ProductTypeHasInsurance { get; set; }
        [JsonIgnore]
        public float SalesPrice { get; set; }
    }
}
