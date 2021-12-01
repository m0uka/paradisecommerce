using System.Collections.Generic;

namespace ParadiseCommerce.Services.Users.Models
{
    public class ErrorModel
    {
        public ErrorModel() {}
        public ErrorModel(int status, string error)
        {
            Status = status;
            Errors.Add(error);
        }
        
        public int Status { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}