using OctopusWebAPI.Entities;
using System;
using System.Collections.Generic;

namespace OctopusWebAPI.Entities
{
    public partial class RefreshToken
    {
        public Guid TokenId { get; set; }
        public string Token { get; set; }
        public string UserID { get; set; }

        public DateTime ExpiryDate { get; set; }

        public User User { get; set; }
    }
}
