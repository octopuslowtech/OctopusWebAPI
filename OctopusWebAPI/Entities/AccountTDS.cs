using System.ComponentModel.DataAnnotations;

namespace OctopusWebAPI.Entities
{
    public class AccountTDS
    {

        public string UserTDS { get; set; }
        public string UserID { get; set; }

        public string Password { get; set; }
        public string Xu { get; set; }
        public string TokenID { get; set; }
        public string Note { get; set; }
        public string Group { get; set; }   
        public User User { get; set; }

    }
}
