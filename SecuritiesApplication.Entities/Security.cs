namespace SecuritiesApplication.Entities
{
    public class Security
    {
        public Guid Id { get; set; }
        public string Isin { get; set; }
        public decimal Price { get; set; }
    }
}
