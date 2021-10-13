namespace ParadiseCommerce.Services.Ordering.Models
{
    public enum OrderStatus
    {
        WaitingForPayment,
        ReadyToShip,
        Shipped,
        Delivered,
        
        Active,
        Suspended,
        Cancelled,
    }
}