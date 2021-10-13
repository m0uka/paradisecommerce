using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace ParadiseCommerce.Services.Users.Models
{
    [CollectionName("Roles")]
    public class Role : MongoIdentityRole<ObjectId>
    {
        public Role() : base()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }
    }
}