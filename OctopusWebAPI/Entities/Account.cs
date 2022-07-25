using System.ComponentModel.DataAnnotations;

namespace OctopusWebAPI.Entities
{
    public class Account
    {        
        public string UserTiktok { get; set; }
        public string UserID { get; set; }

        public string Password { get; set; }
        public string Group { get; set; }
        public string Mail { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime? DateBackup { get; set; }
        public DateTime? DateRun { get; set; }
        public bool Backup { get; set; }

        public User User { get; set; }


    }
}
