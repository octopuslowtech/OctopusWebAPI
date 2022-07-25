
namespace OctopusWebAPI.Entities
{
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public DateTime DateCreate { get; set; }

        public ICollection<Account> Account { get; set; }
        public ICollection<AccountTDS> AccountTDS { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        //public User()
        //{
        //    Account = new HashSet<Account>();
        //    AccountTDS = new HashSet<AccountTDS>();
        //    RefreshTokens = new HashSet<RefreshToken>();
        //}
  

    }
}
