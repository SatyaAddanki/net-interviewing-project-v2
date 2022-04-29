namespace Domain
{
    /// <summary>
    /// Insurance domain
    /// </summary>
    public class Insurance
    {
        public int ProductId { get; set; }
        public float InsuranceValue { get; set; }
        public string ProductTypeName { get; set; }
        public bool ProductTypeHasInsurance { get; set; }
        public float SalesPrice { get; set; }
    }
}
