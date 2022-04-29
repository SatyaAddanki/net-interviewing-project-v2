namespace Domain.V1
{
    /// <summary>
    /// Product DTO
    /// </summary>
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SalesPrice { get; set; }
        public int ProductTypeId { get; set; }
    }
}
