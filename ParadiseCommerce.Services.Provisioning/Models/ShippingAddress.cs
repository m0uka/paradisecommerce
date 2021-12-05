namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ShippingAddress
    {
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}