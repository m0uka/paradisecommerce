using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace ParadiseCommerce.Services.Users.Models
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<ObjectId>
    {
        public User() : base()
        {
            
        }
        
        public User(string userName, string email) : base (userName, email) {}
        
        public string BillingName { get; set; }
        public string BillingStreetAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
    }
}